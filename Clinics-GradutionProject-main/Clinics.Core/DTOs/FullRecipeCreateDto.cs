using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.Core.DTOs
{
    public class FullRecipeCreateDto
    {
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public int Amount { get; set; }
    }
}
