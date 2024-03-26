using Clinics.Core;
using Clinics.Core.Interfaces;
using Clinics.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Any;
using Clinics.Core.DTOs;
using AutoMapper;
using SendGrid.Helpers.Mail;
using Microsoft.AspNetCore.JsonPatch;

namespace Clinics.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PostsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<postReadDto>>> Get()
        {
            var posts = await _unitOfWork.Post.GetAll();
            // distnation <-- src
            var data = _mapper.Map<IEnumerable<postReadDto>>(posts);
            return Ok(data);
        }
         
        [HttpGet("{id}")]
        public async Task<ActionResult<postReadDto>> GetbyId(int id)
        {
            var post = await _unitOfWork.Post.GetById(id);
            if (post == null)
                return NotFound();
            return Ok(_mapper.Map<postReadDto>(post));
        }

        [HttpPost]
        public async Task<ActionResult<postReadDto>> Create(Posts post)
        {
            if (ModelState.IsValid)
            { 
                
                await _unitOfWork.Post.Add(post);
                await _unitOfWork.Complete();
               // var data = _mapper.Map <postCreateDto>(posts);
               // return Ok(data);
                return CreatedAtAction("GetbyId", new { post.id }, post);               
            }
            else
                
            return new JsonResult("Somethign Went wrong") { StatusCode = 500 };
        }

        [HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{

        //    var item = await _unitOfWork.Post.GetById(id);

        //    if (item == null)
        //        return BadRequest();

        //    await _unitOfWork.Post.Delete(id);
        //    await _unitOfWork.Complete();

        //    return Ok(item);


        //}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, postReadDto postsDTO)
        {
            var data = await _unitOfWork.Post.GetById(id);
            if (data == null)
                return NotFound();
            //src -> distnation
            var note = _mapper.Map(postsDTO, data);
            //_unitOfWork.Post.Update(note);
            await _unitOfWork.Complete();

            // return Ok(data);
            return NoContent();
        }

        //Patch api/posts/{id}
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialUpdatae(int id,JsonPatchDocument<postReadDto> patchDoc)
        {
            var data = await _unitOfWork.Post.GetById(id);
            if (data == null)
                return NotFound();
            var postToPatch = _mapper.Map<postReadDto>(data);

            patchDoc.ApplyTo(postToPatch, ModelState);

            if (!TryValidateModel(postToPatch))
                return ValidationProblem(ModelState);
            //src -> distnation 
            //the updater is being done here 
            _mapper.Map(postToPatch, data);

            await _unitOfWork.Complete();
          
            return NoContent();
        }
         
    }
}
