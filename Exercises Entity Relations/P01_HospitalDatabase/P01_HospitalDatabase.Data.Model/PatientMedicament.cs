using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01_HospitalDatabase.Data.Model
{
    public class PatientMedicament
    {
        public int PatientId { get; set; }

        [ForeignKey(nameof(PatientId))]
        public virtual Patient Patient { get; set; }

        public int MedicamentId { get; set; }

        [ForeignKey(nameof(MedicamentId))]
        public virtual Medicament Medicament { get; set; }
    }
}
