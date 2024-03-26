using Clinics.Core.DTOs;
using Clinics.Core.Interfaces;
using Clinics.Core.Models;
using Clinics.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.EF.Repositories
{
    public class ClinicRepository : GenericRepository<Clinic>, IClinic
    {
        protected ClinicContext _context;
        public ClinicRepository(ClinicContext context) : base(context)
        {
            _context = context;
        }

        public class DirectionsResponse
        {
            public string status { get; set; }
            public string error_message { get; set; }
            public DirectionsRoute[] routes { get; set; }
        }

        public class DirectionsRoute
        {
            public DirectionsLeg[] legs { get; set; }
        }

        public class DirectionsLeg
        {
            public Distance distance { get; set; }
        }

        public class Distance
        {
            public string text { get; set; }
            public int value { get; set; }
        }

        public async Task<decimal> GetDrivingDistanceAsync(decimal originLatitude, decimal originLongitude, decimal destinationLatitude, decimal destinationLongitude, string apiKey)
        {
            using (var httpClient = new HttpClient())
            {
                var url = $"https://maps.googleapis.com/maps/api/directions/json?origin={originLatitude},{originLongitude}&destination={destinationLatitude},{destinationLongitude}&key={apiKey}";
                var response = await httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error calling Google Maps API: {response.StatusCode} - {response.ReasonPhrase}");
                }

                var json = await response.Content.ReadAsStringAsync();
                var directionsResponse = JsonConvert.DeserializeObject<DirectionsResponse>(json);

                if (directionsResponse.status != "OK")
                {
                    throw new Exception($"Error calling Google Maps API2: {directionsResponse.status} - {directionsResponse.error_message}");
                }

                var distanceInMeters = directionsResponse.routes[0].legs[0].distance.value;
                var distanceInKilometers = distanceInMeters / 1000.0m;
                return distanceInKilometers;
            }
        }

        public async Task<List<Clinic>> GetClinics(int specializationId)
        {
            var clinics = await _context.Clinics
             .Where(c => c.Doctors.Any(d => d.SpecializationId == specializationId))
             .Distinct()
             .ToListAsync();
             
                if (clinics == null)
            {
                return null;
            }
            return clinics;
        }
    }
}
