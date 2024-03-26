using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.Core.DTOs
{
    public class DoctorScheduleDTO
    {
        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string? DoctorLocation { get; set; }
        public string? DoctorSpecialization { get; set; }
        public string? DoctorQualification { get; set; }

        public List<DoctorScheduleDateDTO> Dates { get; set; }
        public double? AverageRating { get; set; } // Add AverageRating property
    }

    public class DoctorScheduleDateDTO
    {
        public DateTime Date { get; set; }
        public List<TimeSlot> TimeSlots { get; set; }
    }

    public class TimeSlot
    {
        public TimeSpan Time { get; set; }
        public bool IsAvailable { get; set; }
    }



}
