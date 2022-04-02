using Gardenia.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.Interfaces
{
    public interface IMediaRepository
    {
        Task<ApiResponse<List<ImagesDTO>>> GetAllImages();
        Task<ApiResponse<ImagesDTO>> AddImage(ImagesDTO model, IFormFile file);
        Task<ApiResponse<ImagesDTO>> UpdateImage(int id, ImagesDTO model, IFormFile file);
        Task<ApiResponse> DeleteImage(int id);
        Task<ApiResponse<VideoDTO>> AddVideo(VideoDTO model, IFormFile file);
        Task<ApiResponse<VideoDTO>> UpdateVideo(int id, VideoDTO model, IFormFile file);
        Task<ApiResponse<List<VideoDTO>>> GetAllVideos();
        Task<ApiResponse> DeleteVideo(int id);
    }
}
