using Clinics.Core.DTOs;
using Clinics.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.Core.Interfaces
{
    public interface IPrescription : IGenericRepository<Prescription>
    {
        public Task<List<PrescriptionWithDrugDetailsDto>> GetPrescriptionsByPatientId(string patientId, DateTime date);
        public Task<PrescriptionWithDrugDetailsDto> AddPrescription(PrescriptionWithDrugDetailsDto prescriptionDto);
    }
}
