using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.Core.DTOs
{
    public class PostDoctorDTO
    {
        public string UserId { get; set; }
        public int SpecializationId { get; set; }
        public int ClinicId { get; set; }
    }
}
