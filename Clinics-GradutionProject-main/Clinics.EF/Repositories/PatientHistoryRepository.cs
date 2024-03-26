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
    public class PatientHistoryRepository : GenericRepository<PatientHistory>, IPatientHistory
    {
        protected ClinicContext _context;
        public PatientHistoryRepository(ClinicContext context) : base(context)
        {
            _context = context;
        }


        public async Task<PatientHistoryDTO> GetPatientHistory(string id)
        {

            var patientHistories = await _context.PatientHistories
                .Include(d => d.Doctor).ThenInclude(u => u.User)
                .Include(p => p.Patient).ThenInclude(u => u.User)
                .Include(d => d.Diagnosis)
                .Include(s => s.Symptom)
                .Where(ph => ph.PatientId == id)
                .ToListAsync();

            var patientRecord = await _context.Patients
                .Include(u => u.User)
                .Include(pr => pr.MedicalRecord)
                .ThenInclude(m => m.Medications)
                .Include(pr => pr.MedicalRecord)
                .ThenInclude(m => m.Immunizations)
                .FirstOrDefaultAsync(p => p.UserId == id);

            if (patientHistories == null)
            {
                return null;
            };

            var patientDto = new PatientHistoryDTO
            {
                Name = patientRecord.User.FirstName + " " + patientRecord.User.LastName,
                DateOfBirth = patientRecord.DateOfBirth,
                BloodType = patientRecord.bloodType,
                MedicalRecord = patientRecord.MedicalRecord == null ? null : new MedicalRecordDto
                {
                    PastMedicalConditions = patientRecord.MedicalRecord.PastMedicalConditions,
                    PastSurgeries = patientRecord.MedicalRecord.PastSurgeries,
                    Hospitalizations = patientRecord.MedicalRecord.Hospitalizations,
                    FatherMedicalHistory = patientRecord.MedicalRecord.FatherMedicalHistory,
                    MotherMedicalHistory = patientRecord.MedicalRecord.MotherMedicalHistory,
                    GrandfatherMedicalHistory = patientRecord.MedicalRecord.GrandfatherMedicalHistory,
                    Allergies = patientRecord.MedicalRecord.Allergies,
                    Medications = patientRecord.MedicalRecord.Medications.Select(m => new Medication
                    {
                        Name = m.Name,
                    }).ToList(),
                    Immunizations = patientRecord.MedicalRecord.Immunizations.Select(i => new Immunization
                    {
                        Name = i.Name,
                        DateReceived = i.DateReceived
                    }).ToList()
                },
                Visits = patientHistories.Select(ph => new VisitDto
                {
                    DoctorName = ph.Doctor.User.FirstName + " " + ph.Doctor.User.LastName,
                    SymptomName = ph.Symptom.Name,
                    DiagnosisName = ph.Diagnosis.Name,
                    date = ph.Date
                }).ToList()
            };

            if (patientDto == null)
            {
                return null;
            }
            return patientDto;
        }

        public async Task<int> AddPatientHistoryAsync(string doctorId, string patientId, string symptomName, string diagnosisName, DateTime date)
        {
            var symptom = new Symptom { Name = symptomName };
            var diagnosis = new Diagnosis { Name = diagnosisName };

            // Add symptom and diagnosis to their respective tables
            _context.Symptoms.Add(symptom);
            _context.Diagnoses.Add(diagnosis);
            await _context.SaveChangesAsync();

            // Create new patient history record
            var patientHistory = new PatientHistory
            {
                DoctorId = doctorId,
                PatientId = patientId,               
                SymptomId = symptom.Id,
                DiagnosisId = diagnosis.Id,
                Date = date
            };

            // Add new patient history to the database
            _context.PatientHistories.Add(patientHistory);
            await _context.SaveChangesAsync();

            return patientHistory.Id;
        }


    }
}
