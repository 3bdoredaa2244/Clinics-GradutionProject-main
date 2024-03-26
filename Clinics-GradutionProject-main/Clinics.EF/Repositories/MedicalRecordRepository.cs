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
    public class MedicalRecordRepository : GenericRepository<MedicalRecord>, IMedicalRecord
    {
        protected ClinicContext _context;
        public MedicalRecordRepository(ClinicContext context) : base(context)
        {
            _context = context;
        }
      
        public async Task<PostMedicalRecordDTO> AddMedicalRecordAsync(PostMedicalRecordDTO postMedicalRecordDTO)
        {


            var MedicalRecord = new MedicalRecord
            {
                PatientId = postMedicalRecordDTO.PatientId,
                FatherMedicalHistory = postMedicalRecordDTO.FatherMedicalHistory,
                MotherMedicalHistory = postMedicalRecordDTO.MotherMedicalHistory,
                GrandfatherMedicalHistory = postMedicalRecordDTO.GrandfatherMedicalHistory,
                PastMedicalConditions = postMedicalRecordDTO.PastMedicalConditions,
                PastSurgeries = postMedicalRecordDTO.PastSurgeries,
                Allergies = postMedicalRecordDTO.Allergies,
                Hospitalizations = postMedicalRecordDTO.Hospitalizations,
                 
            };
            _context.MedicalRecords.Add(MedicalRecord);
            await _context.SaveChangesAsync();

            #region
            //another approch

            //if (MedicalRecord.Medications != null)
            //{
            //    var medications1 = new List<Medication>();
            //    foreach (var medication in MedicalRecord.Medications)
            //    {
            //        medications1.Add(new Medication
            //        {
            //            Name = medication.Name,
            //            MedicalRecordID = MedicalRecord.Id
            //        });
            //    }
            //    _context.Medications.AddRange(medications1);
            //    await _context.SaveChangesAsync();
            //}

            //// Create immunizations and add to database
            //if (MedicalRecord.Immunizations != null)
            //{
            //    var immunizations1 = new List<Immunization>();
            //    foreach (var immunization in MedicalRecord.Immunizations)
            //    {
            //        immunizations1.Add(new Immunization
            //        {
            //            Name = immunization.Name,
            //            DateReceived = immunization.DateReceived,
            //            MedicalRecordID = MedicalRecord.Id
            //        });
            //    }
            //    _context.Immunizations.AddRange(immunizations1);
            //    await _context.SaveChangesAsync();
            //}
            #endregion


            var medications = MedicalRecord.Medications?.Select(m => new Medication
            {
                Name = m.Name,
                MedicalRecordID = MedicalRecord.Id
            }).ToList();            

            var immunizations = MedicalRecord.Immunizations?.Select(i => new Immunization
            {
                Name = i.Name,
                DateReceived = i.DateReceived,
                MedicalRecordID = MedicalRecord.Id
            }).ToList();

            // Add medications and immunizations to database
            if (medications != null)
            {
                _context.Medications.AddRange(medications);
                await _context.SaveChangesAsync();
            }

            if (immunizations != null)
            {
                _context.Immunizations.AddRange(immunizations);
                await _context.SaveChangesAsync();
            }

            return postMedicalRecordDTO;
        }

        public async Task<MedicalRecord> GetMedicalRecord(string userId)
        {
            var record = await _context.MedicalRecords.FirstOrDefaultAsync(u => u.PatientId == userId);

            if (record == null)
            {
                return null;
            }

            return record;
        }
    }
}
