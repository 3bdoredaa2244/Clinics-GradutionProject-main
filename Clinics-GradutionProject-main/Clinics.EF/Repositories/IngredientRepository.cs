using Clinics.Core.Interfaces;
using Clinics.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.EF.Repositories
{
    public class IngredientRepository: GenericRepository<Core.Models.Ingredient>,IIngredient
    {
        protected ClinicContext _context;
        public IngredientRepository(ClinicContext context) : base(context)
        {
            _context = context;
        }

    }
}
