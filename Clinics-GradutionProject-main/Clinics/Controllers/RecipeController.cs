using AutoMapper;
using Clinics.Core;
using Clinics.Core.DTOs;
using Clinics.Core.Interfaces;
using Clinics.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Clinics.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RecipeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        // GET: api/<Recipe>
        [Authorize]
        [HttpGet]

        public async Task<ActionResult<List<RecipeIngredientDto>>> GetAll()
        {
            var recipes = await _unitOfWork.Recipe.GetRecipes();
            // distnation <-- src

            return Ok(recipes);
        }

        // GET api/<Recipe>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeReadDto>> GetbyId(int id)
        {
            var recipe = await _unitOfWork.Recipe.GetById(id);
            if (recipe == null)
                return NotFound();
            return Ok(_mapper.Map<RecipeReadDto>(recipe));
        }

        [HttpPost("createFullRecipe")]
        public async Task<ActionResult<RecipeIngredientDto>> Create(RecipeIngredientDto recipeIngredientDto)
        {
            var recipe = await _unitOfWork.Recipe.AddRecipe(recipeIngredientDto);

            return Ok(recipeIngredientDto);

        }
        // POST api/<Recipe>
        [HttpPost]
        public async Task<ActionResult<RecipeReadDto>> Create(RecipeReadDto recipeReadDto)
        {
            if (ModelState.IsValid)
            {
                var data = _mapper.Map<Recipe>(recipeReadDto);
                await _unitOfWork.Recipe.Add(data);
                await _unitOfWork.Complete();

                return CreatedAtAction("GetbyId", new { data.Id }, data);
            }
            else

                return new JsonResult("Somethign Went wrong") { StatusCode = 500 };

        }


        // PUT api/<Recipe>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Recipe>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
