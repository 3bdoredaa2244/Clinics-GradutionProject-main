using Clinics.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.Core.DTOs
{
    public class FullRecipeReadDto
    {
        public int Id { get; set; }
        public string RecipeName { get; set; }
        public string RecipeDescription { get; set; }
        public string RecipeImage { get; set; }
       // public int RecipeAmont { get; set; }
       // public string IngredientName { get; set; }
       public IEnumerable<RecipeIngredientDto> recipeIngredients { get; set; }
    }
}
