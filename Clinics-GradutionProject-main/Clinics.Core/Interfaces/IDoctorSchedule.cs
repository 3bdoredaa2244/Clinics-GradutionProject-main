using Clinics.Core.DTOs;
using Clinics.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.Core.Interfaces
{
    public interface IDoctorSchedule : IGenericRepository<DoctorSchedule>
    {
        //public Task<IEnumerable<DoctorScheduleDTO>> GetDoctorSchedulesAsync(int SpecializationId);
        public Task<IEnumerable<DoctorScheduleDTO>> GetDoctorSchedulesAsync(int SpecializationId, int page, int pageSize);
    }
}
