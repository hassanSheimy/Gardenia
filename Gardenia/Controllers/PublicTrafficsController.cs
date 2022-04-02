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
    public class PublicTrafficsController : ControllerBase
    {
        private readonly IPublicTrafficRepository _repo;
        public PublicTrafficsController(IPublicTrafficRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        [Route("addcategory")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddPublicTraffic(PublicTrafficDTO model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddPublicTraffic(model));
            }

            return BadRequest();
        }

        [HttpDelete]
        [Route("deletecategory/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePublicTraffic(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.DeletePublicTraffic(id));
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("getallcategories")]
        [Authorize(Roles = "User,Follower,Admin")]
        public async Task<IActionResult> GetAllPublicTraffics()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllPublicTraffics());
            }
            return BadRequest();
        }


        [HttpPost]
        [Route("addtraffic/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddNormalTraffic(int id, [FromForm] NormalPublicTrafficDTO model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddNormalPublicTraffic(id, model, file));
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("getalltraffics/{id}")]
        [Authorize(Roles = "User,Follower,Admin")]
        public async Task<IActionResult> GetAllTrafficsById(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllNormalTrafficsById(id));
            }
            return BadRequest();
        }


        [HttpPut]
        [Route("updatetraffic/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateTraffic([FromForm] NormalPublicTrafficDTO model, IFormFile file, int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.UpdateNormalPublicTraffic(id, model, file));
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("deletetraffic/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTraffic(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.DeleteNormalPublicTraffic(id));
            }
            return BadRequest();
        }


        [HttpPost]
        [Route("addrate/{id}")]
        [Authorize(Roles = "User,Follower")]
        public async Task<IActionResult> AddRate(AddRateRequest rate, int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddRate(id, rate));
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("editrate/{id}")]
        [Authorize(Roles = "User,Follower")]
        public async Task<IActionResult> UpdateRate(int id, UpdateRateRequest model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.UpdateRate(id, model));
            }
            return BadRequest();
        }


        //Police
        [HttpPost]
        [Route("addpolice")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddPolice([FromForm] PoliceDTO model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddPolice(model, file));
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("updatepolice/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePoliceStation(int id, [FromForm] PoliceDTO model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.UpdatePoliceStation(id, model, file));
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("getallpolicestations")]
        [Authorize(Roles = "User,Follower,Admin")]
        public async Task<IActionResult> GetAllPoliceStations()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllPoliceStations());
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("addpolicerate/{id}")]
        [Authorize(Roles = "User,Follower")]
        public async Task<IActionResult> AddPoliceRate(AddRateRequest rate, int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddPoliceRate(id, rate));
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("editpolicerate/{id}")]
        [Authorize(Roles = "User,Follower")]
        public async Task<IActionResult> UpdatePoliceRate(int id, UpdateRateRequest model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.UpdatePoliceRate(id, model));
            }
            return BadRequest();
        }


        //Electricity
        [HttpPost]
        [Route("addelectricity")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddElectricity([FromForm] ElectricityDTO model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddElectricity(model, file));
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("updateelectricity/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateElectricity(int id, [FromForm] ElectricityDTO model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.UpdateElectricity(id, model, file));
            }
            return BadRequest();
        }


        [HttpPost]
        [Route("addelectricityrate/{id}")]
        [Authorize(Roles = "User,Follower")]
        public async Task<IActionResult> AddElectricityRate(AddRateRequest rate, int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddElectricityRate(id, rate));
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("editelectricityrate/{id}")]
        [Authorize(Roles = "User,Follower")]
        public async Task<IActionResult> UpdateElectricityRate(int id, UpdateRateRequest model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.UpdateElectricityRate(id, model));
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("getallelectricitystations")]
        [Authorize(Roles = "User,Follower,Admin")]
        public async Task<IActionResult> GetAllElectricityStations()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllElectricity());
            }
            return BadRequest();
        }
    }
}
