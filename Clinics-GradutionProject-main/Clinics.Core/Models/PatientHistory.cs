using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.Core.Models
{
    public class PatientHistory
    {
        public int Id { get; set; }

           
        [ForeignKey("Doctor")]
        public string DoctorId { get; set; }
        public Doctor Doctor { get; set; }


        [ForeignKey("Patient")]
        public string PatientId { get; set; }

        public Patient Patient { get; set; }

        

        [ForeignKey("Symptom")]
        public int SymptomId { get; set; }
        public Symptom Symptom { get; set; }


        [ForeignKey("Diagnosis")]
        public int DiagnosisId { get; set; }
        public Diagnosis Diagnosis { get; set; }

       public DateTime Date { get; set; }
        
    }
}
