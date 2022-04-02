using Gardenia.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.Interfaces
{
    public interface IDevelopmentRepository
    {
        public Task<ApiResponse<DevelopmentDTO>> AddDevelopment(DevelopmentDTO model);
        public Task<ApiResponse> DeleteDevelopment(int id);
        public Task<ApiResponse<DevelopmentDTO>> UpdateDevelopment(int id, DevelopmentDTO model);
        public Task<ApiResponse<List<DevelopmentDTO>>> GetAllDevelopments();
        public Task<ApiResponse<List<DevelopmentDTO>>> GetDevelopmentByCategory(int id);
        public Task<ApiResponse<DevelopmentCategoryDTO>> AddCategory(DevelopmentCategoryDTO model);
        public Task<ApiResponse<List<DevelopmentCategoryDTO>>> GetCategories();
        public Task<ApiResponse> DeleteCategory(int id);
        public Task<ApiResponse<DevelopmentCategoryDTO>> UpdateCategory(int id, DevelopmentCategoryDTO model);
    }
}
