using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinics.Core.Models.Authentication;

namespace Clinics.Core.Models
{
    public class Patient
    {

        [Key, ForeignKey("User")]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
        public string? Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? bloodType { get; set; }
        public byte[]? QrCode { get; set; }

        public MedicalRecord? MedicalRecord  { get; set; }
        public ICollection<Prescription>? Prescriptions { get; set; }

        public ICollection<Reservation>? Reservations { get; set; }
        public ICollection<PatientHistory>? PatientHistories { get; set; }
        public ICollection<DoctorRating> DoctorRatings { get; set; }

    }
}
