using Medicines.Data.Models.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicines.DataProcessor.ImportDtos
{
    public class ImportPatientsDTO
    {
        [Required]
        [MaxLength(100)]
        [MinLength(5)]
        [JsonProperty("FullName")]
        public string FullName { get; set; }

        [JsonProperty("AgeGroup")]
        [Required]
        [Range(0,2)]
        public int AgeGroup { get; set; }

        [Required]
        [JsonProperty("Gender")]
        [Range(0,1)]
        public int Gender { get; set; }

        [Required]
        [JsonProperty("Medicines")]
        public int[] MedicinesIds { get; set; }
    }
}
