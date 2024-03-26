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
    public class SpecializationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SpecializationController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        // GET api/<SpecializationController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Specialization>>> GetAll()
        {
            var data = await _unitOfWork.Specialization.GetAll();
            if (data == null || !data.Any())
                return NotFound();
            return Ok(data);
        }

        // GET api/<SpecializationController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IngredientDto>> GetbyId(int id)
        {
            var data = await _unitOfWork.Specialization.GetById(id);
            if (data == null)
                return NotFound();
            return Ok(data);
        }

        // POST api/<SpecializationController>
        [HttpPost]
        public async Task<ActionResult<Specialization>> AddSpecialization(Specialization specialization)
        {
            if (specialization == null)
                return BadRequest();
            await _unitOfWork.Specialization.Add(specialization);
            await _unitOfWork.Complete();
            return CreatedAtAction(nameof(GetbyId), new { id = specialization.ID }, specialization);
        }

        // DELETE api/<SpecializationController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSpecialization(int id)
        {

            var specialization = await _unitOfWork.Specialization.GetById(id);
            if (specialization == null)
                return NotFound();
            _unitOfWork.Specialization.Delete(specialization);
            await _unitOfWork.Complete();
            return NoContent();
        }

    }
}
