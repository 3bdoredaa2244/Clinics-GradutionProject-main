using Clinics.Core.DTOs;
using Clinics.Core.Interfaces;
using Clinics.Core.Models;
using Clinics.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinics.Core.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Clinics.EF.Repositories
{
    public class DoctorRepository : GenericRepository<Doctor>, IDoctor
    {
        protected ClinicContext _context;
        public DoctorRepository(ClinicContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PostDoctorDTO> AddDoctor(PostDoctorDTO postDoctorDTO)
        {
            var doctor = new Doctor
            {
                UserId = postDoctorDTO.UserId,
                SpecializationId = postDoctorDTO.SpecializationId,
                ClinicId = postDoctorDTO.ClinicId,
            };
            _context.Doctors.Add(doctor);

            await _context.SaveChangesAsync();
            return postDoctorDTO;
        }

        public async Task<DoctorDTO> GetDoctorbyId(string id)
        {
            var data = await _context.Doctors
               .Include(s => s.Specialization)
               .Include(c => c.Clinic)
               .Include(u => u.User)
               .FirstOrDefaultAsync(d => d.UserId == id);

            if (data == null)
                return null;

            var Doctor = new DoctorDTO
            {
                Id = data.User.Id,
                UserName = data.User.UserName,
                Cliniclocation = data.Clinic.Location,
                SpecializationName = data.Specialization.Name
            };

            return Doctor;
        }

        public async Task<List<DoctorDTO>> GetDoctors(int clinicid)
        {
            var data = await _context.Doctors
                .Include(s => s.Specialization)
                .Include(c => c.Clinic)
                .Include(u => u.User)
                .Where(d => d.ClinicId == clinicid)
                .ToListAsync();

            var Doctors = data.Select(d => new DoctorDTO
            {
                Id = d.UserId,
                UserName = d.User.FirstName + " " + d.User.LastName,
                Cliniclocation = d.Clinic.Location,
                SpecializationName = d.Specialization.Name

            }).ToList();

            return Doctors;
        }

        public async Task<List<DoctorDTO>> GetAllDoctors()
        {
            var data = await _context.Doctors
                .Include(s => s.Specialization)
                .Include(c => c.Clinic)
                .Include(u => u.User)                
                .ToListAsync();

            var Doctors = data.Select(d => new DoctorDTO
            {
                Id = d.UserId,
                UserName = d.User.FirstName + " " + d.User.LastName,
                Cliniclocation = d.Clinic.Location,
                SpecializationName = d.Specialization.Name

            }).ToList();

            return Doctors;
        }

    }
}
