using AutoMapper;
using Gardenia.Data.DataAccess;
using Gardenia.Data.Models;
using Gardenia.DTOs;
using Gardenia.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.Repositories
{
    public class MediaRepository : IMediaRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicServices _services;
        public MediaRepository(AppDbContext context, IMapper mapper, IBasicServices services)
        {
            _context = context;
            _mapper = mapper;
            _services = services;
        }

        public async Task<ApiResponse<ImagesDTO>> AddImage(ImagesDTO model, IFormFile file)
        {
            var response = new ApiResponse<ImagesDTO>();

            model.Image = await _services.UploadPhoto(file);

            var imagemodel = _mapper.Map<ImagesDTO, Media>(model);
            await _context.Medias.AddAsync(imagemodel);
            await _context.SaveChangesAsync();

            model.Id = imagemodel.Id;

            response.Status = true;
            response.Message = "Success";
            response.Response = model;

            return response;
        }

        public async Task<ApiResponse<ImagesDTO>> UpdateImage(int id, ImagesDTO model, IFormFile file)
        {
            var response = new ApiResponse<ImagesDTO>();

            var imagemodel = await _context.Medias.FindAsync(id);

            if (imagemodel == null)
            {
                response.Status = false;
                response.Message = "This Video is not exist";
                return response;
            }


            if (file != null)
            {
                model.Image = await _services.UploadPhoto(file);
            }
            else
            {
                model.Image = imagemodel.Image;
            }

            //if (model.ImagePath != null)
            //{
            //    if (LandMarkInDb.ImagePath != null)
            //    {
            //        //var filePath = Path.Combine(_environment.WebRootPath, @"Images\", LandMarkInDb.ImagePath);
            //        var filePath = String.Format("{0}\\Images\\{1}", _environment.WebRootPath, LandMarkInDb.ImagePath);
            //        File.Delete(filePath);

            //    }
            //}

            model.Id = id;

            _mapper.Map(model, imagemodel);

            _context.Medias.Update(imagemodel);
            await _context.SaveChangesAsync();


            response.Status = true;
            response.Message = "Success";
            response.Response = model;


            return response;


        }

        public async Task<ApiResponse> DeleteImage(int id)
        {
            var response = new ApiResponse();
            var image = await _context.Medias.FindAsync(id);
            if (image == null)
            {
                response.Status = false;
                response.Message = "This Image is not exist";
                return response;
            }

            _context.Medias.Remove(image);
            await _context.SaveChangesAsync();

            response.Status = true;
            response.Message = "Image is successfully deleted";
            return response;
        }

        public async Task<ApiResponse<List<ImagesDTO>>> GetAllImages()
        {

            var images = _context.Medias.Where(m => m.URL == null).Select(_mapper.Map<Media, ImagesDTO>).ToList();
            var response = new ApiResponse<List<ImagesDTO>>();

            if (images == null)
            {
                response.Status = false;
                response.Message = "There is no Images yet!";

                return response;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = images;

            return response;
        }



        public async Task<ApiResponse<VideoDTO>> AddVideo(VideoDTO model, IFormFile file)
        {
            var response = new ApiResponse<VideoDTO>();
            model.Image = await _services.UploadPhoto(file);


            var videomodel = _mapper.Map<VideoDTO, Media>(model);
            await _context.Medias.AddAsync(videomodel);
            await _context.SaveChangesAsync();

            model.Id = videomodel.Id;

            response.Status = true;
            response.Message = "Success";
            response.Response = model;

            return response;
        }


        public async Task<ApiResponse<VideoDTO>> UpdateVideo(int id, VideoDTO model, IFormFile file)
        {
            var response = new ApiResponse<VideoDTO>();

            var videomodel = await _context.Medias.FindAsync(id);

            if (videomodel == null)
            {
                response.Status = false;
                response.Message = "This Video is not exist";
                return response;
            }


            if (file != null)
            {
                model.Image = await _services.UploadPhoto(file);
            }
            else
            {
                model.Image = videomodel.Image;
            }

            //if (model.ImagePath != null)
            //{
            //    if (LandMarkInDb.ImagePath != null)
            //    {
            //        //var filePath = Path.Combine(_environment.WebRootPath, @"Images\", LandMarkInDb.ImagePath);
            //        var filePath = String.Format("{0}\\Images\\{1}", _environment.WebRootPath, LandMarkInDb.ImagePath);
            //        File.Delete(filePath);

            //    }
            //}

            model.Id = id;

            _mapper.Map(model, videomodel);

            _context.Medias.Update(videomodel);
            await _context.SaveChangesAsync();


            response.Status = true;
            response.Message = "Success";
            response.Response = model;


            return response;


        }


        public async Task<ApiResponse> DeleteVideo(int id)
        {
            var response = new ApiResponse();
            var video = await _context.Medias.FindAsync(id);
            if (video == null)
            {
                response.Status = false;
                response.Message = "This Video is not exist";
                return response;
            }

            _context.Medias.Remove(video);
            await _context.SaveChangesAsync();

            response.Status = true;
            response.Message = "Video is successfully deleted";
            return response;
        }



        public async Task<ApiResponse<List<VideoDTO>>> GetAllVideos()
        {

            var videos = _context.Medias.Where(m => m.URL != null).Select(_mapper.Map<Media, VideoDTO>).ToList();
            var response = new ApiResponse<List<VideoDTO>>();

            if (videos == null)
            {
                response.Status = false;
                response.Message = "There is no Images yet!";

                return response;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = videos;

            return response;
        }
    }
}
