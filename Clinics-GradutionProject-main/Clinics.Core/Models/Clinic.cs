using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.Core.Models
{
    public class Clinic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string phoneNumber { get; set; }
        [Column(TypeName = "decimal(9,6)")]
        public decimal latitude { get; set; }
        [Column(TypeName = "decimal(9,6)")]
        public decimal longitude { get; set; }

        public ICollection<Doctor> Doctors { get; set; }
        //public ICollection<Reservation> Reservations { get; set; }
        public ICollection<PatientHistory> PatientHistories { get; set; }
    }
}
