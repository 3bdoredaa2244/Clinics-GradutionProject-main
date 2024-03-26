using Clinics.Core.Models.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.Core.Models
{
    public class Doctor
    {
        [Key, ForeignKey("User")]
        public string UserId { get; set; }

        public string? Qualification { get; set; }
        public ApplicationUser User { get; set; }

        [ForeignKey("Specialization")]
        public int SpecializationId { get; set; }

        public Specialization Specialization { get; set; }

        [ForeignKey("Clinic")]
        public int ClinicId { get; set; }

        public Clinic Clinic { get; set; }

        public ICollection<Prescription> Prescriptions { get; set; }
        public ICollection<Reservation> Reservations  { get; set; }
        public ICollection<PatientHistory> PatientHistories { get; set; }
        public ICollection<DoctorSchedule> DoctorSchedules  { get; set; }
        public ICollection<DoctorRating> DoctorRatings { get; set; }
    }
}
