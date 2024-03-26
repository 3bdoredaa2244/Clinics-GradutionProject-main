using Clinics.Core.Interfaces;
using Clinics.Core.Models;
using Clinics.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.EF.Repositories
{
    public class SpecializationRepository : GenericRepository<Specialization>, ISpecialization
    {
        protected ClinicContext _context;
        public SpecializationRepository(ClinicContext context) : base(context)
        {
            _context = context;
        }
    }
}
