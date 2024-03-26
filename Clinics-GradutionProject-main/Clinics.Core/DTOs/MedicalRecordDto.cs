using Clinics.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.Core.DTOs
{
    public class MedicalRecordDto
    {
        public string PastMedicalConditions { get; set; }
        public string PastSurgeries { get; set; }
        public string Hospitalizations { get; set; }
        public string FatherMedicalHistory { get; set; }
        public string MotherMedicalHistory { get; set; }
        public string GrandfatherMedicalHistory { get; set; }
        public string Allergies { get; set; }
        public List<Medication> Medications { get; set; }
        public List<Immunization> Immunizations { get; set; }
    }
}
