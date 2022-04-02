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
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersRepository _repo;
        public OrdersController(IOrdersRepository repo)
        {
            _repo = repo;
        }

       
        
        //Orders
        [HttpPost]
        [Route("addorder/{id}")]
        [Authorize(Roles = "User,Follower")]
        public async Task<IActionResult> AddOrder(int id, OrderDTO model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddOrder(id, model));
            }
            return BadRequest();
        }


        [HttpPost]
        [Route("addorderimages/{id}")]
        [Authorize(Roles = "User,Follower")]
        public async Task<IActionResult> AddOrderImages(int id, [FromForm] IFormFile[] files)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddOrderImages(id, files));
            }
            return BadRequest();
        }


        [HttpPost]
        [Route("addorderresponse/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddOrderResponse(int id, OrderResponseDTO model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddOrderResponse(id, model));
            }
            return BadRequest();
        }


        [HttpPost]
        [Route("addorderresponseimages/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddOrderResponseImages(int id, [FromForm] IFormFile[] files)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddOrderResponseImages(id, files));
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("getallorders/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllOrders(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllOrders(id));
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("getorderbyid/{id}")]
        [Authorize(Roles = "User,Follower,Admin")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetOrderById(id));
            }
            return BadRequest();
        }


        
        
        //Report Type
        [HttpPost]
        [Route("addreporttype")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddReportType(ReportTypeDTO model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddReportType(model));
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("getallreporttypes")]
        [Authorize(Roles = "User,Follower,Admin")]
        public async Task<IActionResult> GetAllReportTypes()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllReportTypes());
            }
            return BadRequest();
        }


        [HttpDelete]
        [Route("deletereporttype/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteReportType(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.DeleteReportType(id));
            }
            return BadRequest();
        }


        [HttpPut]
        [Route("updatereporttype/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateReportType(ReportTypeDTO model, int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.UpdateReportType(id, model));
            }
            return BadRequest();
        }


        
        
        //Report
        [HttpPost]
        [Route("addreport")]
        [Authorize(Roles = "User,Follower")]
        public async Task<IActionResult> AddReport(ReportDTO model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddReport(model));
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("addreportimages/{id}")]
        [Authorize(Roles = "User,Follower")]
        public async Task<IActionResult> AddReportImages(int id, [FromForm] IFormFile[] files)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddReportImages(id, files));
            }
            return BadRequest();
        }


        [HttpPost]
        [Route("addreportresponse/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddReportResponse(int id, ReportResponseDTO model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddReportResponse(id, model));
            }
            return BadRequest();
        }


        [HttpPost]
        [Route("addreportresponseimages/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddReportResponseImages(int id, [FromForm] IFormFile[] files)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddReportResponseImages(id, files));
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("getallreports")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllReports()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllReports());
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("getreportbyid/{id}")]
        [Authorize(Roles = "User,Follower,Admin")]
        public async Task<IActionResult> GetReportById(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetReportById(id));
            }
            return BadRequest();
        }


        
        
        //Maintainance Type
        [HttpPost]
        [Route("AddMaintainanceType")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddMaintainanceType(MaintainanceTypeDTO model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddMaintainanceType(model));
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("GetAllMaintainanceTypes")]
        [Authorize(Roles = "User,Follower,Admin")]
        public async Task<IActionResult> GetAllMaintainanceTypes()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllMaintainanceTypes());
            }
            return BadRequest();
        }


        [HttpDelete]
        [Route("DeleteMaintainanceType/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMaintainanceType(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.DeleteMaintainanceType(id));
            }
            return BadRequest();
        }


        [HttpPut]
        [Route("UpdateMaintainanceType/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateMaintainanceType(MaintainanceTypeDTO model, int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.UpdateMaintainanceType(id, model));
            }
            return BadRequest();
        }


        
        
        //Maintainance
        [HttpPost]
        [Route("AddMaintainance/{id}")]
        [Authorize(Roles = "User,Follower")]
        public async Task<IActionResult> AddMaintainance(int id, MaintainanceDTO model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddMaintainance(id, model));
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("AddMaintainanceImages/{id}")]
        [Authorize(Roles = "User,Follower")]
        public async Task<IActionResult> AddMaintainanceImages(int id, [FromForm] IFormFile[] files)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddMaintainanceImages(id, files));
            }
            return BadRequest();
        }


        [HttpPost]
        [Route("AddMaintainanceResponse/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddMaintainanceResponse(int id, MaintainanceResponseDTO model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddMaintainanceResponse(id, model));
            }
            return BadRequest();
        }


        [HttpPost]
        [Route("AddMaintainanceResponseImages/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddMaintainanceResponseImages(int id, [FromForm] IFormFile[] files)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddMaintainanceResponseImages(id, files));
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("GetAllMaintainances")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllMaintainances()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllMaintainances());
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("GetMaintainanceById/{id}")]
        [Authorize(Roles = "User,Follower,Admin")]
        public async Task<IActionResult> GetMaintainanceById(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetMaintainanceById(id));
            }
            return BadRequest();
        }


        
        
        //My Contributories
        [HttpGet]
        [Route("getallordersforoneuser/{id}")]
        [Authorize(Roles = "User,Follower")]
        public async Task<IActionResult> GetAllOrdersForOneUser(string id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllOrdersForOneUser(id));
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("GetAllComplaintsForOneUser/{id}")]
        [Authorize(Roles = "User,Follower")]
        public async Task<IActionResult> GetAllComplaintsForOneUser(string id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllComplaintsForOneUser(id));
            }
            return BadRequest();
        }



        [HttpGet]
        [Route("GetAllSuggessionsForOneUser/{id}")]
        [Authorize(Roles = "User,Follower")]
        public async Task<IActionResult> GetAllSuggessionsForOneUser(string id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllSuggessionsForOneUser(id));
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("GetAllReportsForOneUser/{id}")]
        [Authorize(Roles = "User,Follower")]
        public async Task<IActionResult> GetAllReportsForOneUser(string id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllReportsForOneUser(id));
            }
            return BadRequest();
        }



        [HttpGet]
        [Route("GetAllMaintainancesForOneUser/{id}")]
        [Authorize(Roles = "User,Follower")]
        public async Task<IActionResult> GetAllMaintainancesForOneUser(string id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllMaintainancesForOneUser(id));
            }
            return BadRequest();
        }
    }
}
