using AutoMapper;
using Clinics.Core;
using Clinics.Core.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinics.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PrescriptionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        [HttpGet("patient/{id}/prescriptions")]
        public async Task<ActionResult<List<PrescriptionWithDrugDetailsDto>>> GetPrescription(string id, DateTime date)
        {
            var data = await _unitOfWork.Prescription.GetPrescriptionsByPatientId(id,date);
            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<PrescriptionWithDrugDetailsDto>> AddPrescription(PrescriptionWithDrugDetailsDto prescriptionDto)
        {
            if (prescriptionDto == null)
            {
                return BadRequest();
            }

            await _unitOfWork.Prescription.AddPrescription(prescriptionDto);
            await _unitOfWork.Complete();

            return prescriptionDto;
        }


    }
}
