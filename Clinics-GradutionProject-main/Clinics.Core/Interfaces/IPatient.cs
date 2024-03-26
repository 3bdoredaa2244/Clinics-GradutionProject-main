using Clinics.Core.DTOs;
using Clinics.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.Core.Interfaces
{
    public interface IPatient : IGenericRepository<Patient>
    {
        Task<List<PatientDTO>> GetPatients();
        Task<PatientDTO> GetPatientbyId(string id);
        Task<PostPatientDTO> AddPatient(PostPatientDTO postPatientDTO);
    
    }
}
