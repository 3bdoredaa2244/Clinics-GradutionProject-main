using AutoMapper;
using Clinics.Core;
using Clinics.Core.DTOs;
using Clinics.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinics.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientHistoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PatientHistoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientHistoryDTO>> GetPatientHistory(string id)
        {
            try
            {
                var data = await _unitOfWork.PatientHistory.GetPatientHistory(id);
                if (data == null)
                    return NotFound("Patient history not found.");

                return Ok(data);
            }
            catch (Exception ex)
            {
                // Log the exception here
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while getting the patient history.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddPatientHistory(string doctorId, string patientId, string symptomName, string diagnosisName, DateTime date)
        {
            
          

            if (doctorId == null || patientId == null||symptomName == null||diagnosisName == null||date == null)
                return NotFound();

            _unitOfWork.PatientHistory.AddPatientHistoryAsync(doctorId,patientId,symptomName,diagnosisName,date);

            // Save changes to the repository
            await _unitOfWork.Complete();

            // Map the PatientHistory entity to a DTO and return it
            
            return Ok();
        }

    }
}
