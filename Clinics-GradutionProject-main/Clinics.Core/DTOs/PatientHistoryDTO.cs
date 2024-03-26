using Clinics.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.Core.DTOs
{
    public class PatientHistoryDTO
    {
        public string Name { get; set; }        
        public DateTime? DateOfBirth { get; set; }
        public string? BloodType { get; set; }

        public MedicalRecordDto? MedicalRecord { get; set; }
        public List<VisitDto>? Visits { get; set; }
    }
}
