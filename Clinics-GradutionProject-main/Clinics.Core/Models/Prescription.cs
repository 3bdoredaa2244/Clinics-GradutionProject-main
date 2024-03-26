using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.Core.Models
{
    public class Prescription
    {
        public int Id { get; set; }


        [ForeignKey("Doctor")]
        public string DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        [ForeignKey("Patient")]
        public string PatientId { get; set; }

        public Patient Patient { get; set; }
  
        public DateTime Date { get; set; }
        public string Notes { get; set; }

        public ICollection<DrugDetail> DrugDetails { get; set; }
    }

}
