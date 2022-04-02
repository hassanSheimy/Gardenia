using AutoMapper;
using Gardenia.Data.DataAccess;
using Gardenia.Data.Models;
using Gardenia.DTOs;
using Gardenia.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicServices _services;

        public NewsRepository(AppDbContext context, IMapper mapper, IBasicServices services)
        {
            _context = context;
            _mapper = mapper;
            _services = services;
        }

        public async Task<ApiResponse<NewsDTO>> AddNews(NewsDTO model)
        {
            var response = new ApiResponse<NewsDTO>();
            model.Date = DateTime.UtcNow.AddHours(2);

            var news = _mapper.Map<NewsDTO, News>(model);
            await _context.News.AddAsync(news);
            await _context.SaveChangesAsync();

            model.Id = news.Id;

            response.Status = true;
            response.Message = "Success";
            response.Response = model;

            return response;
        }


        public async Task<ApiResponse> AddNewsImages(int id, IFormFile[] files)
        {
            var response = new ApiResponse();
            var news = _context.News.SingleOrDefault(n => n.Id == id);

            if (news == null)
            {
                response.Status = false;
                response.Message = "This News is not exist";

                return response;
            }

            //var OldImages = _context.NewsImages.Where(n => n.NewsID == id);
            //if (OldImages != null)
            //{
            //    _context.NewsImages.RemoveRange(OldImages);
            //}

            foreach (var file in files)
            {
                var newsImages = new NewsImagesDTO()
                {
                    Image = await _services.UploadPhoto(file),
                    NewsID = id
                };

                var newImagesInDb = _mapper.Map<NewsImagesDTO, NewsImages>(newsImages);
                _context.NewsImages.Add(newImagesInDb);
                await _context.SaveChangesAsync();
            }

            response.Status = true;
            response.Message = "Success";

            return response;
        }


        public async Task<ApiResponse<NewsDTO>> UpdateNews(int id, NewsDTO model)
        {
            var response = new ApiResponse<NewsDTO>();

            var NewsInDb = await _context.News.FindAsync(id);

            if (NewsInDb == null)
            {
                response.Status = false;
                response.Message = "This News is not exist";
                return response;
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
            model.Date = NewsInDb.Date;
            _mapper.Map(model, NewsInDb);

            _context.News.Update(NewsInDb);
            await _context.SaveChangesAsync();


            response.Status = true;
            response.Message = "Success";
            response.Response = model;


            return response;
        }


        //public async Task<ApiResponse> UpdateNewsImages(int id, IFormFile[] files)
        //{
        //    var response = new ApiResponse();
        //    var news = _context.News.SingleOrDefault(n => n.Id == id);

        //    if (news == null)
        //    {
        //        response.Status = false;
        //        response.Message = "This News is not exist";

        //        return response;
        //    }

        //    news.Image = await _services.UploadPhoto(files[0]);

        //    var OldImages = _context.NewsImages.Where(n => n.NewsID == id);
        //    _context.NewsImages.RemoveRange(OldImages);

        //    foreach (var file in files)
        //    {
        //        var newsImages = new NewsImagesDTO()
        //        {
        //            Image = await _services.UploadPhoto(file),
        //            NewsID = id
        //        };

        //        var newImagesInDb = _mapper.Map<NewsImagesDTO, NewsImages>(newsImages);
        //        _context.NewsImages.Add(newImagesInDb);
        //        await _context.SaveChangesAsync();
        //    }

        //    response.Status = true;
        //    response.Message = "Success";

        //    return response;
        //}



        public async Task<ApiResponse> DeleteNews(int id)
        {
            var response = new ApiResponse();
            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                response.Status = false;
                response.Message = "This News is not exist";
                return response;
            }

            _context.News.Remove(news);
            await _context.SaveChangesAsync();

            response.Status = true;
            response.Message = "News is successfully deleted";
            return response;
        }

        public async Task<ApiResponse<List<NewsDTO>>> GetAllNews()
        {
            var allNews = _context.News.Select(_mapper.Map<News, NewsDTO>).ToList();
            var response = new ApiResponse<List<NewsDTO>>();

            if (allNews == null)
            {
                response.Status = false;
                response.Message = "There is no News yet!";

                return response;
            }

            foreach (var news in allNews)
            {
                var images = _context.NewsImages.Where(n => n.NewsID == news.Id).Select(_mapper.Map<NewsImages, NewsImagesDTO>).ToList();
                news.Images = images;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = allNews;

            return response;
        }

        public async Task<ApiResponse<bool>> IsActive(int id, NewsStatus model)
        {
            var response = new ApiResponse<bool>();

            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                response.Status = false;
                response.Message = "This is no News yet!";

                return response;
            }

            news.IsActive = model.Status;
            _context.News.Attach(news);
            bool isActive = _context.Entry(news).Property(n => n.IsActive).IsModified == true;
            await _context.SaveChangesAsync();

            response.Status = true;
            response.Message = "Success";
            response.Response = isActive;

            return response;
        }

        public async Task<ApiResponse<IsLikeResponse>> IsLike(int id, NewsStatus model)
        {
            var response = new ApiResponse<IsLikeResponse>();

            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                response.Status = false;
                response.Message = "This is no News yet!";

                return response;
            }

            if (model.Status == true)
            {
                news.Likes++;
            }
            else
            {
                news.DisLikes++;
            }

            _context.News.Attach(news);
            bool like = _context.Entry(news).Property(n => n.Likes).IsModified == true;
            bool disLike = _context.Entry(news).Property(n => n.DisLikes).IsModified == true;
            await _context.SaveChangesAsync();


            var isLikeResponse = new IsLikeResponse()
            {
                Likes = news.Likes,
                DisLikes = news.DisLikes
            };

            response.Status = true;
            response.Message = "Success";
            response.Response = isLikeResponse;

            return response;
        }

        public async Task<ApiResponse<ResponseCommentDTO>> AddComment(int id, RequestCommentDTO model)
        {
            var response = new ApiResponse<ResponseCommentDTO>();
            var user = _context.Users.SingleOrDefault(u => u.Id == model.UserID);
            if (user == null)
            {
                response.Status = false;
                response.Message = "This User is not exist";

                return response;
            }

            var news = _context.News.SingleOrDefault(u => u.Id == id);
            if (news == null)
            {
                response.Status = false;
                response.Message = "This News is not exist";

                return response;
            }

            var responseModel = new ResponseCommentDTO()
            {
                UserID = user.Id,
                UserImage = user.Image,
                UserName = user.Name,
                CommentDate = DateTime.UtcNow.AddHours(2),
                Content = model.Content,
                NewsID = id
            };

            var comment = _mapper.Map<ResponseCommentDTO, NewsComments>(responseModel);
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            responseModel.Id = comment.Id;

            response.Status = true;
            response.Message = "Success";
            response.Response = responseModel;

            return response;
        }


        public async Task<ApiResponse> DeleteComment(int id)
        {
            var response = new ApiResponse();
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                response.Status = false;
                response.Message = "This comment is not exist";
                return response;
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            response.Status = true;
            response.Message = "comment is successfully deleted";
            return response;
        }

        public async Task<ApiResponse<GetNewsById>> GetNewsById(int id)
        {
            var response = new ApiResponse<GetNewsById>();

            var newsInDb = await _context.News.SingleOrDefaultAsync(n => n.Id == id);
            var newsImages = _context.NewsImages.Where(n => n.NewsID == id).Select(_mapper.Map<NewsImages, NewsImagesDTO>).ToList();
            var comments = _context.Comments.Include(c => c.User).ToList().Where(c => c.NewsID == id).Select(_mapper.Map<NewsComments, ResponseCommentDTO>).ToList();

            var news = _mapper.Map<News, NewsWithAllProperties>(newsInDb);

            var News = new GetNewsById()
            {
                News = news,
                NewsImages = newsImages,
                Comments = comments
            };


            if (newsInDb == null)
            {
                response.Status = false;
                response.Message = "This News is not exist!";

                return response;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = News;

            return response;
        }



        public async Task<ApiResponse> DeleteNewsImage(int id)
        {
            var response = new ApiResponse();
            var image = await _context.NewsImages.FindAsync(id);
            if (image == null)
            {
                response.Status = false;
                response.Message = "This image is not exist";
                return response;
            }

            _context.NewsImages.Remove(image);
            await _context.SaveChangesAsync();

            response.Status = true;
            response.Message = "The image is successfully deleted";
            return response;
        }

        public async Task<ApiResponse<List<NewsDTO>>> GetAllFavNews(FavNewsDTO model)
        {
            var allnews = _context.News.Where(n => model.FavNews.Contains(n.Id)).Select(_mapper.Map<News, NewsDTO>).ToList();
            var response = new ApiResponse<List<NewsDTO>>();

            if (allnews == null)
            {
                response.Status = false;
                response.Message = "There is no Fav news yet!";

                return response;
            }

            foreach (var news in allnews)
            {
                var images = _context.NewsImages.Where(n => n.NewsID == news.Id).Select(_mapper.Map<NewsImages, NewsImagesDTO>).ToList();
                news.Images = images;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = allnews;

            return response;
        }
    }
}
