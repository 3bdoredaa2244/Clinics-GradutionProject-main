﻿using Clinics.Core.DTOs;
using Clinics.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.Core.Interfaces
{
    public interface IRecipe :IGenericRepository<Recipe>
    {
       Task<List<RecipeIngredientDto>> GetRecipes();
        Task<RecipeIngredientDto> AddRecipe(RecipeIngredientDto recipeIngredientDto);

    }
}
