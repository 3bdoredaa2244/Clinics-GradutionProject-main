using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.Core.DTOs
{
    public class ClinicDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string phoneNumber { get; set; }
        public string Location { get; set; }
        [Column(TypeName = "decimal(9,6)")]
        public decimal latitude { get; set; }
        [Column(TypeName = "decimal(9,6)")]
        public decimal longitude { get; set; }
    }
}
