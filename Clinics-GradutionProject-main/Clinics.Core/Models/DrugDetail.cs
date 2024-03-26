using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.Core.Models
{
    public class DrugDetail
    {
        public int id { get; set; }
        public string DrugName { get; set; }
        public int Amount { get; set; }
        public string Route { get; set; }
        public string Frequency { get; set; }

        [ForeignKey("Prescription")]
        public int PrescriptionId { get; set; }

        public Prescription Prescription { get; set; }
    }
}
