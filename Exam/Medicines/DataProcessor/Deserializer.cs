namespace Medicines.DataProcessor
{
    using Footballers.Helpers;
    using Medicines.Data;
    using Medicines.Data.Models;
    using Medicines.Data.Models.Enums;
    using Medicines.DataProcessor.ImportDtos;
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Linq;
    using System.Net.Http.Headers;
    using System.Text;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid Data!";
        private const string SuccessfullyImportedPharmacy = "Successfully imported pharmacy - {0} with {1} medicines.";
        private const string SuccessfullyImportedPatient = "Successfully imported patient - {0} with {1} medicines.";

        public static string ImportPatients(MedicinesContext context, string jsonString)
        {
            StringBuilder sb=new StringBuilder();
            var PatientsDTOs = JsonConvert.DeserializeObject<ImportPatientsDTO[]>(jsonString);
            var existingMedicineIds = context.Medicines
                .Select(m => m.Id)
                .ToArray();
            List<Patient> patients = new List<Patient>();
            foreach (var patientDTO in PatientsDTOs)
            {
                if (!IsValid(patientDTO))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Patient patienttoAdd = new Patient()
                {
                    FullName = patientDTO.FullName,
                    AgeGroup = (AgeGroup)patientDTO.AgeGroup,
                    Gender = (Gender)patientDTO.Gender
                };
                foreach (var patientmedicineid in patientDTO.MedicinesIds)
                {
                    if (patienttoAdd.PatientsMedicines.Any(pm=>pm.MedicineId==patientmedicineid))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    patienttoAdd.PatientsMedicines.Add(new PatientMedicine()
                    {
                        Patient = patienttoAdd,
                        MedicineId = patientmedicineid,
                    });
                }
                patients.Add(patienttoAdd);
                sb.AppendLine(string.Format(SuccessfullyImportedPatient,patienttoAdd.FullName,patienttoAdd.PatientsMedicines.Count()));
            }
            context.Patients.AddRange(patients);
            context.SaveChanges();
            return sb.ToString();  
        }

        public static string ImportPharmacies(MedicinesContext context, string xmlString)
        {
            var pharmacieDTOs = XmlSerializationHelper.Deserialize<ImportPharmacieDTO[]>(xmlString, "Pharmacies");

            StringBuilder sb= new StringBuilder();
            List<Pharmacy> pharmacies = new List<Pharmacy>();
            foreach (var pharmaciDTO in pharmacieDTOs)
            {
                if (!IsValid(pharmaciDTO))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                if (pharmaciDTO.IsNonStop!="true"&&pharmaciDTO.IsNonStop!="false")
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Pharmacy pharmacytoADD = new Pharmacy()
                {
                    Name = pharmaciDTO.Name,
                    IsNonStop=Convert.ToBoolean(pharmaciDTO.IsNonStop),
                    PhoneNumber= pharmaciDTO.PhoneNumber,
                };
                foreach (var medicineDTO in pharmaciDTO.importMedicineDTOs)
                {
                    if (!IsValid(medicineDTO))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    string format = "yyyy-MM-dd";
                    DateTime productionDate = DateTime.ParseExact(medicineDTO.ProductionDate, format, CultureInfo.InvariantCulture);
                    DateTime expiryDate = DateTime.ParseExact(medicineDTO.ExpiryDate, format, CultureInfo.InvariantCulture);
                    if (productionDate>=expiryDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    if (pharmacytoADD.Medicines.Any(m => m.Name == medicineDTO.Name && m.Producer == medicineDTO.Producer))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    pharmacytoADD.Medicines.Add(new Medicine()
                    { 
                        Name= medicineDTO.Name,
                        Category=(Category)medicineDTO.Category,
                        Price=medicineDTO.Price,
                        ProductionDate=productionDate,
                        ExpiryDate=expiryDate,
                        Producer=medicineDTO.Producer,
                    });
                }
                pharmacies.Add(pharmacytoADD);
                sb.AppendLine(string.Format(SuccessfullyImportedPharmacy, pharmacytoADD.Name, pharmacytoADD.Medicines.Count()));
            }
            context.Pharmacies.AddRange(pharmacies);
            context.SaveChanges();
            return sb.ToString();
        }


        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
