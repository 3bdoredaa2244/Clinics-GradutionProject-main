using Clinics.Core.DTOs;
using Clinics.Core.Interfaces;
using Clinics.Core.Models;
using Clinics.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.EF.Repositories
{
    public class PatientRepository : GenericRepository<Patient>, IPatient
    {
        protected ClinicContext _context;
        public PatientRepository(ClinicContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PostPatientDTO> AddPatient(PostPatientDTO postPatientDTO)
        {
            var patient = new Patient
            {
                UserId = postPatientDTO.Id,
                Address = postPatientDTO.Address,
                bloodType = postPatientDTO.BloodType,
                QrCode = postPatientDTO.QRCode
            };
            _context.Patients.Add(patient);

            await _context.SaveChangesAsync();
            return postPatientDTO;
        }

        public async Task<PatientDTO> GetPatientbyId(string id)
        {
            var data = await _context.Patients
               .Include(u => u.User)
               .FirstOrDefaultAsync(d => d.UserId == id);

            if (data == null)
                return null;

            var patient = new PatientDTO
            {
                Id = data.User.Id,
                UserName = data.User.UserName,
                FullName = data.User.FirstName +" "+ data.User.LastName,
                Address = data.Address,
                BloodType = data.bloodType,
                QRCode = data.QrCode
            };

            return patient;
        }

        public async Task<List<PatientDTO>> GetPatients()
        {
            var data = await _context.Patients                
                .Include(u => u.User).ToListAsync();

            var Patients = data.Select(d => new PatientDTO
            {
                Id = d.UserId,
                UserName = d.User.UserName,
                FullName = d.User.FirstName + " " + d.User.LastName,
                Address = d.Address,
                BloodType = d.bloodType,
                QRCode = d.QrCode

            }).ToList();

            return Patients;
        }

    }
}
