using AutoMapper;
using Gardenia.Data.DataAccess;
using Gardenia.Data.Models;
using Gardenia.DTOs;
using Gardenia.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.Repositories
{
    public class DevelopmentRepository : IDevelopmentRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public DevelopmentRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ApiResponse<DevelopmentCategoryDTO>> AddCategory(DevelopmentCategoryDTO model)
        {
            var response = new ApiResponse<DevelopmentCategoryDTO>();

            var category = _mapper.Map<DevelopmentCategoryDTO, DevelopmentCategory>(model);
            await _context.DevelopmentCategories.AddAsync(category);
            await _context.SaveChangesAsync();

            model.Id = category.Id;

            response.Status = true;
            response.Message = "Success";
            response.Response = model;

            return response;
        }

        public async Task<ApiResponse<DevelopmentDTO>> AddDevelopment(DevelopmentDTO model)
        {
            var response = new ApiResponse<DevelopmentDTO>();

            var category = _context.DevelopmentCategories.SingleOrDefault(p => p.Id == model.CategoryID);
            if (category == null)
            {
                response.Status = false;
                response.Message = "This Category is not exist";

                return response;
            }

            var development = _mapper.Map<DevelopmentDTO, Development>(model);
            await _context.Developments.AddAsync(development);
            await _context.SaveChangesAsync();

            model.Id = development.Id;

            response.Status = true;
            response.Message = "Success";
            response.Response = model;

            return response;
        }

        public async Task<ApiResponse> DeleteCategory(int id)
        {
            var response = new ApiResponse();
            var category = await _context.DevelopmentCategories.FindAsync(id);
            if (category == null)
            {
                response.Status = false;
                response.Message = "This Category is not exist";
                return response;
            }

            _context.DevelopmentCategories.Remove(category);
            await _context.SaveChangesAsync();

            response.Status = true;
            response.Message = "Category is successfully deleted";
            return response;
        }


        public async Task<ApiResponse<DevelopmentCategoryDTO>> UpdateCategory(int id, DevelopmentCategoryDTO model)
        {
            var response = new ApiResponse<DevelopmentCategoryDTO>();

            var category = _context.DevelopmentCategories.SingleOrDefault(p => p.Id == id);
            if (category == null)
            {
                response.Status = false;
                response.Message = "This Category is not exist";

                return response;
            }


            model.Id = id;

            _mapper.Map(model, category);

            _context.DevelopmentCategories.Update(category);
            await _context.SaveChangesAsync();


            response.Status = true;
            response.Message = "Success";
            response.Response = model;


            return response;
        }

        public async Task<ApiResponse> DeleteDevelopment(int id)
        {
            var response = new ApiResponse();
            var development = await _context.Developments.FindAsync(id);
            if (development == null)
            {
                response.Status = false;
                response.Message = "This Development is not exist";
                return response;
            }

            _context.Developments.Remove(development);
            await _context.SaveChangesAsync();

            response.Status = true;
            response.Message = "Development is successfully deleted";
            return response;
        }

        public async Task<ApiResponse<List<DevelopmentDTO>>> GetAllDevelopments()
        {
            var developments = _context.Developments.Include(a => a.Category).Select(_mapper.Map<Development, DevelopmentDTO>).ToList();

            //var query = from achievement in _context.Achievements
            //            join category in _context.AchievementCategories
            //            on achievement.CategoryID equals category.Id
            //            select new { achievement.Id, achievement.Title, achievement.Description, achievement.Date, category.Name };


            var response = new ApiResponse<List<DevelopmentDTO>>();

            if (developments == null)
            {
                response.Status = true;
                response.Message = "There is no Developments yet!";

                return response;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = developments;

            return response;
        }

        public async Task<ApiResponse<List<DevelopmentCategoryDTO>>> GetCategories()
        {
            var Categories = _context.DevelopmentCategories.Select(_mapper.Map<DevelopmentCategory, DevelopmentCategoryDTO>).ToList();
            var response = new ApiResponse<List<DevelopmentCategoryDTO>>();

            if (Categories == null)
            {
                response.Status = true;
                response.Message = "There is no Categories yet!";

                return response;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = Categories;

            return response;
        }

        public async Task<ApiResponse<List<DevelopmentDTO>>> GetDevelopmentByCategory(int id)
        {
            var response = new ApiResponse<List<DevelopmentDTO>>();
            var category = _context.DevelopmentCategories.SingleOrDefault(p => p.Id == id);
            if (category == null)
            {
                response.Status = false;
                response.Message = "This Category is not exist";

                return response;
            }

            var developments = _context.Developments.Where(a => a.CategoryID == id).Select(_mapper.Map<Development, DevelopmentDTO>).ToList();

            if (developments == null)
            {
                response.Status = true;
                response.Message = "There is no Developments yet in this categories!";

                return response;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = developments;

            return response;
        }

        public async Task<ApiResponse<DevelopmentDTO>> UpdateDevelopment(int id, DevelopmentDTO model)
        {
            var response = new ApiResponse<DevelopmentDTO>();

            var category = _context.DevelopmentCategories.SingleOrDefault(p => p.Id == model.CategoryID);
            if (category == null)
            {
                response.Status = false;
                response.Message = "This Category is not exist";

                return response;
            }

            var development = await _context.Developments.FindAsync(id);

            if (development == null)
            {
                response.Status = false;
                response.Message = "This Development is not exist";
                return response;
            }


            model.Id = id;

            _mapper.Map(model, development);

            _context.Developments.Update(development);
            await _context.SaveChangesAsync();


            response.Status = true;
            response.Message = "Success";
            response.Response = model;


            return response;
        }
    }
}
