using AutoMapper;
using Clinics.Core;
using Clinics.Core.DTOs;
using Clinics.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace Clinics.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    

    public class IngredientController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<IngredientController> _logger;
       // private readonly AuthRepository _authRepository;
        public IngredientController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<IngredientController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
           // _logger = logger;
           // _authRepository = authRepository;
        }

        //[Authorize(Policy = "RequireAuth")]
        [Authorize]
        [HttpGet]        
        public async Task<ActionResult<IngredientDto>> GetbyId(int id)
        {
                var data = await _unitOfWork.Ingredient.GetById(id);
            if (data == null)
                return NotFound();
            return Ok(_mapper.Map<IngredientDto>(data));
           
           
        }

        [HttpPost]
        public async Task<ActionResult<Ingredient>> Create(IngredientDto ingredientReadDto)
        {
            if (ModelState.IsValid)
            {
                var data = _mapper.Map<Ingredient>(ingredientReadDto);
                await _unitOfWork.Ingredient.Add(data);
                await _unitOfWork.Complete();

                return CreatedAtAction("GetbyId", new { data.Id }, data);
            }
            else

                return new JsonResult("Somethign Went wrong") { StatusCode = 500 };

        }
        [HttpPost("CreateFullRecipe")]
        public async Task<ActionResult<Ingredient>> CreateFullRecipe(FullRecipeCreateDto fullRecipeCreateDto)
        {
            if (ModelState.IsValid)
            {
                var data = _mapper.Map<Ingredient>(fullRecipeCreateDto);
                await _unitOfWork.Ingredient.Add(data);
                await _unitOfWork.Complete();

                return CreatedAtAction("GetbyId", new { data.Id }, data);
            }
            else

                return new JsonResult("Somethign Went wrong") { StatusCode = 500 };

        }
        [HttpGet("hi")]
        [Authorize]
        public async Task<IActionResult> test(string Greeting)
        {

            return Ok("hi");
        }

    }
}
