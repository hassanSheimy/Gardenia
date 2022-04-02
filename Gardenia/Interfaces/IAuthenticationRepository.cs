using Gardenia.Data.Models;
using Gardenia.DTOs;
using Gardenia.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.Interfaces
{
    public interface IAuthenticationRepository
    {
        //Registeration Part
        Task<ApiResponse<RegisterAdminModel>> RegisterAdminAsync(RegisterAdminModel model, IFormFile file);
        Task<ApiResponse<LoginAdminModel.Response>> LoginAdminAsync(LoginAdminModel.Request request);
        Task<ApiResponse<EditAdminModel>> UpdateAdmin(string id, EditAdminModel model, IFormFile file);
        Task<ApiResponse> DeleteAdmin(string id);
        Task<ApiResponse<List<AddUserModel>>> GetAllAdmins();
        //Task<ApiResponse<AddUserModel>> AddUser(AddUserModel model, IFormFile file);
        Task<ApiResponse<AddUserModel>> AddUsers(AddUserModel model);

        //Users Part
        Task<ApiResponse<List<AddUserModel>>> AddUsers(List<AddUserModel> models);
        Task<ApiResponse<List<AddUserModel>>> GetOwners();
        Task<ApiResponse<AddUserModel>> UpdateUser(string id, AddUserModel model, IFormFile file);
        Task<ApiResponse> DeleteUser(string id);
        Task<ApiResponse<List<AddUserModel>>> GetAllUsers();
        Task<ApiResponse<List<AddUserModel>>> GetFollowersByOwnerId(string id);
        Task<ApiResponse<List<AppUser>>> GetAllSecurityMembers();
        Task<ApiResponse<AddUserModel>> GetUsersPerUnit(int id);
        Task<ApiResponse<List<UnitDTO>>> GetUnitsForUser(string id);
        Task<ApiResponse> DeleteUserFromUnit(AddUserToUnitModel model);
        Task<ApiResponse<bool>> IsActive(string id, NewsStatus model);
        Task<ApiResponse<AddUserToUnitDTO>> AddUsersToUnits(AddUserToUnitDTO model);


        //User Identity
        Task<ApiResponse<UserIdentityDTO>> AddUserIdentity(UserIdentityDTO model);
        Task<ApiResponse> DeleteUserIdentity(int id);
        Task<ApiResponse<UserIdentityDTO>> UpdateUserIdentity(int id, UserIdentityDTO model);
        Task<ApiResponse<List<UserIdentityDTO>>> GetAllIdentities();


        //User Registeration
        //Send Verification Code
        Task<ApiResponse> SendVerfyCode(SendCodeModel model);

        //Recieve Verification Code
        Task<ApiResponse> RecieveVerficationCode(VerificationCodeModel model);

        //Change Password
        Task<ApiResponse> ChangePassword(ChangePasswordModel model);

        //User Login
        Task<ApiResponse<LoginAdminModel.Response>> UserLogin(LoginAdminModel.Request request);

        //Update User Image
        Task<ApiResponse<UpdateUserImage>> UpdateUserImage(UpdateUserImage model, IFormFile file);


        //Security Part
        Task<ApiResponse<GetQRCode>> GetQRCode(string id);
        Task<ApiResponse<AppUser>> ScanQRCode(GetQRCode model);
        Task<ApiResponse<List<GateLogDTO>>> GetGateLogs(string StartDate, string EndDate, string UserID);
        Task<ApiResponse<List<VisitLogDTO>>> GetVisitsLogs(string StartDate, string EndDate);
        Task<ApiResponse<List<UserSearchDTO>>> UserSearch(string subSearch);

        // visitor
        Task<ApiResponse<VisitorDTO>> LoginVisitor(VisitorDTO model);

        // invitation
        Task<ApiResponse<InvitationDTO>> SendInvitation(InvitationDTO model);
        Task<ApiResponse<List<InvitationDTO>>> GetInvitation(string ownerId);
        Task<ApiResponse> DeleteInvitation(int id, int type);
        Task<ApiResponse<List<InvitationDTO>>> GetInvitationsForToday();
        Task<ApiResponse<ConfirmEntranceDTO>> ConfirmEnterance(ConfirmEntranceDTO model, List<IFormFile> files);
        Task<ApiResponse<List<InvitationDTO>>> GetInvitationByVisitorId(int VisitorId);
    }
}
