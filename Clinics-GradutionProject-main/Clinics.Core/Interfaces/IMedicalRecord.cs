using Clinics.Core.DTOs;
using Clinics.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.Core.Interfaces
{
    public interface IMedicalRecord : IGenericRepository<MedicalRecord>
    {

        public Task<PostMedicalRecordDTO> AddMedicalRecordAsync(PostMedicalRecordDTO postMedicalRecordDTO);
        public Task<MedicalRecord> GetMedicalRecord(string userId);
    }

}
