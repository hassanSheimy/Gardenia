using Gardenia.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.Interfaces
{
    public interface IAboutGardeniaRepository
    {
        Task<ApiResponse<AboutGardeniaDTO>> UpdateAbout(int? id, AboutGardeniaDTO model);
        Task<ApiResponse<List<AboutGardeniaDTO>>> GetAllAbout();
        Task<ApiResponse> DeleteAbout(int id);
        Task<ApiResponse<GardeniaDataDTO>> UpdateGardeniaData(int? id, GardeniaDataDTO model, IFormFile file);
        Task<ApiResponse<List<GardeniaDataDTO>>> GetGardeniaData();
    }
}
