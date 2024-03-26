using Clinics.Core.DTOs;
using Clinics.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.Core.Interfaces
{
    public interface IReservation :IGenericRepository<Reservation>
    {
        Task<IEnumerable<ReservationDTO>> GetReservation(string id);
        Task<IEnumerable<ReservationDTO>> GetReservations();
        Task<PostReservationDTO> AddReservation(PostReservationDTO postReservationDTO);
    }
}
