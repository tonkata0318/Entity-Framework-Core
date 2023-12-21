using Medicines.Data.Models.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Medicines.DataProcessor.ExportDtos
{
    [XmlType("Patient")]
    public class ExportPatientDTO
    {
        [Required]
        [MaxLength(100)]
        [MinLength(5)]
        [XmlElement("Name")]
        public string FullName { get; set; }

        [Required]
        [XmlAttribute("Gender")]
        public string Gender { get; set; }

        [Required]
        [XmlElement("AgeGroup")]
        public string AgeGroup { get; set; }

        [XmlArray("Medicines")]
        public ExportMedicineDTO[] exportMedicineDTOs { get; set; }

    }
}
