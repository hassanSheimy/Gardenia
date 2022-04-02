using Gardenia.Data.Models;
using Gardenia.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.Interfaces
{
    public interface IUnitRepository
    {
        //Units Part
        Task<ApiResponse<List<UnitDTO>>> AddUnits(List<UnitDTO> models);
        Task<ApiResponse> DeleteUnit(int id);
        Task<ApiResponse<UnitDTO>> UpdateUnit(int id, UnitDTO model);
        Task<ApiResponse<List<UnitDTO>>> GetAllUnits();
        Task<ApiResponse<UnitDTO>> GetUnitById(int id);
        Task<ApiResponse> UnitAcceptConfirmation(int id, NewsStatus model);

        //Observation of Units Part
        Task<ApiResponse<UnitObservationDTO>> AddObservation(UnitObservationDTO model);
        Task<ApiResponse> AddObservationImages(int id, IFormFile[] files);
        Task<ApiResponse<UnitObservationDTO>> UpdateObservation(int id, UnitObservationDTO model);
        Task<ApiResponse> DeleteObservation(int id);
        Task<ApiResponse<ObservationTypeDTO>> AddObservationType(ObservationTypeDTO model);
        Task<ApiResponse> DeleteObservationType(int id);
        Task<ApiResponse<List<ObservationTypeDTO>>> GetAllObservationTypes();
        Task<ApiResponse<ObservationTypeDTO>> UpdateObservationType(int id, ObservationTypeDTO model);
        Task<ApiResponse<ObservationResponseDTO>> AddObservationResponse(int id, ObservationResponseDTO model);
        Task<ApiResponse> AddObservationResponseImages(int id, IFormFile[] files);
        Task<ApiResponse<List<UnitObservationDTO>>> GetAllObservationsByUnitId(int id);
        Task<ApiResponse<List<UnitObservationDTO>>> GetAllObservations();
        Task<ApiResponse<GetObservationById>> GetObservationById(int id);

        
        //Sections Part
        Task<ApiResponse<SectionDTO>> AddSection(SectionDTO model);
        Task<ApiResponse> DeleteSection(int id);
        Task<ApiResponse<SectionDTO>> UpdateSection(int id, SectionDTO model);
        Task<ApiResponse<List<SectionDTO>>> GetAllSections();
        Task<ApiResponse<List<UnitDTO>>> GetAllSectionUnits(int id);
        Task<ApiResponse<List<UnitDTO>>> GetAllDoneSectionUnits(int id);
        //Task<ApiResponse<List<AppUser>>> GetAllUsersPerSection(int id);

        
        //Building Part
        Task<ApiResponse<BuildingDTO>> AddBuilding(int id, BuildingDTO model);
        Task<ApiResponse> DeleteBuilding(int id);
        Task<ApiResponse<BuildingDTO>> UpdateBuilding(int id, BuildingDTO model);
        Task<ApiResponse<List<BuildingDTO>>> GetAllBuildings();
        Task<ApiResponse<List<BuildingDTO>>> GetAllBuildingsPerSection(int id);
        Task<ApiResponse<List<UnitDTO>>> GetAllBuildingUnits(int id);
        Task<ApiResponse<List<UnitDTO>>> GetAllDoneBuildingUnits(int id);
        //Task<ApiResponse<List<AppUser>>> GetAllUsersPerBuilding(int id);


        
        //Floar Part
        Task<ApiResponse<FloarDTO>> AddFloar(int id, FloarDTO model);
        Task<ApiResponse> DeleteFloar(int id);
        Task<ApiResponse<FloarDTO>> UpdateFloar(int id, FloarDTO model);
        Task<ApiResponse<List<FloarDTO>>> GetAllFloars();
        Task<ApiResponse<List<FloarDTO>>> GetAllFloarsPerBuilding(int id);
        Task<ApiResponse<List<UnitDTO>>> GetAllFloarUnits(int id);
        Task<ApiResponse<List<UnitDTO>>> GetAllDoneFloarUnits(int id);


        
        //Rooms Part
        Task<ApiResponse<List<RoomDTO>>> AddRoomsToUnit(int id, List<RoomDTO> models);
        Task<ApiResponse> DeleteRoom(int id);
        Task<ApiResponse<RoomDTO>> UpdateRoom(int id, RoomDTO model);
        Task<ApiResponse<List<RoomDTO>>> GetAllUnitRooms(int id);


        //Room Type Part
        Task<ApiResponse<RoomTypeDTO>> AddRoomType(RoomTypeDTO model);
        Task<ApiResponse> DeleteRoomType(int id);
        Task<ApiResponse<List<RoomTypeDTO>>> GetAllRoomTypes();
        Task<ApiResponse<RoomTypeDTO>> UpdateRoomType(int id, RoomTypeDTO model);



        //Unit Type Part
        Task<ApiResponse<UnitTypeDTO>> AddUnitType(UnitTypeDTO models);
        Task<ApiResponse> DeleteUnitType(int id);
        Task<ApiResponse<UnitTypeDTO>> UpdateUnitType(int id, UnitTypeDTO model);
        Task<ApiResponse<List<UnitTypeDTO>>> GetAllUnitTypes();
        Task<ApiResponse<List<UnitDTO>>> GetAllUnitsByTypeId(int id);


        //Unit Status
        Task<ApiResponse<UnitStatusDTO>> AddUnitStatus(UnitStatusDTO model);


        Task<ApiResponse<List<UnitStatusDTO>>> GetAllUnitStatus(int id);


        //Unit Progress Bar (Statistics)
        Task<ApiResponse<StatisticsModel>> UnitsStatistics();
        Task<ApiResponse<StatisticsModel>> ObservationsStatistics();
    }
}
