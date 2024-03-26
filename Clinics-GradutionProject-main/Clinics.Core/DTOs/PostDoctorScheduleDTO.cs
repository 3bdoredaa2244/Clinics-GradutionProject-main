using Clinics.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.Core.DTOs
{
    public class PostDoctorScheduleDTO
    {    
        public string DoctorId { get; set; }
        
        public DateTime ScheduleDateTime { get; set; }
        public bool IsAvailable { get; set; }
    }
}
