﻿using Clinics.Core.Interfaces;
using Clinics.Core.Models;
using Clinics.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.EF.Repositories
{
    public class SymptomRepository : GenericRepository<Symptom>, ISymptom
    {
        protected ClinicContext _context;
        public SymptomRepository(ClinicContext context) : base(context)
        {
            _context = context;
        }
    
    }
}
