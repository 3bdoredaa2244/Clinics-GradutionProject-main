using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.Core.Models
{
    public class MedicalRecord
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Patient")]
        public string PatientId { get; set; }

        public Patient Patient { get; set; }
        // MedicalRecord
        public string PastMedicalConditions { get; set; } // "None
        public string PastSurgeries { get; set; } // "None
        public string Hospitalizations { get; set; } // "None
        public string FatherMedicalHistory { get; set; } //"Hypertension"
        public string MotherMedicalHistory { get; set; } //"Type 2 diabetes"
        public string GrandfatherMedicalHistory { get; set; }//"Heart disease"       
        public string Allergies { get; set; } //None

        public List<Medication>? Medications { get; set; } //1 "Lisinopril (10mg) for blood pressure" 2 "    ",
        public List<Immunization>? Immunizations { get; set; }  // 1 -"Flu vaccine received annually" 2 "Tetanus vaccine received in 2018" 
    }
}
