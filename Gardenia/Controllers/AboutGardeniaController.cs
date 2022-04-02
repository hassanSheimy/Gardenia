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
    public class AboutGardeniaController : ControllerBase
    {
        private readonly IAboutGardeniaRepository _repo;
        public AboutGardeniaController(IAboutGardeniaRepository repo)
        {
            _repo = repo;
        }


        [HttpPost]
        [Route("addhistory/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAbout(int? id, AboutGardeniaDTO model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.UpdateAbout(id, model));
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("gethistory")]
       // [Authorize(Roles = "User,Follower,Admin")]
        public async Task<IActionResult> GetAllAbouts()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllAbout());
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("deleteabout/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAbout(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.DeleteAbout(id));
            }
            return BadRequest();
        }


        [HttpPost]
        [Route("addgardeniadata/{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddGardeniaData(int? id, [FromForm] GardeniaDataDTO model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.UpdateGardeniaData(id, model, file));
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("getgardeniadata")]
        [Authorize(Roles = "User,Follower,Admin")]
        public async Task<IActionResult> GetGardeniaData()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetGardeniaData());
            }
            return BadRequest();
        }
    }
}
