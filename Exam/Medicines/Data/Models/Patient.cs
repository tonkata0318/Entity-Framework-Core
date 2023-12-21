﻿using Medicines.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicines.Data.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(5)]
        public string FullName { get; set; }

        [Required]
        public AgeGroup AgeGroup { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public virtual ICollection<PatientMedicine> PatientsMedicines { get; set; } = new List<PatientMedicine>();
    }

}
