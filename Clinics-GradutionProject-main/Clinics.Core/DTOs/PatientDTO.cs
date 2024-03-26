using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.Core.DTOs
{
    public class PatientDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }        
        public string Address { get; set; }
        public string BloodType { get; set; }
        public byte[]? QRCode { get; set; }
    }
}
