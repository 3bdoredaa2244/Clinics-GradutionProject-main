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
    public class SymptomController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SymptomController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SymptomDTO>>> GetAll()
        {
            var data = await _unitOfWork.Symptom.GetAll();
            if (data == null || !data.Any())
                return NotFound();
            return Ok(_mapper.Map<IEnumerable<SymptomDTO>>(data));
        }

        // GET api/<ClinicController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Symptom>> GetById(int id)
        {
            var data = await _unitOfWork.Symptom.GetById(id);
            if (data == null)
                return NotFound();
            return Ok(_mapper.Map<SymptomDTO>(data));
        }

        // POST api/<ClinicController>
        [HttpPost]
        public async Task<ActionResult<Symptom>> AddClinic(SymptomDTO symptomDTO)
        {
            if (ModelState.IsValid)
            {
                var data = _mapper.Map<Symptom>(symptomDTO);
                await _unitOfWork.Symptom.Add(data);
                await _unitOfWork.Complete();

                return CreatedAtAction("GetbyId", new { data.Id }, data);
            }
            else

                return new JsonResult("Somethign Went wrong") { StatusCode = 500 };
        }

        // DELETE api/<ClinicController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClinic(int id)
        {
            var symptom = await _unitOfWork.Symptom.GetById(id);
            if (symptom == null)
                return NotFound();
            await _unitOfWork.Symptom.Delete(symptom);
            await _unitOfWork.Complete();
            return NoContent();
        }

    }
}
