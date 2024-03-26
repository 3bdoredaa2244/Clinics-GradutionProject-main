using Clinics.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Clinics.Core.Interfaces
{
    public interface IClinic : IGenericRepository<Clinic>
    {
        Task<decimal> GetDrivingDistanceAsync(decimal originLatitude, decimal originLongitude, decimal destinationLatitude, decimal destinationLongitude, string apiKey);
        public Task<List<Clinic>> GetClinics(int specializationId);
        // Task<Clinic> FindClosestClinics(decimal userLatitude, decimal userLongitude, int numberOfClinicsToReturn, string apiKey);
    }
}
