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
    public class PublicTrafficRepository : IPublicTrafficRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicServices _services;
        public PublicTrafficRepository(AppDbContext context, IMapper mapper, IBasicServices services)
        {
            _context = context;
            _mapper = mapper;
            _services = services;
        }

        public async Task<ApiResponse<PublicTrafficDTO>> AddPublicTraffic(PublicTrafficDTO model)
        {
            var response = new ApiResponse<PublicTrafficDTO>();

            var publicTraffic = _mapper.Map<PublicTrafficDTO, PublicTraffic>(model);
            await _context.PublicTraffics.AddAsync(publicTraffic);
            await _context.SaveChangesAsync();

            model.Id = publicTraffic.Id;

            response.Status = true;
            response.Message = "Success";
            response.Response = model;

            return response;
        }

        public async Task<ApiResponse> DeletePublicTraffic(int id)
        {
            var response = new ApiResponse();
            var traffic = await _context.PublicTraffics.FindAsync(id);
            if (traffic == null)
            {
                response.Status = false;
                response.Message = "This Category is not exist";
                return response;
            }

            _context.PublicTraffics.Remove(traffic);
            await _context.SaveChangesAsync();

            response.Status = true;
            response.Message = "Category is successfully deleted";
            return response;
        }

        public async Task<ApiResponse<List<PublicTrafficDTO>>> GetAllPublicTraffics()
        {
            var publicTraffics = _context.PublicTraffics.Select(_mapper.Map<PublicTraffic, PublicTrafficDTO>).ToList();


            var response = new ApiResponse<List<PublicTrafficDTO>>();

            if (publicTraffics == null)
            {
                response.Status = true;
                response.Message = "There is no Public Traffics yet!";

                return response;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = publicTraffics;

            return response;
        }


        //Actual Public Traffics
        public async Task<ApiResponse<NormalPublicTrafficDTO>> AddNormalPublicTraffic(int id, NormalPublicTrafficDTO model, IFormFile file)
        {
            var response = new ApiResponse<NormalPublicTrafficDTO>();

            model.Image = await _services.UploadPhoto(file);
            model.PublicTrafficID = id;

            var category = _context.PublicTraffics.SingleOrDefault(p => p.Id == model.PublicTrafficID);
            if (category == null)
            {
                response.Status = false;
                response.Message = "This Category is not exist";

                return response;
            }

            var normalTraffic = _mapper.Map<NormalPublicTrafficDTO, NormalPublicTraffic>(model);
            await _context.NormalPublicTraffics.AddAsync(normalTraffic);
            await _context.SaveChangesAsync();

            model.Id = normalTraffic.Id;

            response.Status = true;
            response.Message = "Success";
            response.Response = model;

            return response;
        }

        public async Task<ApiResponse<List<NormalPublicTrafficDTO>>> GetAllNormalTrafficsById(int id)
        {

            var response = new ApiResponse<List<NormalPublicTrafficDTO>>();

            var category = _context.PublicTraffics.SingleOrDefault(p => p.Id == id);
            if (category == null)
            {
                response.Status = false;
                response.Message = "This Category is not exist";

                return response;
            }

            var normalTraffics = _context.NormalPublicTraffics.Where(traffic => traffic.PublicTrafficID == id).Select(_mapper.Map<NormalPublicTraffic, NormalPublicTrafficDTO>).ToList();
            if (normalTraffics == null)
            {
                response.Status = true;
                response.Message = "There is no Traffics yet!";

                return response;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = normalTraffics;

            return response;
        }

        public async Task<ApiResponse<NormalPublicTrafficDTO>> UpdateNormalPublicTraffic(int id, NormalPublicTrafficDTO model, IFormFile file)
        {
            var response = new ApiResponse<NormalPublicTrafficDTO>();

            var category = _context.PublicTraffics.SingleOrDefault(p => p.Id == model.PublicTrafficID);
            if (category == null)
            {
                response.Status = false;
                response.Message = "This Category is not exist";

                return response;
            }

            var Traffic = await _context.NormalPublicTraffics.FindAsync(id);

            if (Traffic == null)
            {
                response.Status = false;
                response.Message = "This Traffic is not exist";
                return response;
            }


            if (file != null)
            {
                model.Image = await _services.UploadPhoto(file);
            }
            else
            {
                model.Image = Traffic.Image;
            }
            model.Rate = Traffic.Rate;
            model.RatersCount = Traffic.RatersCount;
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

            _mapper.Map(model, Traffic);

            _context.NormalPublicTraffics.Update(Traffic);
            await _context.SaveChangesAsync();


            response.Status = true;
            response.Message = "Success";
            response.Response = model;


            return response;
        }

        public async Task<ApiResponse> DeleteNormalPublicTraffic(int id)
        {
            var response = new ApiResponse();
            var traffic = await _context.NormalPublicTraffics.FindAsync(id);
            if (traffic == null)
            {
                response.Status = false;
                response.Message = "This Traffic is not exist";
                return response;
            }

            _context.NormalPublicTraffics.Remove(traffic);
            await _context.SaveChangesAsync();

            response.Status = true;
            response.Message = "Public Traffic is successfully deleted";
            return response;
        }


        //Rate
        public async Task<ApiResponse<RateResponse>> AddRate(int id, AddRateRequest model)
        {
            var traffic = await _context.NormalPublicTraffics.FindAsync(id);
            var trafficDTO = _mapper.Map<NormalPublicTraffic, NormalPublicTrafficDTO>(traffic);

            var rateResponse = new RateResponse();
            var response = new ApiResponse<RateResponse>();

            if (trafficDTO != null)
            {
                trafficDTO.Rate = _services.AddRate(trafficDTO.Rate, trafficDTO.RatersCount, model.UserRate);
                trafficDTO.RatersCount++;

                rateResponse.TotalRate = trafficDTO.Rate;
                rateResponse.RatersCount = trafficDTO.RatersCount;

                _mapper.Map(trafficDTO, traffic);


                _context.NormalPublicTraffics.Update(traffic);
                await _context.SaveChangesAsync();



                response.Status = true;
                response.Message = "Success";
                response.Response = rateResponse;

                return response;
            }

            response.Status = false;
            response.Message = "This Traffic is not exist";

            return response;
        }


        public async Task<ApiResponse<RateResponse>> UpdateRate(int id, UpdateRateRequest model)
        {
            var traffic = await _context.NormalPublicTraffics.FindAsync(id);
            var trafficDTO = _mapper.Map<NormalPublicTraffic, NormalPublicTrafficDTO>(traffic);

            var rateResponse = new RateResponse();
            var response = new ApiResponse<RateResponse>();

            if (trafficDTO.RatersCount == 1)
            {
                trafficDTO.RatersCount++;
            }

            if (trafficDTO != null)
            {
                trafficDTO.Rate = _services.UpdateRate(trafficDTO.Rate, trafficDTO.RatersCount, model.NewRate, model.OldRate);

                rateResponse.TotalRate = trafficDTO.Rate;
                rateResponse.RatersCount = trafficDTO.RatersCount;

                _mapper.Map(trafficDTO, traffic);


                _context.NormalPublicTraffics.Update(traffic);
                await _context.SaveChangesAsync();

                response.Status = true;
                response.Message = "Success";
                response.Response = rateResponse;
                return response;
            }

            response.Status = false;
            response.Message = "This Traffic is not exist";

            return response;
        }


        //Police
        public async Task<ApiResponse<PoliceDTO>> AddPolice(PoliceDTO model, IFormFile file)
        {
            var response = new ApiResponse<PoliceDTO>();

            model.Image = await _services.UploadPhoto(file);

            var police = _mapper.Map<PoliceDTO, Police>(model);
            await _context.Polices.AddAsync(police);
            await _context.SaveChangesAsync();

            model.Id = police.Id;

            response.Status = true;
            response.Message = "Success";
            response.Response = model;

            return response;
        }


        public async Task<ApiResponse<PoliceDTO>> UpdatePoliceStation(int id, PoliceDTO model, IFormFile file)
        {
            var response = new ApiResponse<PoliceDTO>();

            var police = await _context.Polices.FindAsync(id);

            if (police == null)
            {
                response.Status = false;
                response.Message = "This Police Station is not exist";

                return response;
            }


            if (file != null)
            {
                model.Image = await _services.UploadPhoto(file);
            }
            else
            {
                model.Image = police.Image;
            }

            model.Rate = police.Rate;
            model.RatersCount = police.RatersCount;


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

            _mapper.Map(model, police);

            _context.Polices.Update(police);
            await _context.SaveChangesAsync();


            response.Status = true;
            response.Message = "Success";
            response.Response = model;


            return response;
        }

        public async Task<ApiResponse<List<PoliceDTO>>> GetAllPoliceStations()
        {
            var policeStations = _context.Polices.Select(_mapper.Map<Police, PoliceDTO>).ToList();


            var response = new ApiResponse<List<PoliceDTO>>();

            if (policeStations == null)
            {
                response.Status = true;
                response.Message = "There is no Police Stations yet!";

                return response;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = policeStations;

            return response;
        }


        public async Task<ApiResponse<RateResponse>> AddPoliceRate(int id, AddRateRequest model)
        {
            var police = await _context.Polices.FindAsync(id);
            var policeDTO = _mapper.Map<Police, PoliceDTO>(police);

            var rateResponse = new RateResponse();
            var response = new ApiResponse<RateResponse>();

            if (policeDTO != null)
            {
                policeDTO.Rate = _services.AddRate(policeDTO.Rate, policeDTO.RatersCount, model.UserRate);
                policeDTO.RatersCount++;

                rateResponse.TotalRate = policeDTO.Rate;
                rateResponse.RatersCount = policeDTO.RatersCount;

                _mapper.Map(policeDTO, police);


                _context.Polices.Update(police);
                await _context.SaveChangesAsync();



                response.Status = true;
                response.Message = "Success";
                response.Response = rateResponse;

                return response;
            }

            response.Status = false;
            response.Message = "This Police station is not exist";

            return response;
        }


        public async Task<ApiResponse<RateResponse>> UpdatePoliceRate(int id, UpdateRateRequest model)
        {
            var police = await _context.Polices.FindAsync(id);
            var policeDTO = _mapper.Map<Police, PoliceDTO>(police);


            var rateResponse = new RateResponse();
            var response = new ApiResponse<RateResponse>();

            if (policeDTO.RatersCount == 1)
            {
                policeDTO.RatersCount++;
            }

            if (policeDTO != null)
            {
                policeDTO.Rate = _services.UpdateRate(policeDTO.Rate, policeDTO.RatersCount, model.NewRate, model.OldRate);

                rateResponse.TotalRate = policeDTO.Rate;
                rateResponse.RatersCount = policeDTO.RatersCount;

                _mapper.Map(policeDTO, police);


                _context.Polices.Update(police);
                await _context.SaveChangesAsync();

                response.Status = true;
                response.Message = "Success";
                response.Response = rateResponse;
                return response;
            }

            response.Status = false;
            response.Message = "This Police station is not exist";

            return response;
        }


        //Electricity
        public async Task<ApiResponse<ElectricityDTO>> AddElectricity(ElectricityDTO model, IFormFile file)
        {
            var response = new ApiResponse<ElectricityDTO>();

            model.Image = await _services.UploadPhoto(file);

            var electricity = _mapper.Map<ElectricityDTO, Electricity>(model);
            await _context.Electricities.AddAsync(electricity);
            await _context.SaveChangesAsync();

            model.Id = electricity.Id;

            response.Status = true;
            response.Message = "Success";
            response.Response = model;

            return response;
        }

        public async Task<ApiResponse<ElectricityDTO>> UpdateElectricity(int id, ElectricityDTO model, IFormFile file)
        {
            var response = new ApiResponse<ElectricityDTO>();

            var electricity = await _context.Electricities.FindAsync(id);

            if (electricity == null)
            {
                response.Status = false;
                response.Message = "This Electricity Station is not exist";

                return response;
            }


            if (file != null)
            {
                model.Image = await _services.UploadPhoto(file);
            }
            else
            {
                model.Image = electricity.Image;
            }

            model.Rate = electricity.Rate;
            model.RatersCount = electricity.RatersCount;


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

            _mapper.Map(model, electricity);

            _context.Electricities.Update(electricity);
            await _context.SaveChangesAsync();


            response.Status = true;
            response.Message = "Success";
            response.Response = model;


            return response;
        }

        public async Task<ApiResponse<List<ElectricityDTO>>> GetAllElectricity()
        {
            var ElectricityStations = _context.Electricities.Select(_mapper.Map<Electricity, ElectricityDTO>).ToList();


            var response = new ApiResponse<List<ElectricityDTO>>();

            if (ElectricityStations == null)
            {
                response.Status = true;
                response.Message = "There is no Electricity Stations yet!";

                return response;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = ElectricityStations;

            return response;
        }



        public async Task<ApiResponse<RateResponse>> AddElectricityRate(int id, AddRateRequest model)
        {
            var electricity = await _context.Electricities.FindAsync(id);
            var electricityDTO = _mapper.Map<Electricity, ElectricityDTO>(electricity);

            var rateResponse = new RateResponse();
            var response = new ApiResponse<RateResponse>();

            if (electricityDTO != null)
            {
                electricityDTO.Rate = _services.AddRate(electricityDTO.Rate, electricityDTO.RatersCount, model.UserRate);
                electricityDTO.RatersCount++;

                rateResponse.TotalRate = electricityDTO.Rate;
                rateResponse.RatersCount = electricityDTO.RatersCount;

                _mapper.Map(electricityDTO, electricity);


                _context.Electricities.Update(electricity);
                await _context.SaveChangesAsync();



                response.Status = true;
                response.Message = "Success";
                response.Response = rateResponse;

                return response;
            }

            response.Status = false;
            response.Message = "This Electricity station is not exist";

            return response;
        }


        public async Task<ApiResponse<RateResponse>> UpdateElectricityRate(int id, UpdateRateRequest model)
        {
            var electricity = await _context.Electricities.FindAsync(id);
            var electricityDTO = _mapper.Map<Electricity, ElectricityDTO>(electricity);


            var rateResponse = new RateResponse();
            var response = new ApiResponse<RateResponse>();

            if (electricityDTO.RatersCount == 1)
            {
                electricityDTO.RatersCount++;
            }

            if (electricityDTO != null)
            {
                electricityDTO.Rate = _services.UpdateRate(electricityDTO.Rate, electricityDTO.RatersCount, model.NewRate, model.OldRate);

                rateResponse.TotalRate = electricityDTO.Rate;
                rateResponse.RatersCount = electricityDTO.RatersCount;

                _mapper.Map(electricityDTO, electricity);


                _context.Electricities.Update(electricity);
                await _context.SaveChangesAsync();

                response.Status = true;
                response.Message = "Success";
                response.Response = rateResponse;
                return response;
            }

            response.Status = false;
            response.Message = "This Electricity station is not exist";

            return response;
        }
    }
}
