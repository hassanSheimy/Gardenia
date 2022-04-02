using Gardenia.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.Interfaces
{
    public interface IPublicTrafficRepository
    {
        //Category Part
        public Task<ApiResponse<List<PublicTrafficDTO>>> GetAllPublicTraffics();
        public Task<ApiResponse<PublicTrafficDTO>> AddPublicTraffic(PublicTrafficDTO model);
        public Task<ApiResponse> DeletePublicTraffic(int id);

        //Actual Public Traffics
        public Task<ApiResponse<NormalPublicTrafficDTO>> AddNormalPublicTraffic(int id, NormalPublicTrafficDTO model, IFormFile file);
        public Task<ApiResponse<List<NormalPublicTrafficDTO>>> GetAllNormalTrafficsById(int id);
        public Task<ApiResponse<NormalPublicTrafficDTO>> UpdateNormalPublicTraffic(int id, NormalPublicTrafficDTO model, IFormFile file);
        public Task<ApiResponse> DeleteNormalPublicTraffic(int id);


        //Police
        public Task<ApiResponse<PoliceDTO>> AddPolice(PoliceDTO model, IFormFile file);
        public Task<ApiResponse<PoliceDTO>> UpdatePoliceStation(int id, PoliceDTO model, IFormFile file);
        public Task<ApiResponse<List<PoliceDTO>>> GetAllPoliceStations();
        public Task<ApiResponse<RateResponse>> AddPoliceRate(int id, AddRateRequest model);
        public Task<ApiResponse<RateResponse>> UpdatePoliceRate(int id, UpdateRateRequest model);


        //Electricity
        public Task<ApiResponse<ElectricityDTO>> AddElectricity(ElectricityDTO model, IFormFile file);
        public Task<ApiResponse<ElectricityDTO>> UpdateElectricity(int id, ElectricityDTO model, IFormFile file);
        public Task<ApiResponse<RateResponse>> AddElectricityRate(int id, AddRateRequest model);
        public Task<ApiResponse<RateResponse>> UpdateElectricityRate(int id, UpdateRateRequest model);
        public Task<ApiResponse<List<ElectricityDTO>>> GetAllElectricity();



        //Rate
        public Task<ApiResponse<RateResponse>> AddRate(int id, AddRateRequest model);
        public Task<ApiResponse<RateResponse>> UpdateRate(int id, UpdateRateRequest model);
    }
}
