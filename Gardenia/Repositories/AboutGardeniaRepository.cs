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
    public class AboutGardeniaRepository : IAboutGardeniaRepository
    {
        private readonly AppDbContext _context;
        private readonly IBasicServices _services;
        private readonly IMapper _mapper;
        public AboutGardeniaRepository(AppDbContext context, IMapper mapper, IBasicServices services)
        {
            _context = context;
            _services = services;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<AboutGardeniaDTO>>> GetAllAbout()
        {

            var aboutGardenia = _context.AboutGardenia.Select(_mapper.Map<AboutGardenia, AboutGardeniaDTO>).ToList();

            var response = new ApiResponse<List<AboutGardeniaDTO>>();

            if (aboutGardenia == null)
            {
                response.Status = false;
                response.Message = "There is no History yet!";

                return response;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = aboutGardenia;

            return response;
        }

        public async Task<ApiResponse<AboutGardeniaDTO>> UpdateAbout(int? id, AboutGardeniaDTO model)
        {
            var response = new ApiResponse<AboutGardeniaDTO>();

            if (id == null)
            {
                var aboutGardenia = _mapper.Map<AboutGardeniaDTO, AboutGardenia>(model);
                await _context.AboutGardenia.AddAsync(aboutGardenia);
                await _context.SaveChangesAsync();

                model.Id = aboutGardenia.Id;

                response.Status = true;
                response.Message = "Success";
                response.Response = model;

                return response;
            }

            var aboutGardeniaInDb = _context.AboutGardenia.Single(a => a.Id == id);

            if (aboutGardeniaInDb == null)
            {
                response.Status = false;
                response.Message = "This model is not exist!";

                return response;
            }

            model.Id = (int)id;

            _mapper.Map(model, aboutGardeniaInDb);

            _context.AboutGardenia.Update(aboutGardeniaInDb);
            await _context.SaveChangesAsync();


            response.Status = true;
            response.Message = "Success";
            response.Response = model;


            return response;
        }


        public async Task<ApiResponse> DeleteAbout(int id)
        {
            var response = new ApiResponse();
            var about = await _context.AboutGardenia.FindAsync(id);
            if (about == null)
            {
                response.Status = false;
                response.Message = "This Category is not exist";
                return response;
            }

            _context.AboutGardenia.Remove(about);
            await _context.SaveChangesAsync();

            response.Status = true;
            response.Message = "Brief is successfully deleted";
            return response;
        }


        public async Task<ApiResponse<List<GardeniaDataDTO>>> GetGardeniaData()
        {
            var districtData = _context.GardeniaData.Select(_mapper.Map<GardeniaData, GardeniaDataDTO>).ToList();

            var response = new ApiResponse<List<GardeniaDataDTO>>();

            if (districtData == null)
            {
                response.Status = false;
                response.Message = "There is no Data yet!";

                return response;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = districtData;

            return response;
        }


        public async Task<ApiResponse<GardeniaDataDTO>> UpdateGardeniaData(int? id, GardeniaDataDTO model, IFormFile file)
        {
            var response = new ApiResponse<GardeniaDataDTO>();

            model.Image = await _services.UploadPhoto(file);
            if (id == null)
            {
                var gardeniaData = _mapper.Map<GardeniaDataDTO, GardeniaData>(model);
                await _context.GardeniaData.AddAsync(gardeniaData);
                await _context.SaveChangesAsync();

                model.Id = gardeniaData.Id;

                response.Status = true;
                response.Message = "Success";
                response.Response = model;

                return response;
            }

            var gardeniaData1 = _context.GardeniaData.Single(a => a.Id == id);
            if (gardeniaData1 == null)
            {
                response.Status = false;
                response.Message = "This model is not exist!";

                return response;
            }

            if (file != null)
            {
                model.Image = await _services.UploadPhoto(file);
            }
            else
            {
                model.Image = gardeniaData1.Image;
            }

            model.Id = (int)id;

            _mapper.Map(model, gardeniaData1);

            _context.GardeniaData.Update(gardeniaData1);
            await _context.SaveChangesAsync();


            response.Status = true;
            response.Message = "Success";
            response.Response = model;


            return response;
        }
    }
}
