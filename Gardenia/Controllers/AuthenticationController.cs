using Gardenia.DTOs;
using Gardenia.Helpers;
using Gardenia.Interfaces;
using Gardenia.Repositories;
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
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationRepository _repo;
        public AuthenticationController(IAuthenticationRepository repo)
        {
            _repo = repo;
        }


        [HttpPost]
        [Route("registerAdmin")]
       // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterAdmin([FromForm] RegisterAdminModel model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.RegisterAdminAsync(model, file));
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("loginAdmin")]
        public async Task<IActionResult> LoginAdmin(LoginAdminModel.Request model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.LoginAdminAsync(model));
            }
            return BadRequest();
        }


        [HttpPut]
        [Route("updateadmin/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAdmin(string id, [FromForm] EditAdminModel model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.UpdateAdmin(id, model, file));
            }
            return BadRequest();
        }


        [HttpDelete]
        [Route("deleteadmin/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAdmin(string id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.DeleteAdmin(id));
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("getadmins")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAdmins()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllAdmins());
            }
            return BadRequest();
        }


        [HttpPost]
        [Route("adduser")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddUsers(AddUserModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddUsers(model));
            }
            return BadRequest();
        }
        
        [HttpGet]
        [Route("getOwners")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetOwners()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetOwners());
            }
            return BadRequest();
        }


        [HttpPost]
        [Route("addlistofusers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddUsers(List<AddUserModel> models)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddUsers(models));
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("updateuser/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser(string id, [FromForm] AddUserModel model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.UpdateUser(id, model, file));
            }
            return BadRequest();
        }


        [HttpDelete]
        [Route("deleteuser/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.DeleteUser(id));
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("getusers")]
       // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllUsers());
            }
            return BadRequest();
        }
        

        [HttpGet]
        [Route("GetFollowersByOwnerId/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetFollowersByOwnerId(string id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetFollowersByOwnerId(id));
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("GetAllSecurityMembers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllSecurityMembers()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllSecurityMembers());
            }
            return BadRequest();
        }


        [HttpPost]
        [Route("adduserstounit")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddUsersToUnits(AddUserToUnitDTO model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddUsersToUnits(model));
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("getusersperunit/{id}")]
      //  [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsersPerUnit(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetUsersPerUnit(id));
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("getunitsforuser/{id}")]
        [Authorize(Roles = "User,Follower,Admin")]
        public async Task<IActionResult> GetUnitsForUser(string id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetUnitsForUser(id));
            }
            return BadRequest();
        }


        [HttpDelete]
        [Route("deleteuserfromunit")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUserFromUnit(AddUserToUnitModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.DeleteUserFromUnit(model));
            }
            return BadRequest();
        }

        //User Registeration 

        //Send verification code
        [HttpPost]
        [Route("sendcode")]
        public async Task<IActionResult> SendVerfyCode(SendCodeModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.SendVerfyCode(model));
            }
            return BadRequest();
        }
        // Get Gate Logs For Owners And Followers
        [HttpGet]
        [Route("GetGateLogs")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetGateLogs([FromQuery]string StartDate,[FromQuery] string EndDate, [FromQuery] string id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetGateLogs(StartDate, EndDate, id));
            }
            return BadRequest();
        }

        // Get Gate Logs For Visitors
        [HttpGet]
        [Route("GetVisitsLogs")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetVisitsLogs([FromQuery] string StartDate, [FromQuery] string EndDate)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetVisitsLogs(StartDate, EndDate));
            }
            return BadRequest();
        }

        //Recieve Verification Code
        [HttpPost]
        [Route("checkverifycode")]
        public async Task<IActionResult> RecieveVerficationCode(VerificationCodeModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.RecieveVerficationCode(model));
            }
            return BadRequest();
        }

        //Change Password
        [HttpPost]
        [Route("changepassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.ChangePassword(model));
            }
            return BadRequest();
        }

        //User Login
        [HttpPost]
        [Route("userlogin")]
        public async Task<IActionResult> UserLogin(LoginAdminModel.Request model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.UserLogin(model));
            }
            return BadRequest();
        }


        //Update User Image
        [HttpPost]
        [Route("updateuserimage")]
        [Authorize(Roles = "User,Follower,Admin")]
        public async Task<IActionResult> UpdateUserImage([FromForm] UpdateUserImage model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.UpdateUserImage(model, file));
            }
            return BadRequest();
        }


        [HttpPut]
        [Route("isactive/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> IsActive(string id, NewsStatus status)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.IsActive(id, status));
            }
            return BadRequest();
        }


        //User Identity Part
        [HttpPost]
        [Route("adduseridentity")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddUserIdentity(UserIdentityDTO model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddUserIdentity(model));
            }
            return BadRequest();
        }


        [HttpDelete]
        [Route("deleteuseridentity/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUserIdentity(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.DeleteUserIdentity(id));
            }
            return BadRequest();
        }



        [HttpPut]
        [Route("updateuseridentity/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUserIdentity(int id, UserIdentityDTO model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.UpdateUserIdentity(id, model));
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("getallidentities")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllIdentities()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllIdentities());
            }
            return BadRequest();
        }



        ////Security Gate
        //Generate QRCode
        [HttpGet]
        [Route("GetQRCode/{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetQRCode(string id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetQRCode(id));
            }
            return BadRequest();
        }


        //Scan QRCode
        [HttpPost]
        [Route("ScanQRCode")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> ScanQRCode(GetQRCode model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.ScanQRCode(model));
            }
            return BadRequest();
        }

        // User Search
        [HttpGet]
        [Route("UserSearch")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UserSearch([FromQuery]string subName)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.UserSearch(subName));
            }
            return BadRequest();
        }
        // User Search
        [HttpPost]
        [Route("loginvisitor")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> LoginVisitor(VisitorDTO model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.LoginVisitor(model));
            }
            return BadRequest();
        }

        // User Search
        [HttpPost]
        [Route("sendinvitation")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> SendInvitation(InvitationDTO model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.SendInvitation(model));
            }
            return BadRequest();
        }
        // User Search
        [HttpGet]
        [Route("getinvitationsfortoday")]
        [VisitrorAuthorize]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetInvitationsForToday()
        {
           if (ModelState.IsValid)
            {
                return Ok(await _repo.GetInvitationsForToday());
            }
            return BadRequest();
        }
        
        [HttpPost]
        [Route("ConfirmEnterance")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> ConfirmEnterance([FromForm]ConfirmEntranceDTO model,[FromForm] List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.ConfirmEnterance(model, files));
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("GetInvitationByVisitorId/{visitorId}")]
        [VisitrorAuthorize]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetInvitationByVisitorId(int visitorId)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetInvitationByVisitorId(visitorId));
            }
            return BadRequest();
        }
    }
}
