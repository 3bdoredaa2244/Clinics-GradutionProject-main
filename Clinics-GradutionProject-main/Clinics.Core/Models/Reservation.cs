using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.Core.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [ForeignKey("Doctor")]
        public string DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        [ForeignKey("Patient")]
        public string PatientId { get; set; }

        public Patient Patient { get; set; }


        //[ForeignKey("Clinic")]
        //public int ClinicId { get; set; }

        //public Clinic Clinic  { get; set; }

        public DateTime Date { get; set; }
        public bool type { get; set; }
        public bool Online { get; set; }
        
    }
}
