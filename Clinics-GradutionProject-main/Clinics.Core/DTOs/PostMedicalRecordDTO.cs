using Clinics.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.Core.DTOs
{
    public class PostMedicalRecordDTO
    {
        public int id { get; set; }
        public string PatientId { get; set; }
        public string PastMedicalConditions { get; set; } // "None
        public string PastSurgeries { get; set; } // "None
        public string Hospitalizations { get; set; } // "None
        public string FatherMedicalHistory { get; set; } //"Hypertension"
        public string MotherMedicalHistory { get; set; } //"Type 2 diabetes"
        public string GrandfatherMedicalHistory { get; set; }//"Heart disease"       
        public string Allergies { get; set; } //None

        public List<MedicationDTO>? Medications { get; set; } //1 "Lisinopril (10mg) for blood pressure" 2 " 
        public List<ImmunizationDTO>? Immunizations { get; set; }
    }
}
