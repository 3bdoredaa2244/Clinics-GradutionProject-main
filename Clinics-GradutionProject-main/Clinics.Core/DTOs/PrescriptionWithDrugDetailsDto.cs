using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.Core.DTOs
{
    public class PrescriptionWithDrugDetailsDto
    {
        public PrescriptionDto Prescription { get; set; }
        public List<DrugDetailDto> DrugDetails { get; set; }
    }

    public class PrescriptionDto
    {
        public int Id { get; set; }
        public string DoctorId { get; set; }
        public string PatientId { get; set; }
       
        public DateTime Date { get; set; }
        public string Notes { get; set; }
    }

    public class DrugDetailDto
    {
        public int Id { get; set; }
        public string DrugName { get; set; }
        public int Amount { get; set; }
        public string Route { get; set; }
        public string Frequency { get; set; }
    }
}
