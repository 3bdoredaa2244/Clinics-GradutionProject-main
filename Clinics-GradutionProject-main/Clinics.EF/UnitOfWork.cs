using Clinics.Core;
using Clinics.Core.Interfaces;
using Clinics.Core.Models;
using Clinics.Core.Models.Authentication;
using Clinics.Data;
using Clinics.EF.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Clinics.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ClinicContext _context;
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly IConfiguration _configuration;
        public IGenericRepository<Core.Models.Posts> Post { get; private set; }
        

        public IRecipe Recipe { get; private set; }
        public IIngredient Ingredient { get; private set; }

        public IAuth Auth { get; private set; }

        public ISpecialization Specialization { get; private set; }
        public IClinic Clinic  { get; private set; }
        public IDoctor Doctor { get; private set; }
        public IPatient Patient { get; private set; }
        public ISymptom Symptom { get; private set; }
        public IDiagnosis Diagnosis { get; private set; }
        public IReservation Reservation { get; private set; }
        public IMedicalRecord MedicalRecord { get; private set; }
        public IPatientHistory PatientHistory { get; private set; }
        public IPrescription Prescription { get; private set; }
        public IDoctorSchedule DoctorSchedule { get; private set; }
        public IDoctorRating DoctorRating { get; private set; }
        public UnitOfWork(ClinicContext context, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _context = context;
            _userManger = userManager;
            _configuration = configuration;

            Recipe = new RecipeRepository(_context);
            Ingredient = new IngredientRepository(_context);

            Auth = new AuthRepository(userManager,configuration, _context);
            Specialization = new SpecializationRepository(_context);
            Clinic = new ClinicRepository(_context);
            Doctor = new DoctorRepository(_context);
            Patient = new PatientRepository(_context);
            Diagnosis = new DiagnosisRepository(_context);
            Symptom = new SymptomRepository(_context);
            PatientHistory = new PatientHistoryRepository(_context);
            Reservation = new ReservationRepository(_context);
            MedicalRecord = new MedicalRecordRepository(_context);
            Prescription = new PrescriptionRepository(_context);
            DoctorSchedule = new DoctorScheduleRepositrory(_context);
            DoctorRating = new DoctorRatingRepository(_context);
        }

        public async Task Complete()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
