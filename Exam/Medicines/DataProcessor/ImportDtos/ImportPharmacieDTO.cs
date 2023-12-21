using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Medicines.DataProcessor.ImportDtos
{
    [XmlType("Pharmacy")]
    public class ImportPharmacieDTO
    {
        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        [XmlElement("Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(14)]
        [RegularExpression(@"\([0-9]{3}\) [0-9]{3}-[0-9]{4}")]
        [XmlElement("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Required]
        [XmlAttribute("non-stop")]
        public string IsNonStop { get; set; }

        [XmlArray("Medicines")]
        public ImportMedicineDTO[] importMedicineDTOs { get; set; }
    }
}
