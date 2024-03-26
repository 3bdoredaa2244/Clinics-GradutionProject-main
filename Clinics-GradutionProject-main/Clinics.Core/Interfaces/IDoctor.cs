using Clinics.Core.DTOs;
using Clinics.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.Core.Interfaces
{
    public interface IDoctor : IGenericRepository<Doctor>
    {
        Task<List<DoctorDTO>> GetDoctors(int id);
        Task<DoctorDTO> GetDoctorbyId(string id);
        Task<PostDoctorDTO> AddDoctor(PostDoctorDTO postDoctorDTO);
        public Task<List<DoctorDTO>> GetAllDoctors();

    }
}
