using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.Core.DTOs
{
    public class PostReservationDTO
    {
        public int id { get; set; }
        public string DoctorId { get; set; }
        public string PatientID { get; set; }
        public int? ClinicID { get; set; }
        public DateTime Date { get; set; }
        public bool Type { get; set; }
        public bool Online { get; set; }
    }
}
