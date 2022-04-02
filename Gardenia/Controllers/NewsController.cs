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
    public class NewsController : ControllerBase
    {
        private readonly INewsRepository _repo;
        public NewsController(INewsRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        [Route("addnews")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddNews(NewsDTO model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddNews(model));
            }
            return BadRequest();
        }


        [HttpPost]
        [Route("addnewsimages/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddNewsImages(int id, IFormFile[] files)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddNewsImages(id, files));
            }
            return BadRequest();
        }


        [HttpDelete]
        [Route("deletenewsimage/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteNewsImage(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.DeleteNewsImage(id));
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("updatenews/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateNews(int id, NewsDTO model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.UpdateNews(id, model));
            }
            return BadRequest();
        }


        [HttpDelete]
        [Route("deletenews/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteNews(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.DeleteNews(id));
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("getallnews")]
       // [Authorize(Roles = "User,Follower,Admin")]
        public async Task<IActionResult> GetAllNews()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllNews());
            }
            return BadRequest();
        }


        [HttpPost]
        [Route("getallfavnews")]
        [Authorize(Roles = "User,Follower,Admin")]
        public async Task<IActionResult> GetAllFavNews(FavNewsDTO model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllFavNews(model));
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("isactive/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> IsActive(int id, NewsStatus status)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.IsActive(id, status));
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("islike/{id}")]
        [Authorize(Roles = "User,Follower,Admin")]
        public async Task<IActionResult> IsLike(int id, NewsStatus status)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.IsLike(id, status));
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("addcomment/{id}")]
        [Authorize(Roles = "User,Follower,Admin")]
        public async Task<IActionResult> AddComment(int id, RequestCommentDTO model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddComment(id, model));
            }
            return BadRequest();
        }


        [HttpDelete]
        [Route("deletecomment/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.DeleteComment(id));
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("getnewsbyid/{id}")]
        [Authorize(Roles = "User,Follower,Admin")]
        public async Task<IActionResult> GetNewsById(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetNewsById(id));
            }
            return BadRequest();
        }
    }
}
