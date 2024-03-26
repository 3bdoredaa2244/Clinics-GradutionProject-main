using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.Core.DTOs
{
    public class VisitDto
    {
        public string DoctorName { get; set; }
        public string SymptomName { get; set; }
        public string DiagnosisName { get; set; }
        public DateTime date { get; set; }
    }
}
