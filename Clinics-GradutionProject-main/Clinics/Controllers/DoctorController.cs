using AutoMapper;
using Clinics.Core;
using Clinics.Core.DTOs;
using Clinics.Core.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Clinics.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DoctorController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        // GET api/<SpecializationController>
        [HttpGet("clinic/doctors/{id}")]
        public async Task<ActionResult<IEnumerable<DoctorDTO>>> GetAll(int id)
        {
            var data = await _unitOfWork.Doctor.GetDoctors(id);
            if (data == null || !data.Any())
                return NotFound();
            return Ok(data);
        }

        // GET api/<SpecializationController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorDTO>> GetDoctorbyId(string id)
        {
            var data = await _unitOfWork.Doctor.GetDoctorbyId(id);
            if (data == null)
                return NotFound();
            return Ok(data);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorDTO>>> GetAll()
        {
            var data = await _unitOfWork.Doctor.GetAllDoctors();
            if (data == null || !data.Any())
                return NotFound();
            return Ok(data);
        }
        // POST api/<SpecializationController>
        [HttpPost]
        public async Task<ActionResult<PostDoctorDTO>> AddDoctor(PostDoctorDTO postDoctorDTO)
        {
            if (postDoctorDTO == null)
                return BadRequest();
            await _unitOfWork.Doctor.AddDoctor(postDoctorDTO);

            // Add a default rating for the doctor
            var doctorRating = new DoctorRating
            {
                DoctorId = postDoctorDTO.UserId,
                RatingValue = 5,
                PatientId = null
            };

            await _unitOfWork.DoctorRating.Add(doctorRating);
            await _unitOfWork.Complete();

            return CreatedAtAction(nameof(GetDoctorbyId), new { id = postDoctorDTO.UserId }, postDoctorDTO);
        }

        // DELETE api/<SpecializationController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDoctor(string id)
        {

            var doctor = await _unitOfWork.Doctor.GetById(id);
            if (doctor == null)
                return NotFound();
            await _unitOfWork.Doctor.Delete(doctor);
            await _unitOfWork.Complete();
            return NoContent();
        }

    }
}
