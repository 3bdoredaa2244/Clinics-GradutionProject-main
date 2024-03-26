using Clinics.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinics.Core.Models;


namespace Clinics.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Posts> Post { get; }
        IRecipe Recipe { get; }
        IIngredient Ingredient { get; }
        IAuth Auth { get; }
        ISpecialization Specialization { get; }
        IClinic Clinic { get; }
        IDoctor Doctor { get; }
        IPatient Patient { get; }
        IDiagnosis Diagnosis { get; }
        ISymptom Symptom { get; }
        IPatientHistory PatientHistory { get; }
        IReservation Reservation { get; }
        IMedicalRecord MedicalRecord { get; }
        IDoctorSchedule DoctorSchedule { get; }
        IPrescription Prescription { get; }
        IDoctorRating DoctorRating { get; }
        Task Complete();
    }
}
