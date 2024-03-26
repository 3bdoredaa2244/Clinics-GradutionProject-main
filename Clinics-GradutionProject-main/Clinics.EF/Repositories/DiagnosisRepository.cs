using Clinics.Core.Interfaces;
using Clinics.Core.Models;
using Clinics.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.EF.Repositories
{
    public class DiagnosisRepository : GenericRepository<Diagnosis>, IDiagnosis
    {
        protected ClinicContext _context;
        public DiagnosisRepository(ClinicContext context) : base(context)
        {
            _context = context;
        }

    }
}
