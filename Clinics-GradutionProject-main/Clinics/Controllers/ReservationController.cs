using AutoMapper;
using Clinics.Core;
using Clinics.Core.DTOs;
using Clinics.Core.Models;
using Clinics.Core.Models.Authentication;
using Clinics.Data;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;

namespace Clinics.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHubContext<NotificationHub> _hubContext;

        public ReservationController(IUnitOfWork unitOfWork, IMapper mapper, IHubContext<NotificationHub> hubContext, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hubContext = hubContext;
            _userManger = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetAll()
        {
            var data = await _unitOfWork.Reservation.GetReservations();
            if (data == null || !data.Any())
                return NotFound();
            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservation(string id)
        {
            var data = await _unitOfWork.Reservation.GetReservation(id);
            if (data == null)
                return NotFound();
            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<PostReservationDTO>> AddReservation(PostReservationDTO postReservationDTO)
        {
            if (postReservationDTO == null)
                return BadRequest();

            await _unitOfWork.Reservation.AddReservation(postReservationDTO);
            await _unitOfWork.Complete();

            DateTime utcDate = postReservationDTO.Date.ToUniversalTime();
            DateTime notificationDate = utcDate.AddMinutes(-1);
            // Bad practice
            // await Task.Delay(notificationDate - DateTime.UtcNow);
            // Schedule the notification using Hangfire
            var patient = await _userManger.FindByIdAsync(postReservationDTO.PatientID);
            string patientEmail = patient.Email;

            var doctor = await _userManger.FindByIdAsync(postReservationDTO.DoctorId);
            string doctorEmail = doctor.Email;

            string recipientEmail = patientEmail; // Replace with the recipient's email address
            string subject = "Reservation";
            string body = "Your email reservation is booked";
            Console.WriteLine($"******Loooooooooooooooook here   {patientEmail}  **********");
            SendEmail(recipientEmail, subject, body);
            // postReservationDTO.DoctorId
            BackgroundJob.Schedule(() => SendNotification(postReservationDTO), notificationDate - DateTime.UtcNow);


            return CreatedAtAction(nameof(GetReservation), new { id = postReservationDTO.id }, postReservationDTO);
        }



        private void SendEmail(string recipientEmail, string subject, string body)
        {
            string senderEmail = "m01093777329@gmail.com"; // Replace with your Gmail address
            string senderPassword = "liotqidsipvktbec"; // Replace with your Gmail password

            var smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                EnableSsl = true
            };

            MailMessage mailMessage = new MailMessage(senderEmail, recipientEmail, subject, body);

            try
            {
                smtpClient.Send(mailMessage);
                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to send email: " + ex.Message);
            }
        }



        [HttpPost("Notification-Hangfire")]
        public async Task<IActionResult> SendNotification(PostReservationDTO postReservationDTO)
        {
            Console.WriteLine("is it working ??????");
            // Perform the notification logic here
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", $"The meeting with ID {postReservationDTO.PatientID} will start on {postReservationDTO.Date.ToString("yyyy-MM-dd hh:mm:ss tt")}.", postReservationDTO.PatientID, postReservationDTO.DoctorId);
            return Ok();
        }
    }
}
