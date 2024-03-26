using Clinics.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.Core.DTOs
{
    public class RecipeIngredientDto
    {

        public string name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public List<IngredientDto> ingredients { get; set; }
      //  public int Amount { get; set; }
        
        
    }
}
