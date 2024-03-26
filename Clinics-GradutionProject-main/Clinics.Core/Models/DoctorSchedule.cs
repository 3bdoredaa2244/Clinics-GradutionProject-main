using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.Core.Models
{
    public class DoctorSchedule
    {
        
        public int Id { get; set; }
        [ForeignKey("Doctor")]
        public string DoctorId { get; set; }
        public Doctor Doctor  { get; set; }
       
        public DateTime ScheduleDateTime { get; set; }
        public bool IsAvailable { get; set; }
    }
}
