using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.Core.DTOs
{
    public class PostPatientDTO
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public string BloodType { get; set; }
        public byte[]? QRCode { get; set; }

    }
}
 