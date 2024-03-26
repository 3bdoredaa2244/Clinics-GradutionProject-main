using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.Core.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }            
       // public ICollection<Recipe> Recipes { get; set; }
        public List<RecipeIngredient> RecipeIngredient { get; set; }
    }
}
