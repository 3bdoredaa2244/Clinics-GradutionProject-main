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
    public class DoctorScheduleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DoctorScheduleController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        [HttpGet("{SpecializationId}")]
        public async Task<IActionResult> GetDoctorSchedules(int SpecializationId, int page, int pageSize)
        {
            var schedules = await _unitOfWork.DoctorSchedule.GetDoctorSchedulesAsync(SpecializationId,page, pageSize);

            if (schedules == null)
            {
                return NotFound(); // Return 404 Not Found status code
            }

            return Ok(schedules);
        }

        [HttpPost]
        public async Task<IActionResult> AddDoctorSchedules(PostDoctorScheduleDTO postDoctorScheduleDTO)
        {
            var doctorSchedule = _mapper.Map<DoctorSchedule>(postDoctorScheduleDTO);

            await _unitOfWork.DoctorSchedule.Add(doctorSchedule);
            await _unitOfWork.Complete();

            if (doctorSchedule.Id > 0)
            {
                // Data saved successfully
                return Ok(doctorSchedule);
            }
            else
            {
                // Failed to save data
                return StatusCode(500); // You can use any appropriate status code here
            }
        }

    }
}
