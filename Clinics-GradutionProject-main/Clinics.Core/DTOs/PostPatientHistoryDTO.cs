using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.Core.DTOs
{
    public class PostPatientHistoryDTO
    {
        public int Id { get; set; }
        public string DoctorId { get; set; }
        public string PatientId { get; set; }
        public int ClinicId { get; set; }
        public int SymptomId { get; set; }
        public int DiagnosisId { get; set; }

        public string PastMedicalConditions { get; set; }
        public string PastSurgeries { get; set; }
        public string Hospitalizations { get; set; }
        public string FatherMedicalHistory { get; set; }
        public string MotherMedicalHistory { get; set; }
        public string GrandfatherMedicalHistory { get; set; }
        public string Allergies { get; set; }
        public List<string> Medications { get; set; }
        public List<string> Immunizations { get; set; }
    }
}
