namespace Medicines.DataProcessor
{
    using Footballers.Helpers;
    using Medicines.Data;
    using Medicines.Data.Models;
    using Medicines.Data.Models.Enums;
    using Medicines.DataProcessor.ExportDtos;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using System.Globalization;
    using System.Xml.Linq;

    public class Serializer
    {
        public static string ExportPatientsWithTheirMedicines(MedicinesContext context, string date)
        {
            string format = "yyyy-MM-dd";
            DateTime productionDate = DateTime.ParseExact(date, format, CultureInfo.InvariantCulture);

            var patients=context.Patients
                .Include(p=>p.PatientsMedicines)
                .ThenInclude(pm=>pm.Medicine)
                .AsNoTracking()
                .ToArray()
                .Where(p=>p.PatientsMedicines.Any(pm=>pm.Medicine.ProductionDate>=productionDate))
                .Select(p=>new ExportPatientDTO()
                {
                    FullName= p.FullName,
                    Gender=p.Gender.ToString().ToLower(),
                    AgeGroup= p.AgeGroup.ToString(),
                    exportMedicineDTOs=p.PatientsMedicines
                        .Where(pm=>pm.Medicine.ProductionDate>=productionDate)
                        .Select(pm=> new ExportMedicineDTO()
                        {
                            Name=pm.Medicine.Name,
                            Price=pm.Medicine.Price,
                            Producer=pm.Medicine.Producer,
                            BestBefore=pm.Medicine.ExpiryDate.ToString("yyyy-MM-dd"),
                            Category=pm.Medicine.Category.ToString().ToLower()
                        })
                        .OrderByDescending(pm=>pm.BestBefore)
                        .ThenBy(pm=>pm.Price)
                        .ToArray()
                })
                .OrderByDescending(p=>p.exportMedicineDTOs.Count())
                .ThenBy(p=>p.FullName)
                .ToList();

            return XmlSerializationHelper.Serialize(patients, "Patients");
        }

        public static string ExportMedicinesFromDesiredCategoryInNonStopPharmacies(MedicinesContext context, int medicineCategory)
        {
            Category category = (Category)medicineCategory;
            var medicines = context.Medicines
                .Where(m => m.Category == category && m.Pharmacy.IsNonStop)
                .Select(m => new
                {
                    Name = m.Name,
                    Price = m.Price,
                    Pharmacy = new
                    {
                        Name = m.Pharmacy.Name,
                        PhoneNumber = m.Pharmacy.PhoneNumber
                    }
                })
                .OrderBy(m=>m.Price)
                .ThenBy(m=>m.Name)
                .ToArray();
            var medicine2 = medicines
                .Select(m => new
                {
                    Name = m.Name,
                    Price = m.Price.ToString("F2"),
                    Pharmacy = new
                    {
                        Name = m.Pharmacy.Name,
                        PhoneNumber = m.Pharmacy.PhoneNumber
                    }
                });
            return JsonConvert.SerializeObject(medicine2, Formatting.Indented);
        }
    }
}
