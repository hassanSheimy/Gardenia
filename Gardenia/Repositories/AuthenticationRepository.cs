using AutoMapper;
using Gardenia.Data.DataAccess;
using Gardenia.Data.Models;
using Gardenia.DTOs;
using Gardenia.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IBasicServices _services;
        private readonly ISmsService _smsServices;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;


        public AuthenticationRepository(IMapper mapper, AppDbContext context, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IBasicServices services, ISmsService smsServices)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _services = services;
            _smsServices = smsServices;
            _context = context;
            _mapper = mapper;
        }

        //Admin Registeration
        public async Task<ApiResponse<RegisterAdminModel>> RegisterAdminAsync(RegisterAdminModel model, IFormFile file)
        {
            var responseModel = new ApiResponse<RegisterAdminModel>();

            //Check if user exist
            if (await _userManager.FindByEmailAsync(model.Email) != null)
            {
                responseModel.Status = false;
                responseModel.Message = "This user is already exist";
                return responseModel;
            }

            //Get Admin counter which is used in UserName creation
            var compound = _context.Compounds.OrderByDescending(c => c.AdminCounter).First();
            AppUser user = new()
            {
                Name = model.Name,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "Gard" + ++compound.AdminCounter,
                PhoneNumber = model.PhoneNumber,
                Image = await _services.UploadPhoto(file)
            };

            if (file != null)
            {
                user.Image = await _services.UploadPhoto(file);
            }


            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description},";
                }
                responseModel.Message = errors;
                return responseModel;
            }

            //Add Role
            await _userManager.AddToRoleAsync(user, "Admin");

            model.Id = user.Id;
            model.UserName = user.UserName;
            model.Image = user.Image;

            //Send SMS with username and password
            var smsRequest = new SmsRequest()
            {
                ToPhoneNumber = model.PhoneNumber,
                Body = $"بيانات الدخول علي حسابكم هي" + "\n" + "\n" + model.UserName + "\n" + model.Password
            };

            await _smsServices.SendSmsAsync(smsRequest);

            responseModel.Status = true;
            responseModel.Message = "Success, User is successfully created";
            responseModel.Response = model;

            return responseModel;
        }


        //Update Admin
        public async Task<ApiResponse<EditAdminModel>> UpdateAdmin(string id, EditAdminModel model, IFormFile file)
        {
            var responseModel = new ApiResponse<EditAdminModel>();

            //Get Admin from DB
            var userInDb = await _userManager.FindByIdAsync(id);

            //Check if Admin is exist or not
            if (userInDb == null)
            {
                responseModel.Status = false;
                responseModel.Message = "This Admin is not exist";

                return responseModel;
            }

            model.Id = id;
            model.UserName = userInDb.UserName;
            if (file != null)
                model.Image = await _services.UploadPhoto(file);
            else
                model.Image = userInDb.Image;


            _mapper.Map(model, userInDb);

            //Create Admin
            var result = await _userManager.UpdateAsync(userInDb);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description},";
                }
                responseModel.Message = errors;
                return responseModel;
            }

            responseModel.Status = true;
            responseModel.Message = "Success, Admin is successfully updated";
            responseModel.Response = model;

            return responseModel;
        }

        //Delete Admin 
        public async Task<ApiResponse> DeleteAdmin(string id)
        {
            var responseModel = new ApiResponse();

            //Get Admin from DB
            var user = await _userManager.FindByIdAsync(id);

            //Check if Admin is exist or not
            if (user == null)
            {
                responseModel.Status = false;
                responseModel.Message = "This Admin is not exist";

                return responseModel;
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description},";
                }
                responseModel.Message = errors;
                return responseModel;
            }

            responseModel.Status = true;
            responseModel.Message = "Success, Admin is successfully updated";

            return responseModel;
        }

        //Login Admin
        public async Task<ApiResponse<LoginAdminModel.Response>> LoginAdminAsync(LoginAdminModel.Request request)
        {
            var responseModel = new ApiResponse<LoginAdminModel.Response>();

            AppUser user;

            if (request.UserNameOrEmail.Contains("@"))
            {
                user = await _userManager.FindByEmailAsync(request.UserNameOrEmail);
            }
            else
            {
                user = await _userManager.FindByNameAsync(request.UserNameOrEmail);
            }

            if (user == null)
            {
                responseModel.Status = false;
                responseModel.Message = "This user is not exist";
                return responseModel;
            }

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);
            var jwtSecurityToken = await _services.GenerateJwt(user);
            if (result.Succeeded)
            {
                responseModel.Response = new LoginAdminModel.Response()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Image = user.Image,
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
                };
                responseModel.Status = true;
                responseModel.Message = "Success";
                return responseModel;
            }
            else
            {
                responseModel.Status = false;
                responseModel.Message = "failure during signning in";
                return responseModel;
            }
        }

        //Get all Admins
        public async Task<ApiResponse<List<AddUserModel>>> GetAllAdmins()
        {
            var admins = await _userManager.GetUsersInRoleAsync("Admin");
            var response = new ApiResponse<List<AddUserModel>>();

            if (admins == null)
            {
                response.Status = false;
                response.Message = "There is no Admins yet!";

                return response;
            }

            var adminsDTO = admins.Select(_mapper.Map<AppUser, AddUserModel>).ToList();
            response.Status = true;
            response.Message = "Success";
            response.Response = adminsDTO;

            return response;
        }

        //Add User
        //public async Task<ApiResponse<AddUserModel>> AddUser(AddUserModel model, IFormFile file)
        //{
        //    var responseModel = new ApiResponse<AddUserModel>();

        //    //Check if user exist
        //    if (await _userManager.FindByEmailAsync(model.Email) != null)
        //    {
        //        responseModel.Status = false;
        //        responseModel.Message = "This user is already exist";
        //        return responseModel;
        //    }

        //    AppUser user = new()
        //    {
        //        Name = model.Name,
        //        Email = model.Email,
        //        //UnitNumber = model.UnitNumber,
        //        SecurityStamp = Guid.NewGuid().ToString(),
        //        UserName = model.PhoneNumber,
        //        PhoneNumber = model.PhoneNumber,
        //        Image = await _services.UploadPhoto(file)
        //    };

        //    //Create User
        //    var result = await _userManager.CreateAsync(user, model.Password);
        //    if (!result.Succeeded)
        //    {
        //        var errors = string.Empty;
        //        foreach (var error in result.Errors)
        //        {
        //            errors += $"{error.Description},";
        //        }
        //        responseModel.Message = errors;
        //        return responseModel;
        //    }

        //    await _userManager.AddToRoleAsync(user, "User");

        //    model.Id = user.Id;
        //    model.Image = user.Image;
        //    model.UserName = user.UserName;

        //    responseModel.Status = true;
        //    responseModel.Message = "Success, User is successfully created";
        //    responseModel.Response = model;

        //    return responseModel;
        //}


        // add just one user
        public async Task<ApiResponse<AddUserModel>> AddUsers(AddUserModel model)
        {
            var responseModel = new ApiResponse<AddUserModel>();
            //Check if user exist
            if (await _userManager.FindByEmailAsync(model.Email) != null)
            {
                responseModel.Status = false;
                responseModel.Message = String.Format("This user {0} is already exist!", model.PhoneNumber);
                return responseModel;
            }
            var compound = await _context.Compounds.FirstOrDefaultAsync();
            AppUser user = new()
            {
                Name = model.Name,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.PhoneNumber,
                PhoneNumber = model.PhoneNumber,
                NationalID = model.NationalID,
                IsActive = true,
                Type = model.Type,
                SecurityMemberID = (model.Type == 3) ? ++compound.SecurityMemberIDCounter : null,
                UserIdentityID = model.UserIdentityID
            };
            //Create User
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description},";
                }
                responseModel.Message = errors;
                return responseModel;
            }
            model.Id = user.Id;
            model.UserName = user.UserName;

            // 1 = owner
            if (user.Type == 1)
            {
                user.OwnerId = null;
                await _userManager.UpdateAsync(user);
                await _userManager.AddToRoleAsync(user, "User");
            }
            // 2 follower
            else if (user.Type == 2)
            {
                user.OwnerId = model.OwnerId;
                await _userManager.UpdateAsync(user);
                await _userManager.AddToRoleAsync(user, "Follower");
            }
            else if (user.Type == 3)
            {
                user.OwnerId = null;
                await _userManager.UpdateAsync(user);
                await _userManager.AddToRoleAsync(user, "Security");
            }

            responseModel.Status = true;
            responseModel.Message = "Success, Users is successfully created";
            responseModel.Response = model;

            return responseModel;
        }
        
        public async Task<ApiResponse<List<AddUserModel>>> AddUsers(List<AddUserModel> models)
        {
            var responseModel = new ApiResponse<List<AddUserModel>>();
            foreach (var model in models)
            {
                //Check if user exist
                if (await _userManager.FindByEmailAsync(model.Email) != null)
                {
                    responseModel.Status = false;
                    responseModel.Message = String.Format("This user {0} is already exist!", model.PhoneNumber);
                    return responseModel;
                }
                var compound = await _context.Compounds.FirstOrDefaultAsync();
                AppUser user = new()
                {
                    Name = model.Name,
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.PhoneNumber,
                    PhoneNumber = model.PhoneNumber,
                    NationalID = model.NationalID,
                    IsActive = true,
                    Type = model.Type,
                    SecurityMemberID = (model.Type == 3) ? ++compound.SecurityMemberIDCounter : null,
                    UserIdentityID = model.UserIdentityID
                };

                //Create User
                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    var errors = string.Empty;
                    foreach (var error in result.Errors)
                    {
                        errors += $"{error.Description},";
                    }
                    responseModel.Message = errors;
                    return responseModel;
                }
                model.Id = user.Id;
                model.UserName = user.UserName;

                // 1 = owner
                if (user.Type == 1)
                    await _userManager.AddToRoleAsync(user, "User");
                // 2 follower
                else if (user.Type == 2)
                    await _userManager.AddToRoleAsync(user, "Follower");
                else if (user.Type == 3)
                    await _userManager.AddToRoleAsync(user, "Security");
            }
            responseModel.Status = true;
            responseModel.Message = "Success, Users is successfully created";
            responseModel.Response = models;

            return responseModel;
        }

        #region Old AddUser
        //public async Task<ApiResponse<List<AddUserModel>>> AddUsers(List<AddUserModel> models)
        //{
        //    var responseModel = new ApiResponse<List<AddUserModel>>();
        //    foreach (var model in models)
        //    {
        //        //Check if user exist
        //        if (await _userManager.FindByEmailAsync(model.Email) != null)
        //        {
        //            responseModel.Status = false;
        //            responseModel.Message = String.Format("This user {0} is already exist!", model.PhoneNumber);
        //            return responseModel;
        //        }
        //        var compound = await _context.Compounds.FirstOrDefaultAsync();
        //        AppUser user = new()
        //        {
        //            Name = model.Name,
        //            Email = model.Email,
        //            SecurityStamp = Guid.NewGuid().ToString(),
        //            UserName = model.PhoneNumber,
        //            PhoneNumber = model.PhoneNumber,
        //            NationalID = model.NationalID,
        //            IsActive = true,
        //            Type = model.Type,
        //            SecurityMemberID = (model.Type == 3) ? ++compound.SecurityMemberIDCounter : null,
        //            UserIdentityID = model.UserIdentityID
        //        };

        //        //Create User
        //        var result = await _userManager.CreateAsync(user, model.Password);
        //        if (!result.Succeeded)
        //        {
        //            var errors = string.Empty;
        //            foreach (var error in result.Errors)
        //            {
        //                errors += $"{error.Description},";
        //            }
        //            responseModel.Message = errors;
        //            return responseModel;
        //        }
        //        model.Id = user.Id;
        //        model.UserName = user.UserName;

        //        // 1 = owner
        //        if (user.Type == 1)
        //            await _userManager.AddToRoleAsync(user, "User");
        //        // 2 follower
        //        else if (user.Type == 2)
        //            await _userManager.AddToRoleAsync(user, "Follower");
        //        else if (user.Type == 3)
        //            await _userManager.AddToRoleAsync(user, "Security");
        //    }

        //    responseModel.Status = true;
        //    responseModel.Message = "Success, Users is successfully created";
        //    responseModel.Response = models;

        //    return responseModel;
        //}

        #endregion


        public async Task<ApiResponse<List<AddUserModel>>> GetOwners()
        {
            var responseModel = new ApiResponse<List<AddUserModel>>();
            var owners = _userManager.Users.Where(user => user.Type == 1 && user.OwnerId == null).ToList();
            responseModel.Status = false;
            responseModel.Message = "All Owner Users";
            responseModel.Response = _mapper.Map<List<AppUser>,List<AddUserModel>>(owners);

            return responseModel;
        }

        //Update user
        public async Task<ApiResponse<AddUserModel>> UpdateUser(string UserId, AddUserModel model, IFormFile file)
        {
            var responseModel = new ApiResponse<AddUserModel>();

            //Get User from DB
            var userInDb = await _userManager.FindByIdAsync(UserId);

            //Check if user is exist or not
            if (userInDb == null)
            {
                responseModel.Status = false;
                responseModel.Message = "This user is not exist";

                return responseModel;
            }

            //Make user to be Owner if he was Follower
            if (userInDb.Type == 2 && model.Type == 1)
            {
                userInDb.IsRegistered = false;
                userInDb.OwnerId = null;
                await _userManager.UpdateAsync(userInDb);
                await _context.SaveChangesAsync();
            }

            //Check if user make observations or not if not remove him from his units and convert him to a follower user
            if (userInDb.Type == 1 && model.Type == 2)
            {
                var unitsOfUser = await _context.Units.SingleOrDefaultAsync(uu => uu.AppUserId == UserId);
                if (unitsOfUser == null)
                {
                    userInDb.Type = model.Type;
                }
                else
                {
                    var unitObservations = _context.UnitObservations.Where(uo => uo.UnitID == unitsOfUser.Id).ToList();
                    if (unitObservations == null)
                    {
                        userInDb.IsRegistered = false;
                        unitsOfUser.AppUserId = null;
                        _context.Units.Update(unitsOfUser);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        responseModel.Status = false;
                        responseModel.Message = "This user is already make observations for his units So can't make him a follower user!";

                        return responseModel;
                    }
                }
            }

            model.Id = UserId;
            model.UserName = model.PhoneNumber;
            if (file == null)
                model.Image = userInDb.Image;
            else
                model.Image = await _services.UploadPhoto(file);

            _mapper.Map(model, userInDb);

            //Create User
            var result = await _userManager.UpdateAsync(userInDb);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description},";
                }
                responseModel.Message = errors;
                return responseModel;
            }


            responseModel.Status = true;
            responseModel.Message = "Success, User is successfully updated";
            responseModel.Response = model;

            return responseModel;
        }

        //Delete User 
        public async Task<ApiResponse> DeleteUser(string id)
        {
            var responseModel = new ApiResponse();

            //Get User from DB
            var user = await _userManager.FindByIdAsync(id);

            //Check if user is exist or not
            if (user == null)
            {
                responseModel.Status = false;
                responseModel.Message = "This user is not exist";

                return responseModel;
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description},";
                }
                responseModel.Message = errors;
                return responseModel;
            }

            responseModel.Status = true;
            responseModel.Message = "Success, User is successfully deleted";

            return responseModel;
        }

        //Get All Users
        public async Task<ApiResponse<List<AddUserModel>>> GetFollowersByOwnerId(string id)
        {
            var response = new ApiResponse<List<AddUserModel>>();

            var user = await _userManager.FindByIdAsync(id);
            if(user == null)
            {
                response.Status = false;
                response.Message = "There is no Owner With This Id !";

                return response;
            }
            var users = _userManager.Users.Where(o => o.OwnerId == id);
            if (users.Count()==0 )
            {
                response.Status = false;
                response.Message = "There is no Followers To This Owner yet!";

                return response;
            }
          
            response.Status = true;
            response.Message = "Success";
            response.Response = _mapper.Map<List<AppUser>,List<AddUserModel>>(users.ToList());

            return response;
        }
        
        public async Task<ApiResponse<List<AddUserModel>>> GetAllUsers()
        {
            var response = new ApiResponse<List<AddUserModel>>();

            var users = await _userManager.GetUsersInRoleAsync("User");
            if (users == null)
            {
                response.Status = false;
                response.Message = "There is no users yet!";

                return response;
            }
            var followers = await _userManager.GetUsersInRoleAsync("Follower");

            foreach (var follower in followers)
            {
                users.Add(follower);
            }

            //var adminsDTO = admins.Select(_mapper.Map<AhyaaUser, AhyaaUserDTO>);
            response.Status = true;
            response.Message = "Success";
            response.Response = _mapper.Map<List<AppUser>,List<AddUserModel>>(users.ToList());

            return response;
        }


        //Get All Security Members
        public async Task<ApiResponse<List<AppUser>>> GetAllSecurityMembers()
        {
            var response = new ApiResponse<List<AppUser>>();

            var users = await _userManager.GetUsersInRoleAsync("Security");
            if (users == null)
            {
                response.Status = false;
                response.Message = "There is no users yet!";

                return response;
            }

            //var adminsDTO = admins.Select(_mapper.Map<AhyaaUser, AhyaaUserDTO>);
            response.Status = true;
            response.Message = "Success";
            response.Response = (List<AppUser>)users;

            return response;
        }


        //AddUsersToUnits
        public async Task<ApiResponse<AddUserToUnitDTO>> AddUsersToUnits(AddUserToUnitDTO model)
        {
            var responseModel = new ApiResponse<AddUserToUnitDTO>();
            if (model.UserId == null || model.UnitId == 0)
            {
                responseModel.Status = false;
                responseModel.Message = "Invalid UserId Or Unit Id";

                return responseModel;
            }

            var unit = _context.Units.Where(unit => unit.Id == model.UnitId).FirstOrDefault();
            if (unit == null)
            {
                responseModel.Status = false;
                responseModel.Message = "This unit is not exist!";

                return responseModel;
            }


            //Check if this unit had already a main user
            if (unit.AppUserId != null)
            {
                var user = await _userManager.FindByIdAsync(unit.AppUserId);
                if (user == null)
                {
                    responseModel.Status = false;
                    responseModel.Message = "This user is not exist!";

                    return responseModel;
                }
                if (user.Type == 1)
                {

                    responseModel.Status = false;
                    responseModel.Message = "This unit is already has a main user!";

                    return responseModel;

                }
            }
            else
            {
                //Check if the user is already added to this unit
                if (unit.AppUserId == model.UserId && unit.Id == model.UnitId)
                {
                    responseModel.Status = false;
                    responseModel.Message = "This user is already added to this unit!";

                    return responseModel;
                }
                else
                {
                    unit.AppUserId = model.UserId;
                    _context.Units.Update(unit);
                    await _context.SaveChangesAsync();
                }
            }
            responseModel.Status = true;
            responseModel.Message = "Success, Users are successfully added to the unit";
            responseModel.Response = model;

            return responseModel;
        }


        //Get Users Per UnitID
        public async Task<ApiResponse<AddUserModel>> GetUsersPerUnit(int id)
        {
            var response = new ApiResponse<AddUserModel>();

            var unit = _context.Units.Where(unit => unit.Id == id).FirstOrDefault();

            if (unit == null)
            {
                response.Status = true;
                response.Message = "Wrong Unit Id";

                return response;
            }

            if (unit.AppUserId == null)
            {
                response.Status = true;
                response.Message = "There is no Users in this Unit yet!";

                return response;
            }

            var mainUser = await _userManager.FindByIdAsync(unit.AppUserId);
            if (mainUser == null)
            {
                response.Status = true;
                response.Message = "There Is No User With This User Id";

                return response;
            }

            //var followers = _userManager.Users.Where(O => O.OwnerId == mainUser.Id);

            //var usersUnit = _context.UsersUnits.Where(u => u.UnitID == id).ToList();

            //if (usersUnit == null)
            //{
            //    response.Status = true;
            //    response.Message = "There is no Users in this Unit yet!";

            //    return response;
            //}

            var users = new List<AddUserModel>();
            users.Add(_mapper.Map<AppUser, AddUserModel>(mainUser));

            //foreach (var follower in followers)
            //{
            //    var user = await _userManager.FindByIdAsync(follower.Id);
            //    var userDto = _mapper.Map<AppUser, AddUserModel>(user);
            //    users.Add(userDto);
            //}

            response.Status = true;
            response.Message = "Success";
            response.Response = _mapper.Map<AppUser, AddUserModel>(mainUser);

            return response;
        }


        //Get Units For User
        public async Task<ApiResponse<List<UnitDTO>>> GetUnitsForUser(string id)
        {
            var response = new ApiResponse<List<UnitDTO>>();
            var units = _context.Units.Where(unit => unit.AppUserId == id ).ToList();
            if(units.Count() == 0)
            {
                response.Status = true;
                response.Message = "There is no Units For this User yet!";

                return response;
            }

            var unitsTDO = new List<UnitDTO>();

            var unitDto = _mapper.Map<List<Unit>, List<UnitDTO>>(units);
            response.Status = true;
            response.Message = "Success";
            response.Response = unitDto;

            return response;
        }


        //Delete User From Unit
        public async Task<ApiResponse> DeleteUserFromUnit(AddUserToUnitModel model)
        {
            var responseModel = new ApiResponse();

            //Get User from DB
            //var userUnit = await _context.UsersUnits.SingleOrDefaultAsync(uu => uu.UserID == model.UserID && uu.UnitID == id);

            //Check if user is exist or not
            //if (userUnit == null)
            //{
            //    responseModel.Status = false;
            //    responseModel.Message = "This user is not assigned to any units!";

            //    return responseModel;
            //}

            //_context.UsersUnits.Remove(userUnit);
            //await _context.SaveChangesAsync();

            var unit = _context.Units.Where(i => i.Id == model.UnitID).FirstOrDefault();
            if(unit != null)
            {
                if(unit.AppUserId == model.UserID)
                {
                    unit.AppUserId = null;
                    _context.Units.Update(unit);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    responseModel.Status = true;
                    responseModel.Message = "Wrong User Id";
                    return responseModel;

                }
            }
            else
            {
                responseModel.Status = true;
                responseModel.Message = "Wrong Unit Id";
                return responseModel;

            }

            responseModel.Status = true;
            responseModel.Message = "Success, User is successfully deleted";

            return responseModel;
        }

        //User Registeration
        //Check User existance and send verification code
        public async Task<ApiResponse> SendVerfyCode(SendCodeModel model)
        {
            var response = new ApiResponse();
            var user = await _userManager.FindByNameAsync(model.PhoneNumber);
            var units = _context.Units.Where(unit=>unit.AppUserId == user.Id).ToList();
            //var userUnit = _context.UsersUnits.Where(uu => uu.UserID == user.Id).ToList();
            if (user == null)
            {
                response.Status = false;
                response.Message = "This user isn't exist!";

                return response;
            }

            if (user.IsRegistered == true)
            {
                response.Status = false;
                response.Message = "This user is already registered!";

                return response;
            }

            foreach (var uu in units)
            {
                if (uu.Id == model.UnitID)
                {
                    var random = new Random();
                    user.VerificationCode = random.Next(1000, 9999).ToString();

                    var smsRequest = new SmsRequest()
                    {
                        ToPhoneNumber = model.PhoneNumber,
                        Body = $"كود التفعيل هو" + "\n" + "\n" + user.VerificationCode
                    };
                    //await _smsServices.SendSmsAsync(smsRequest);

                    _context.Users.Attach(user);
                    bool isActive = _context.Entry(user).Property(n => n.VerificationCode).IsModified == true;
                    await _context.SaveChangesAsync();

                    response.Status = true;
                    response.Message = "Code is successfully send to" + model.PhoneNumber;

                    return response;
                }
            }


            response.Status = false;
            response.Message = "This user is not assigned to a unit yet!";

            return response;

        }

        //Recieve Verification Code
        public async Task<ApiResponse> RecieveVerficationCode(VerificationCodeModel model)
        {
            var response = new ApiResponse();
            var user = await _userManager.FindByNameAsync(model.PhoneNumber);
            if (user == null)
            {
                response.Status = false;
                response.Message = "This user isn't valide";

                return response;
            }

            if (user.VerificationCode != model.VerificationCode)
            {
                response.Status = false;
                response.Message = "Verfy code isn't correct!";

                return response;
            }

            user.IsRegistered = true;
            _context.Users.Attach(user);
            bool isActive = _context.Entry(user).Property(n => n.IsRegistered).IsModified == true;
            await _context.SaveChangesAsync();

            response.Status = true;
            response.Message = "User is successfully registered";

            return response;
        }


        //Change Password
        public async Task<ApiResponse> ChangePassword(ChangePasswordModel model)
        {
            var response = new ApiResponse();
            var user = await _userManager.FindByNameAsync(model.PhoneNumber);
            if (user == null)
            {
                response.Status = false;
                response.Message = "This user isn't valide";

                return response;
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, token, model.Password);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description},";
                }
                response.Message = errors;
                return response;
            }

            response.Status = true;
            response.Message = "Success, Password is successfully updated";

            return response;
        }


        //User Login
        public async Task<ApiResponse<LoginAdminModel.Response>> UserLogin(LoginAdminModel.Request request)
        {
            var responseModel = new ApiResponse<LoginAdminModel.Response>();

            AppUser user;

            if (request.UserNameOrEmail.Contains("@"))
            {
                user = await _userManager.FindByEmailAsync(request.UserNameOrEmail);
            }
            else
            {
                user = await _userManager.FindByNameAsync(request.UserNameOrEmail);
            }

            if (user == null)
            {
                responseModel.Status = false;
                responseModel.Message = "This user is not exist!";
                return responseModel;
            }

            if (user.IsRegistered == false)
            {
                responseModel.Status = false;
                responseModel.Message = "This user is not Registered!";
                return responseModel;
            }


            var jwtSecurityToken = await _services.GenerateJwt(user);
            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);
            if (result.Succeeded)
            {
                responseModel.Response = new LoginAdminModel.Response()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Image = user.Image,
                    Type = user.Type,
                    NationalID = user.NationalID,
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
                };
                responseModel.Status = true;
                responseModel.Message = "Success";
                return responseModel;
            }
            else
            {
                responseModel.Status = false;
                responseModel.Message = "failure during signning in";
                return responseModel;
            }

        }


        //Update User Image
        public async Task<ApiResponse<UpdateUserImage>> UpdateUserImage(UpdateUserImage model, IFormFile file)
        {
            var response = new ApiResponse<UpdateUserImage>();
            var user = await _userManager.FindByIdAsync(model.UserID);
            if (user == null)
            {
                response.Status = false;
                response.Message = "This user isn't valide";

                return response;
            }

            model.Image = await _services.UploadPhoto(file);

            user.Image = model.Image;
            _context.Users.Attach(user);
            bool isActive = _context.Entry(user).Property(n => n.Image).IsModified == true;
            await _context.SaveChangesAsync();

            response.Status = true;
            response.Message = "User Image is successfully Changed";
            response.Response = model;

            return response;
        }


        //Suspend User
        public async Task<ApiResponse<bool>> IsActive(string id, NewsStatus model)
        {
            var response = new ApiResponse<bool>();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                response.Status = false;
                response.Message = "This user is not exist!";

                return response;
            }

            user.IsActive = model.Status;
            _context.Users.Attach(user);
            bool isActive = _context.Entry(user).Property(n => n.IsActive).IsModified == true;
            await _context.SaveChangesAsync();

            response.Status = true;
            response.Message = "Success";
            response.Response = isActive;

            return response;
        }



        //User Identity Part
        public async Task<ApiResponse<UserIdentityDTO>> AddUserIdentity(UserIdentityDTO model)
        {
            var response = new ApiResponse<UserIdentityDTO>();

            var category = _mapper.Map<UserIdentityDTO, UserIdentity>(model);
            await _context.UserIdentities.AddAsync(category);
            await _context.SaveChangesAsync();

            model.Id = category.Id;

            response.Status = true;
            response.Message = "Success";
            response.Response = model;

            return response;
        }



        public async Task<ApiResponse> DeleteUserIdentity(int id)
        {
            var response = new ApiResponse();
            var category = await _context.UserIdentities.FindAsync(id);
            if (category == null)
            {
                response.Status = false;
                response.Message = "This Identity is not exist";
                return response;
            }

            _context.UserIdentities.Remove(category);
            await _context.SaveChangesAsync();

            response.Status = true;
            response.Message = "User Identity is successfully deleted";
            return response;
        }



        public async Task<ApiResponse<UserIdentityDTO>> UpdateUserIdentity(int id, UserIdentityDTO model)
        {
            var response = new ApiResponse<UserIdentityDTO>();

            var category = _context.UserIdentities.SingleOrDefault(p => p.Id == id);
            if (category == null)
            {
                response.Status = false;
                response.Message = "This Identity is not exist";

                return response;
            }


            model.Id = id;

            _mapper.Map(model, category);

            _context.UserIdentities.Update(category);
            await _context.SaveChangesAsync();


            response.Status = true;
            response.Message = "Success";
            response.Response = model;


            return response;
        }


        public async Task<ApiResponse<List<UserIdentityDTO>>> GetAllIdentities()
        {
            var Categories = _context.UserIdentities.Select(_mapper.Map<UserIdentity, UserIdentityDTO>).ToList();
            var response = new ApiResponse<List<UserIdentityDTO>>();

            if (Categories == null)
            {
                response.Status = true;
                response.Message = "There is no Identities yet!";

                return response;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = Categories;

            return response;
        }


        //Security Gate
        //Generate QRCode
        public async Task<ApiResponse<GetQRCode>> GetQRCode(string id)
        {
            var response = new ApiResponse<GetQRCode>();
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                response.Status = false;
                response.Message = "This user is not exist!";

                return response;
            }

            var model = new GetQRCode()
            {
                QRCode = _services.GenerateRandomCode() + id,
                ValidateDate = DateTime.UtcNow.AddHours(2)
            };

            user.QRCode = model.QRCode;
            user.QRGenerationTime = model.ValidateDate;
            _context.Users.Attach(user);
            bool isActive = _context.Entry(user).Property(n => n.QRCode).IsModified == true;
            bool isActive2 = _context.Entry(user).Property(n => n.QRGenerationTime).IsModified == true;
            await _context.SaveChangesAsync();

            response.Status = true;
            response.Message = "Success";
            response.Response = model;

            return response;
        }



        //Scan QRCode
        public async Task<ApiResponse<AppUser>> ScanQRCode(GetQRCode model)
        {
            var response = new ApiResponse<AppUser>();
            var id = model.QRCode.Substring(8, model.QRCode.Length - 8);

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                response.Status = false;
                response.Message = "This user is not exist!";

                return response;
            }

            if (DateTime.UtcNow.AddHours(2) > user.QRGenerationTime.Value.AddHours(1))
            {
                response.Status = false;
                response.Message = "QRCode expired!";

                return response;
            }


            if (!model.QRCode.Trim().Equals(user.QRCode.Trim()))
            {
                response.Status = false;
                response.Message = "QRCode doesn't exist!";

                return response;
            }


            var gateLog = new GateLog()
            {
                UserID = id,
                EntryDate = DateTime.UtcNow.AddHours(2)
            };

            await _context.GateLogs.AddAsync(gateLog);
            await _context.SaveChangesAsync();

            response.Status = true;
            response.Message = "Success";
            response.Response = user;

            return response;
        }

        public bool CheckDateTime(DateTime start, DateTime end)
        {
            DateTime PastYear = DateTime.Now.AddYears(-1).Date;
            if (start.Date < PastYear)
                return false;
            if ((end.Date - start.Date).TotalDays > 31)
                return false;
            return true;
        }
        public async Task<ApiResponse<List<GateLogDTO>>> GetGateLogs(string _StartDate, string _EndDate, string UserID)
        {
            var response = new ApiResponse<List<GateLogDTO>>();
            DateTime StartDate;
            DateTime EndDate;
            List<GateLogDTO> GateLogs;
            if (_StartDate == null || _EndDate == null)
            {
                GateLogs = _context.GateLogs.Include(u => u.User)
                .Where(log => log.UserID == (UserID ?? log.UserID))
                .Select(log => new GateLogDTO
                {
                    Id = log.Id,
                    EntryDate = log.EntryDate,
                    Type = log.User.Type,
                    UserID = log.User.Id,
                    Image = log.User.Image,
                    Name = log.User.Name
                }).ToList();
            }
            else
            {
                StartDate = Convert.ToDateTime(_StartDate);
                EndDate = Convert.ToDateTime(_EndDate);
                EndDate = EndDate.AddDays(1);
                if (!CheckDateTime(StartDate, EndDate))
                {
                    response.Status = false;
                    response.Message = " ";
                    response.Response = null;
                    return response;
                }
                GateLogs = _context.GateLogs.Include(u => u.User)
                .Where(log => log.EntryDate >= StartDate && log.EntryDate <= EndDate && log.UserID == (UserID ?? log.UserID))
                .Select(log => new GateLogDTO
                {
                    Id = log.Id,
                    EntryDate = log.EntryDate,
                    Type = log.User.Type,
                    UserID = log.User.Id,
                    Image = log.User.Image,
                    Name = log.User.Name
                }).ToList();
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = GateLogs;

            return response;
        }

        public async Task<ApiResponse<List<VisitLogDTO>>> GetVisitsLogs(string _StartDate, string _EndDate)
        {
            var response = new ApiResponse<List<VisitLogDTO>>();

            DateTime StartDate = Convert.ToDateTime(_StartDate);
            DateTime EndDate = Convert.ToDateTime(_EndDate);
            EndDate = EndDate.AddDays(1);

            if (!CheckDateTime(StartDate, EndDate))
            {
                response.Status = false;
                response.Message = "Wrong Date!";
                response.Response = null;
                return response;
            }

            var Logs = _context.VisitsLogs
                .Include(log => log.QRVisitorInvitation.Visitor)
                .Include(log => log.QRVisitorInvitation.Owner)
                .Include(log => log.SMSVisitorInvitation)
                .Where(log => log.Date >= StartDate && log.Date <= EndDate)
                .Select(log => new VisitLogDTO
                {
                    Id = log.Id,
                    Date = log.Date,
                    Type = log.QRVisitorInvitationId == null ? 2 : 1,
                    VisitorName = log.QRVisitorInvitationId == null ? log.SMSVisitorInvitation.NameOrDesc : log.QRVisitorInvitation.Visitor.UserName,
                    OwnerName = log.QRVisitorInvitationId == null ? log.SMSVisitorInvitation.Owner.Name : log.QRVisitorInvitation.Owner.Name,
                    SecurityName = log.SecurityGate.Name
                }).ToList();

            response.Status = true;
            response.Message = "Success";
            response.Response = Logs;

            return response;
        }

        public async Task<ApiResponse<List<UserSearchDTO>>> UserSearch(string subSearch)
        {
            var response = new ApiResponse<List<UserSearchDTO>>();

            var Users = _context.Users.Where(user => (user.Type == 1 || user.Type == 2) &&
                                        (user.Name.Contains(subSearch) || user.PhoneNumber.Contains(subSearch))).ToList();
            var UserSearchDTO = _mapper.Map<List<UserSearchDTO>>(Users);

            response.Status = true;
            response.Message = "Success";
            response.Response = UserSearchDTO;

            return response;
        }
        
    }
}
