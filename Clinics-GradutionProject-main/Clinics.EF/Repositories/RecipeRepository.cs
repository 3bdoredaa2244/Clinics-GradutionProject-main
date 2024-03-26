using Clinics.Core.Interfaces;
using Clinics.Core.Models;
using Clinics.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinics.Core.DTOs;

namespace Clinics.EF.Repositories
{
    public class RecipeRepository : GenericRepository<Core.Models.Recipe>, IRecipe
    {
        protected ClinicContext _context;
        public RecipeRepository(ClinicContext context) : base(context)
        {
            _context = context;
        }

        async Task<RecipeIngredientDto> IRecipe.AddRecipe(RecipeIngredientDto recipeIngredientDto)
        {
            var recipe = new Recipe
            {
                Name = recipeIngredientDto.name,
                Description = recipeIngredientDto.Description,
                ImagePath = recipeIngredientDto.ImagePath
            };
            recipe.RecipeIngredient = new List<RecipeIngredient>();
            foreach (var ingredientDto in recipeIngredientDto.ingredients)
            {
                var ingredient = new Ingredient
                {
                    Name = ingredientDto.Name
                };

                var recipeIngredient = new RecipeIngredient
                {
                    Recipe = recipe,
                    Ingredient = ingredient,
                    Amount = ingredientDto.Amount
                };
               
                recipe.RecipeIngredient.Add(recipeIngredient);
            }
              _context.Recipes.Add(recipe);
              await _context.SaveChangesAsync();
             return recipeIngredientDto;
        }
        
        
        public async Task<List<RecipeIngredientDto>> GetRecipes()
        {
         #region FullRecipe
            #region using LINQ
            //var recipes = (from r in _context.Recipes
            //              join ir in _context.RecipeIngredients
            //              on r.Id equals ir.RecipeId
            //              join i in _context.Ingredients
            //              on ir.IngredientId equals i.Id
            //              select new FullRecipeReadDto 
            //              {
            //                  Id = r.Id,
            //                 RecipeName = r.Name,
            //                 RecipeDescription = r.Description,
            //                 RecipeImage = r.ImagePath,
            //                 RecipeAmont = ir.Amount,
            //                 IngredientName =i.Name
            //               }).ToListAsync();
            #endregion
            var recipes = await _context.Recipes.
                Include(ri => ri.RecipeIngredient).
                ThenInclude(i => i.Ingredient).ToListAsync();
            
            var recipesDetail = recipes.Select(R => new RecipeIngredientDto
                {
                    name = R.Name,
                    Description = R.Description,
                    ImagePath = R.ImagePath,
                    ingredients = R.RecipeIngredient
                   .Select(ri => new IngredientDto
                   {
                       Name = ri.Ingredient.Name,
                       Amount = ri.Amount,
                   }).ToList()
                    }).ToList();

            return recipesDetail;
        }
        #endregion
    }
}
