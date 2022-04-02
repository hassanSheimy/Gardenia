using Gardenia.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.Interfaces
{
    public interface INewsRepository
    {
        public Task<ApiResponse<NewsDTO>> AddNews(NewsDTO model);
        public Task<ApiResponse> AddNewsImages(int id, IFormFile[] files);
        public Task<ApiResponse> DeleteNewsImage(int id);
        public Task<ApiResponse<NewsDTO>> UpdateNews(int id, NewsDTO model);
        //public Task<ApiResponse> UpdateNewsImages(int id, IFormFile[] files);
        public Task<ApiResponse> DeleteNews(int id);
        public Task<ApiResponse<List<NewsDTO>>> GetAllNews();
        public Task<ApiResponse<bool>> IsActive(int id, NewsStatus model);
        public Task<ApiResponse<IsLikeResponse>> IsLike(int id, NewsStatus model);
        public Task<ApiResponse<ResponseCommentDTO>> AddComment(int id, RequestCommentDTO model);
        public Task<ApiResponse> DeleteComment(int id);
        public Task<ApiResponse<GetNewsById>> GetNewsById(int id);
        public Task<ApiResponse<List<NewsDTO>>> GetAllFavNews(FavNewsDTO model);
    }
}
