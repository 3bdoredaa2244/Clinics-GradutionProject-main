using AutoMapper;
using Clinics.Core;
using Clinics.Core.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinics.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PatientController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDTO>>> GetAll()
        {
            var data = await _unitOfWork.Patient.GetPatients();
            if (data == null || !data.Any())
                return NotFound();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDTO>> GetPatientbyId(string id)
        {
            var data = await _unitOfWork.Patient.GetPatientbyId(id);
            if (data == null)
                return NotFound();
            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<PostPatientDTO>> AddDoctor(PostPatientDTO postPatientDTO)
        {
            if (postPatientDTO == null)
                return BadRequest();
            await _unitOfWork.Patient.AddPatient(postPatientDTO);
            await _unitOfWork.Complete();
            return CreatedAtAction(nameof(GetPatientbyId), new { id = postPatientDTO.Id }, postPatientDTO);
        }
    }
}
