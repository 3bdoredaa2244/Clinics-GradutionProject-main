using Clinics.Core.DTOs;
using Clinics.Core.Interfaces;
using Clinics.Core.Models;
using Clinics.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.EF.Repositories
{
    public class PrescriptionRepository : GenericRepository<Prescription>, IPrescription
    {

        public PrescriptionRepository(ClinicContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PrescriptionWithDrugDetailsDto> AddPrescription(PrescriptionWithDrugDetailsDto prescriptionDto)
        {
           
            DateTime utcDate =  prescriptionDto.Prescription.Date.ToUniversalTime();
            var prescription = new Prescription
            {
                DoctorId = prescriptionDto.Prescription.DoctorId,
                PatientId = prescriptionDto.Prescription.PatientId,
               
                Date = utcDate,
                Notes = prescriptionDto.Prescription.Notes,
            };

            _context.Prescriptions.Add(prescription);

            await _context.SaveChangesAsync();

            foreach (var detail in prescriptionDto.DrugDetails)
            {
                var drugDetail = new DrugDetail
                {
                    DrugName = detail.DrugName,
                    Amount = detail.Amount,
                    Route = detail.Route,
                    Frequency = detail.Frequency,
                    PrescriptionId = prescription.Id
                };
                _context.DrugDetails.Add(drugDetail);
            }
            await _context.SaveChangesAsync();

            return prescriptionDto;

        }

        public async Task<List<PrescriptionWithDrugDetailsDto>> GetPrescriptionsByPatientId(string patientId, DateTime date)
        {
            var prescriptions = await _context.Prescriptions
                .Include(p => p.DrugDetails)
                .Where(p => p.PatientId == patientId && p.Date.Date == date.Date)
                .ToListAsync();

            var prescriptionDtos = new List<PrescriptionWithDrugDetailsDto>();
            foreach (var prescription in prescriptions)
            {
                var drugDetailDtos = prescription.DrugDetails.Select(d => new DrugDetailDto
                {
                    DrugName = d.DrugName,
                    Amount = d.Amount,
                    Route = d.Route,
                    Frequency = d.Frequency
                    
                }).ToList();

                var prescriptionDto = new PrescriptionWithDrugDetailsDto
                {
                    Prescription = new PrescriptionDto
                    {
                        Id = prescription.Id,
                        DoctorId = prescription.DoctorId,
                        PatientId = prescription.PatientId,                       
                        Date = prescription.Date,
                        Notes = prescription.Notes
                                               
                    },
                    DrugDetails = drugDetailDtos
                };

                prescriptionDtos.Add(prescriptionDto);
            }

            return prescriptionDtos;
        }


        //public async Task<PostPrescriptionDTO> AddPrescription(PostPrescriptionDTO postPrescriptionDTO)
        //{
        //    var dignosis = new Diagnosis
        //    {
        //        Name = postPrescriptionDTO.DiagnosisName,
        //    };
        //    var symptom = new Symptom
        //    {
        //        Name = postPrescriptionDTO.SymptomName,
        //    };

        //    _context.Diagnoses.Add(dignosis);
        //    _context.Symptoms.Add(symptom);
        //    await _context.SaveChangesAsync();

        //    var prescription = new Prescription
        //    {
        //        DoctorId = postPrescriptionDTO.DoctorId,
        //        PatientId = postPrescriptionDTO.PatientId,
        //        Drug = postPrescriptionDTO.Drug,
        //        Serial = postPrescriptionDTO.Serial,
        //        Route = postPrescriptionDTO.Route,
        //        Frequency = postPrescriptionDTO.Frequency,
        //        Amount = postPrescriptionDTO.Amount,
        //        Notes = postPrescriptionDTO.Notes,
        //        Date = postPrescriptionDTO.Date,
        //    };
        //    _context.Prescriptions.Add(prescription);

        //    var PatientHistory = new PatientHistory
        //    {
        //        DoctorId = postPrescriptionDTO.DoctorId,
        //        PatientId = postPrescriptionDTO.PatientId,
        //        ClinicId = postPrescriptionDTO.ClinicId,
        //        Date = postPrescriptionDTO.Date,
        //        SymptomId = symptom.Id,
        //        DiagnosisId = dignosis.Id,
        //    };
        //    _context.PatientHistories.Add(PatientHistory);

        //    await _context.SaveChangesAsync();


        //    return postPrescriptionDTO;
        //}


        //public async Task<PrescriptionDTO> getPrescription(string id)
        //{
        //    var data = await _context.Prescriptions
        //                                .Include(p => p.Patient)
        //                                .ThenInclude(u => u.User)
        //                                .Include(d => d.Doctor)
        //                                .ThenInclude(u => u.User)
        //                                .FirstOrDefaultAsync(p => p.PatientId == id);

        //    if (data == null)
        //    {
        //        return null;
        //    }
        //    var prescription = new PrescriptionDTO
        //    {
        //        PatientName = data.Patient.User.FirstName + " " + data.Patient.User.LastName,
        //        DoctorName = data.Doctor.User.FirstName + " " + data.Doctor.User.LastName,
        //        Serial = data.Serial,
        //        Drug = data.Drug,
        //        Amount = data.Amount,
        //        Frequency = data.Frequency,
        //        Route = data.Route,
        //        Notes = data.Notes,
        //        Date = data.Date,
        //    };

        //    return prescription;




        //}


    }
}
