using AutoMapper;
using Clinics.Core;
using Clinics.Core.DTOs;
using Clinics.Core.Interfaces;
using Clinics.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinics.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MedicalRecordController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalRecord>> GetMedicalRecordbyId(string id)
        {
            var data = await _unitOfWork.MedicalRecord.GetMedicalRecord(id);
            if (data == null)
                return NotFound();
            return Ok(data);
        }

        //[HttpPost]
        //public async Task<ActionResult> AddMedicalRecord(MedicalRecord medicalRecord)
        //{
        //    if (medicalRecord == null)
        //    {
        //        return BadRequest();
        //    }
        //    await _unitOfWork.MedicalRecord.Add(medicalRecord);
        //    await _unitOfWork.Complete();

        //    return CreatedAtAction(nameof(GetMedicalRecordbyId), new { id = medicalRecord.Id }, medicalRecord);

        //}

        [HttpPost]
        public async Task<ActionResult<PostMedicalRecordDTO>> AddMedicalRecordAsync(PostMedicalRecordDTO postMedicalRecordDTO)
        {
            if (postMedicalRecordDTO == null)
            {
                return BadRequest();
            }
            await _unitOfWork.MedicalRecord.AddMedicalRecordAsync(postMedicalRecordDTO);
            await _unitOfWork.Complete();

            return CreatedAtAction(nameof(GetMedicalRecordbyId), new { id = postMedicalRecordDTO.id }, postMedicalRecordDTO);
        }

    }
}
