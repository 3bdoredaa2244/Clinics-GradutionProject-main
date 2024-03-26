using Clinics.Core.DTOs;
using Clinics.Core.Interfaces;
using Clinics.Core.Models;
using Clinics.Data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.EF.Repositories
{
    public class ReservationRepository : GenericRepository<Reservation>, IReservation
    {
        protected ClinicContext _context;
        
        public ReservationRepository(ClinicContext context) : base(context)
        {
            _context = context;
        }  

        public async Task<IEnumerable<ReservationDTO>> GetReservation(string id)
        {
            var data = await _context.Reservations
                .Include(d => d.Doctor).ThenInclude(u => u.User)
                .Include(p => p.Patient).ThenInclude(u => u.User)                                
                .Where(r => r.PatientId == id || r.DoctorId == id)
                .ToListAsync();


            if (data == null)
            {
                return null;
            }

            var resrvations = data.Select(r => new ReservationDTO
            {
                id = r.Id,
                DoctorName = r.Doctor.User.FirstName + " " + r.Doctor.User.LastName,
                PatientName = r.Patient.User.FirstName + " " + r.Patient.User.LastName,             
                Date = r.Date.AddHours(2),
                Type = r.type,
                Online = r.Online


            });
            return resrvations;
        }
        public async Task<IEnumerable<ReservationDTO>> GetReservations()
        {
            var data = await _context.Reservations
                .Include(d => d.Doctor).ThenInclude(u => u.User)
                .Include(p => p.Patient).ThenInclude(u => u.User)                
                .ToListAsync();

            if (data == null)
            {
                return null;
            }
            var resrvations = data.Select(r => new ReservationDTO
            {
                id = r.Id,
                DoctorName = r.Doctor.User.FirstName + " " + r.Doctor.User.LastName,
                PatientName = r.Patient.User.FirstName + " " + r.Patient.User.LastName,                
                Date = r.Date,
                Type = r.type,
                Online = r.Online
                

            });
            return resrvations;
        }
        public async Task<PostReservationDTO> AddReservation(PostReservationDTO postReservationDTO)
        {
            DateTime utcDate = postReservationDTO.Date.ToUniversalTime();
            var reservation = new Reservation
            {
                DoctorId = postReservationDTO.DoctorId,
                PatientId = postReservationDTO.PatientID,                
                Date = utcDate,
                type = postReservationDTO.Type,
                Online = postReservationDTO.Online,
            };

            _context.Reservations.Add(reservation);
            

            return postReservationDTO;
            
        }
    }
}
