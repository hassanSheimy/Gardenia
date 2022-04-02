using Gardenia.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.Interfaces
{
    public interface IBasicServices
    {
        public Task<string> UploadPhoto(IFormFile file);
        Task<string> UploadPhotoImageID(IFormFile file);
        public Task<JwtSecurityToken> GenerateJwt(AppUser user);
        public Task<JwtSecurityToken> GenerateJwtForVisitor(Visitor visitor);
        public double AddRate(double mainRate, int ratersCount, double userRate);
        public double UpdateRate(double mainRate, int ratersCount, double userNewRate, double userOldRate);
        public string GenerateRandomCode();
    }
}
