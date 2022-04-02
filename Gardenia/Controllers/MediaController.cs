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
    public class MediaController : ControllerBase
    {
        private readonly IMediaRepository _repo;
        public MediaController(IMediaRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        [Route("addimage")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddImage([FromForm] ImagesDTO model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddImage(model, file));
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("getimages")]
        [Authorize(Roles = "User,Follower,Admin")]
        public async Task<IActionResult> GetAllImages()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllImages());
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("updateimage/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateImage([FromForm] ImagesDTO model, IFormFile file, int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.UpdateImage(id, model, file));
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("deleteimage/{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.DeleteImage(id));
            }
            return BadRequest();
        }


        [HttpPost]
        [Route("addvideo")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddVideo([FromForm] VideoDTO model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddVideo(model, file));
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("updatevideo/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateVideo([FromForm] VideoDTO model, IFormFile file, int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.UpdateVideo(id, model, file));
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("getvideos")]
      //  [Authorize(Roles = "User,Follower,Admin")]
        public async Task<IActionResult> GetAllVideos()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllVideos());
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("deletevideo/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteVideo(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.DeleteVideo(id));
            }
            return BadRequest();
        }
    }
}
