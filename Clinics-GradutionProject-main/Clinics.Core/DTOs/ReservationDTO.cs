using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.Core.DTOs
{
    public class ReservationDTO
    {
        public int id { get; set; }
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
        public string? ClinicName { get; set; }
        public DateTime Date { get; set; }
        public bool Type { get; set; }
        public bool Online { get; set; }


    }
}
