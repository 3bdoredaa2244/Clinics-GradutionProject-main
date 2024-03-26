using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.Core.Models
{
    public class Medication
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("MedicalRecord")]
        public int MedicalRecordID { get; set; }
        public MedicalRecord MedicalRecord { get; set; }
    }
}
