using Clinics.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.Core.DTOs
{
    public class PostPrescriptionDTO
    {
        
        public int ClinicId { get; set; }
        public string PatientId { get; set; }

        public string DoctorId { get; set; }
        public string Drug { get; set; }
        public int Serial { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public string Notes { get; set; }
        public string Route { get; set; }
        public string Frequency { get; set; }

        public string SymptomName { get; set; }           
        public string DiagnosisName { get; set; }
    }
}
