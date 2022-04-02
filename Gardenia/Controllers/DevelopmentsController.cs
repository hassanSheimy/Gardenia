using Gardenia.DTOs;
using Gardenia.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevelopmentsController : ControllerBase
    {
        private readonly IDevelopmentRepository _repo;
        public DevelopmentsController(IDevelopmentRepository repo)
        {
            _repo = repo;
        }


        [HttpPost]
        [Route("adddevelopment")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddDevelopment(DevelopmentDTO achievement)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddDevelopment(achievement));
            }

            return BadRequest();
        }


        [HttpPut]
        [Route("updatedevelopment/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateDevelopment(DevelopmentDTO model, int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.UpdateDevelopment(id, model));
            }
            return BadRequest();
        }


        [HttpDelete]
        [Route("deletedevelopment/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteDevelopment(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.DeleteDevelopment(id));
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("getalldevelopments")]
        [Authorize(Roles = "User,Follower,Admin")]
        public async Task<IActionResult> GetAllDevelopments()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllDevelopments());
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("getDevelopmentsbycategory/{id}")]
        [Authorize(Roles = "User,Follower,Admin")]
        public async Task<IActionResult> GetDevelopmentsByCategory(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetDevelopmentByCategory(id));
            }
            return BadRequest();
        }


        [HttpPost]
        [Route("addcategory")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCategory(DevelopmentCategoryDTO model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddCategory(model));
            }

            return BadRequest();
        }


        [HttpGet]
        [Route("getcategories")]
        [Authorize(Roles = "User,Follower,Admin")]
        public async Task<IActionResult> GetCategories()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetCategories());
            }
            return BadRequest();
        }


        [HttpDelete]
        [Route("deletecategory/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.DeleteCategory(id));
            }
            return BadRequest();
        }


        [HttpPut]
        [Route("updatecategory/{id}")]
        [Authorize(Roles = UserRole.Admin)]
        public async Task<IActionResult> UpdateCategory(DevelopmentCategoryDTO model, int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.UpdateCategory(id, model));
            }
            return BadRequest();
        }
    }
}
