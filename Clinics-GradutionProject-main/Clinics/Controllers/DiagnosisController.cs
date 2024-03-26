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
    public class DiagnosisController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DiagnosisController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiagnosisDTO>>> GetAll()
        {
            var data = await _unitOfWork.Diagnosis.GetAll();
            if (data == null || !data.Any())
                return NotFound();
            return Ok(_mapper.Map<IEnumerable<DiagnosisDTO>>(data));
        }

        // GET api/<ClinicController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Diagnosis>> GetById(int id)
        {
            var data = await _unitOfWork.Diagnosis.GetById(id);
            if (data == null)
                return NotFound();
            return Ok(_mapper.Map<DiagnosisDTO>(data));
        }

        // POST api/<ClinicController>
        [HttpPost]
        public async Task<ActionResult<Diagnosis>> AddClinic(DiagnosisDTO diagnosisDTO)
        {
            if (ModelState.IsValid)
            {
                var data = _mapper.Map<Diagnosis>(diagnosisDTO);
                await _unitOfWork.Diagnosis.Add(data);
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
            var diagnosis = await _unitOfWork.Diagnosis.GetById(id);
            if (diagnosis == null)
                return NotFound();
            await _unitOfWork.Diagnosis.Delete(diagnosis);
            await _unitOfWork.Complete();
            return NoContent();
        }
    }
}
