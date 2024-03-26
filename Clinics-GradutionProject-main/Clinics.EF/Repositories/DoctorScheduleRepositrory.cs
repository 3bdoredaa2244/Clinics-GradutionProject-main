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
    public class DoctorScheduleRepositrory : GenericRepository<DoctorSchedule>, IDoctorSchedule
    {
        protected ClinicContext _context;
        public DoctorScheduleRepositrory(ClinicContext context) : base(context)
        {
            _context = context;
        }


        //public async Task<IEnumerable<DoctorScheduleDTO>> GetDoctorSchedulesAsync(int SpecializationId)
        //{
        //    var doctorSchedules = await _context.DoctorSchedules
        //        .Include(d => d.Doctor).ThenInclude(u => u.User)
        //        .Where(d => d.Doctor.SpecializationId == SpecializationId)
        //        .GroupBy(d => new { d.DoctorId, d.Doctor.User.FirstName, d.Doctor.User.LastName })
        //        .Select(g => new DoctorScheduleDTO
        //        {
        //            DoctorId = g.Key.DoctorId,
        //            DoctorName = g.Key.FirstName + " " + g.Key.LastName,
        //            Dates = g.Select(d => new DoctorScheduleDateDTO
        //            {
        //                Date = d.ScheduleDateTime.Date,
        //                TimeSlots = g.Where(x => x.ScheduleDateTime.Date == d.ScheduleDateTime.Date)
        //                    .Select(x => new TimeSlot
        //                    {
        //                        Time = x.ScheduleDateTime.TimeOfDay,
        //                        IsAvailable = x.IsAvailable
        //                    }).ToList()
        //            }).ToList()
        //        })
        //        .ToListAsync();

        //    return doctorSchedules;
        //}

        /// this function here disperdly ned a refactor when YOu have time <summary>
        /// this function here disperdly ned a refactor when YOu have time
        /// /// this function here disperdly ned a refactor when YOu have time <summary>
        /// this function here disperdly ned a refactor when YOu have time
        /// this function here disperdly ned a refactor when YOu have time
        /// /// this function here disperdly ned a refactor when YOu have time

        public async Task<IEnumerable<DoctorScheduleDTO>> GetDoctorSchedulesAsync(int SpecializationId, int page, int pageSize)
        {
            var doctorSchedules = await _context.DoctorSchedules
                .Include(d => d.Doctor).ThenInclude(u => u.User)
                .Include(d => d.Doctor).ThenInclude(s => s.Specialization)
                .Include(d => d.Doctor).ThenInclude(c => c.Clinic)
                .Where(d => d.Doctor.SpecializationId == SpecializationId)
                .GroupBy(d => new { d.DoctorId, d.Doctor.User.FirstName, d.Doctor.User.LastName })
                .Select(g => new DoctorScheduleDTO
                {
                    DoctorId = g.Key.DoctorId,
                    DoctorName = g.Key.FirstName + " " + g.Key.LastName,
                    DoctorSpecialization = g.First().Doctor.Specialization.Name, // Access the specialization name
                    DoctorLocation = g.First().Doctor.Clinic.Location, // Access the clinic location
                    DoctorQualification = g.First().Doctor.Qualification,
                    Dates = g.GroupBy(d => d.ScheduleDateTime.Date)
                        .Select(gd => new DoctorScheduleDateDTO
                        {
                            Date = gd.Key,
                            TimeSlots = gd.Select(d => new TimeSlot
                            {
                                Time = d.ScheduleDateTime.TimeOfDay,
                                IsAvailable = d.IsAvailable
                            }).ToList()
                        }).ToList()
                })
                .ToListAsync();

            var doctorIds = doctorSchedules.Select(ds => ds.DoctorId).ToList();

            var topRatedDoctors = await _context.DoctorRatings
                .Where(r => doctorIds.Contains(r.DoctorId))
                .GroupBy(r => r.DoctorId)
                .Select(g => new
                {
                    DoctorId = g.Key,
                    AverageRating = g.Average(r => r.RatingValue) 
                })
                .OrderByDescending(r => r.AverageRating)
                .Take(pageSize)
                .ToListAsync();

            doctorSchedules = doctorSchedules.Join(topRatedDoctors,
                ds => ds.DoctorId,
                tr => tr.DoctorId,
                (ds, tr) =>
                {
                    ds.AverageRating = tr?.AverageRating ?? 3.0 ;
                    return ds;
                })
                .OrderByDescending(ds => ds.AverageRating)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return doctorSchedules;
        }

    }
}
