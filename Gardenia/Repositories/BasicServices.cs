using AutoMapper.Configuration;
using Gardenia.Data.Models;
using Gardenia.Helpers;
using Gardenia.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Gardenia.Repositories
{
    public class BasicServices : IBasicServices
    {
        private readonly IWebHostEnvironment _environment;
        private readonly UserManager<AppUser> _userManager;
        private readonly JWT _jwt;
        //private readonly IConfiguration _configuration;


        public BasicServices(IWebHostEnvironment environment, UserManager<AppUser> userManager, IOptions<JWT> jwt)
        {
            _environment = environment;
            _userManager = userManager;
            _jwt = jwt.Value;
        }

        public async Task<JwtSecurityToken> GenerateJwt(AppUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("userid", user.Id)
            }.Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signInCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                    issuer: _jwt.Issuer,
                    audience: _jwt.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(14),
                    signingCredentials: signInCredentials
                );
            return jwtSecurityToken;
        }

        public async Task<JwtSecurityToken> GenerateJwtForVisitor(Visitor visitor)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, visitor.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, visitor.Email),
                new Claim("userid", visitor.Id.ToString()),
                new Claim("IsVisitor", "True")
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signInCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                    issuer: _jwt.Issuer,
                    audience: _jwt.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(14),
                    signingCredentials: signInCredentials
                );
            return jwtSecurityToken;
        }

        public async Task<string> UploadPhotoImageID(IFormFile file)
        {
            try

            {
                if (file.Length > 0)
                {
                    if (!Directory.Exists(Path.Combine(_environment.WebRootPath + "\\ImagesVisitorID\\")))
                    {
                        Directory.CreateDirectory(Path.Combine(_environment.WebRootPath + "\\ImagesVisitorID\\"));
                    }
                    //using FileStream fileStream = File.Create(_environment.WebRootPath + "\\ImagesVisitorID\\" + file.FileName);
                    using (FileStream fileStream =
                        new FileStream(Path.Combine(_environment.WebRootPath + "\\ImagesVisitorID\\",
                        file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                        fileStream.Flush();
                    };


                    return "\\ImagesVisitorID\\" + file.FileName;

                }
                else
                {
                    return "Failure";
                }
            }
            catch (Exception ex)

            {
                return ex.Message.ToString();
            }
        }

        public async Task<string> UploadPhoto(IFormFile file)
        {
            try

            {
                if (file.Length > 0)
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\Images\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\Images\\");
                    }
                    using FileStream fileStream = File.Create(_environment.WebRootPath + "\\Images\\" + file.FileName);
                    file.CopyTo(fileStream);
                    fileStream.Flush();
                    return "\\Images\\" + file.FileName;

                }
                else
                {
                    return "Failure";
                }
            }
            catch (Exception ex)

            {
                return ex.Message.ToString();
            }
        }


        public double AddRate(double mainRate, int ratersCount, double userRate)
        {
            mainRate = ((mainRate * ratersCount) + userRate) / (ratersCount + 1);
            return Math.Round(mainRate, 1);
        }

        public double UpdateRate(double mainRate, int ratersCount, double userNewRate, double userOldRate)
        {
            mainRate = ((mainRate * ratersCount) - userOldRate) / (ratersCount - 1);
            mainRate = ((mainRate * ratersCount) + userNewRate) / (ratersCount + 1);

            return Math.Round(mainRate, 1);
        }


        public string GenerateRandomCode()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            var stringChars = new char[8];

            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new String(stringChars);
        }
    }
}
