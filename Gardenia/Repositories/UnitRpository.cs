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
    public class UnitRpository : IUnitRepository
    {
        private readonly AppDbContext _context;
        private readonly IBasicServices _services;
        private readonly IMapper _mapper;
        public UnitRpository(AppDbContext context, IMapper mapper, IBasicServices services)
        {
            _context = context;
            _services = services;
            _mapper = mapper;
        }

        public async Task<ApiResponse<UnitObservationDTO>> AddObservation(UnitObservationDTO model)
        {
            var response = new ApiResponse<UnitObservationDTO>();


            var unit = _context.Units.SingleOrDefault(u => u.Id == model.UnitID);
            if (unit == null)
            {
                response.Status = false;
                response.Message = "This unit is not exist";

                return response;
            }


            var type = _context.ObservationTypes.SingleOrDefault(r => r.Id == model.ObservationTypeID);
            if (type == null)
            {
                response.Status = false;
                response.Message = "This report type is not exist";

                return response;
            }
            var compound = await _context.Compounds.FirstOrDefaultAsync();

            ++compound.ObservationCounter;
            model.Date = DateTime.UtcNow.AddHours(2);

            var report = _mapper.Map<UnitObservationDTO, UnitObservation>(model);
            await _context.UnitObservations.AddAsync(report);
            await _context.SaveChangesAsync();

            model.Id = report.Id;

            response.Status = true;
            response.Message = "Success";
            response.Response = model;

            return response;
        }

        public async Task<ApiResponse> AddObservationImages(int id, IFormFile[] files)
        {
            var response = new ApiResponse();
            var observation = _context.UnitObservations.SingleOrDefault(o => o.Id == id);
            if (observation == null)
            {
                response.Status = false;
                response.Message = "This report is not exist";

                return response;
            }

            var observationImages = _context.ObservationImages.Where(oi => oi.ObservationID == id).ToList();
            if (observationImages != null)
            {
                _context.ObservationImages.RemoveRange(observationImages);
                await _context.SaveChangesAsync();
            }


            foreach (var file in files)
            {
                var Image = new ObservationImagesDTO()
                {
                    ObservationImage = await _services.UploadPhoto(file),
                    ObservationID = id
                };

                var ImageInDb = _mapper.Map<ObservationImagesDTO, ObservationImages>(Image);
                _context.ObservationImages.Add(ImageInDb);
                await _context.SaveChangesAsync();
            }

            response.Status = true;
            response.Message = "Success";

            return response;
        }



        public async Task<ApiResponse<UnitObservationDTO>> UpdateObservation(int id, UnitObservationDTO model)
        {
            var response = new ApiResponse<UnitObservationDTO>();

            var observation = await _context.UnitObservations.FindAsync(id);

            if (observation == null)
            {
                response.Status = false;
                response.Message = "This Observation is not exist";
                return response;
            }


            model.Id = id;

            _mapper.Map(model, observation);

            _context.UnitObservations.Update(observation);
            await _context.SaveChangesAsync();


            response.Status = true;
            response.Message = "Success";
            response.Response = model;


            return response;
        }


        public async Task<ApiResponse> DeleteObservation(int id)
        {
            var response = new ApiResponse();
            var observation = await _context.UnitObservations.FindAsync(id);
            if (observation == null)
            {
                response.Status = false;
                response.Message = "This Observation is not exist!";
                return response;
            }

            var compound = await _context.Compounds.FirstOrDefaultAsync();

            --compound.ObservationCounter;

            _context.UnitObservations.Remove(observation);
            await _context.SaveChangesAsync();

            response.Status = true;
            response.Message = "The Unit is successfully deleted";
            return response;
        }

        public async Task<ApiResponse<ObservationTypeDTO>> AddObservationType(ObservationTypeDTO model)
        {
            var response = new ApiResponse<ObservationTypeDTO>();
            var report = _mapper.Map<ObservationTypeDTO, ObservationType>(model);
            await _context.ObservationTypes.AddAsync(report);
            await _context.SaveChangesAsync();

            model.Id = report.Id;

            response.Status = true;
            response.Message = "Success";
            response.Response = model;

            return response;
        }


        public async Task<ApiResponse> DeleteObservationType(int id)
        {
            var response = new ApiResponse();
            var reportType = await _context.ObservationTypes.SingleOrDefaultAsync(r => r.Id == id);
            if (reportType == null)
            {
                response.Status = false;
                response.Message = "This Observation type is not exist";

                return response;
            }

            _context.ObservationTypes.Remove(reportType);
            await _context.SaveChangesAsync();

            response.Status = true;
            response.Message = "Observation type is successfully deleted";

            return response;
        }


        public async Task<ApiResponse<ObservationTypeDTO>> UpdateObservationType(int id, ObservationTypeDTO model)
        {
            var response = new ApiResponse<ObservationTypeDTO>();

            var reportType = _context.ObservationTypes.SingleOrDefault(p => p.Id == id);
            if (reportType == null)
            {
                response.Status = false;
                response.Message = "This Observation Type is not exist";

                return response;
            }


            model.Id = id;

            _mapper.Map(model, reportType);

            _context.ObservationTypes.Update(reportType);
            await _context.SaveChangesAsync();


            response.Status = true;
            response.Message = "Success";
            response.Response = model;


            return response;
        }


        public async Task<ApiResponse<List<ObservationTypeDTO>>> GetAllObservationTypes()
        {
            var types = _context.ObservationTypes.Select(_mapper.Map<ObservationType, ObservationTypeDTO>).ToList();
            var response = new ApiResponse<List<ObservationTypeDTO>>();

            if (types == null)
            {
                response.Status = false;
                response.Message = "There is no report types yet!";

                return response;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = types;

            return response;
        }


        public async Task<ApiResponse<List<UnitDTO>>> AddUnits(List<UnitDTO> models)
        {
            var response = new ApiResponse<List<UnitDTO>>();

            var unitsInDb = _context.Units.ToList();
            foreach (var unit in unitsInDb)
            {
                foreach (var model in models)
                {
                    if (unit.SectionID == model.SectionID
                        && unit.BuildingID == model.BuildingID
                        && unit.FloarID == model.FloarID
                        && unit.UnitNumber.Trim().ToUpper().Equals(model.UnitNumber.Trim().ToUpper()))
                    {
                        response.Status = false;
                        response.Message = "This unit is already exist";
                        return response;
                    }
                }
            }
            var compound = await _context.Compounds.FirstOrDefaultAsync();
            foreach (var model in models)
            {
                ++compound.UnitsCounter;
                var unit = _mapper.Map<UnitDTO, Unit>(model);
                await _context.Units.AddAsync(unit);
                await _context.SaveChangesAsync();

                model.Id = unit.Id;
            }


            response.Status = true;
            response.Message = "Success";
            response.Response = models;

            return response;
        }


        public async Task<ApiResponse> DeleteUnit(int id)
        {
            var response = new ApiResponse();
            var unit = await _context.Units.FindAsync(id);
            if (unit == null)
            {
                response.Status = false;
                response.Message = "This unit is not exist";
                return response;
            }

            var compound = await _context.Compounds.FirstOrDefaultAsync();
            --compound.UnitsCounter;
            _context.Units.Remove(unit);
            await _context.SaveChangesAsync();

            response.Status = true;
            response.Message = "The Unit is successfully deleted";
            return response;
        }

        public async Task<ApiResponse<List<UnitDTO>>> GetAllUnits()
        {
            var units = _context.Units
                .Include(u => u.Section)
                .Include(u => u.UnitType)
                .Include(u => u.Building)
                .Include(u => u.Floar)
                .Select(_mapper.Map<Unit, UnitDTO>).ToList();
            var response = new ApiResponse<List<UnitDTO>>();

            if (units == null)
            {
                response.Status = true;
                response.Message = "There is no Units yet!";

                return response;
            }



            response.Status = true;
            response.Message = "Success";
            response.Response = units;

            return response;
        }

        public async Task<ApiResponse<UnitDTO>> GetUnitById(int id)
        {
            var unit = await _context.Units
                .Include(u => u.Section)
                .Include(u => u.UnitType)
                .Include(u => u.Building)
                .Include(u => u.Floar)
                .SingleOrDefaultAsync(u => u.Id == id);
            var response = new ApiResponse<UnitDTO>();

            if (unit == null)
            {
                response.Status = true;
                response.Message = "This unit is not exist!";

                return response;
            }

            var unitDto = _mapper.Map<Unit, UnitDTO>(unit);

            response.Status = true;
            response.Message = "Success";
            response.Response = unitDto;

            return response;
        }

        public async Task<ApiResponse<UnitDTO>> UpdateUnit(int id, UnitDTO model)
        {
            var response = new ApiResponse<UnitDTO>();

            var unit = await _context.Units.FindAsync(id);

            if (unit == null)
            {
                response.Status = false;
                response.Message = "This Unit is not exist";
                return response;
            }


            model.Id = id;

            _mapper.Map(model, unit);

            _context.Units.Update(unit);
            await _context.SaveChangesAsync();


            response.Status = true;
            response.Message = "Success";
            response.Response = model;


            return response;
        }


        public async Task<ApiResponse> UnitAcceptConfirmation(int id, NewsStatus model)
        {
            var response = new ApiResponse();

            var unit = await _context.Units.FindAsync(id);
            if (unit == null)
            {
                response.Status = false;
                response.Message = "This is no News yet!";

                return response;
            }

            if (model.Status == true)
            {
                //Unit Acceptance Confirmation
                unit.Status = 0;
            }
            _context.Units.Attach(unit);
            var isActive = _context.Entry(unit).Property(n => n.Status).IsModified == true;
            await _context.SaveChangesAsync();

            response.Status = true;
            response.Message = "Success";

            return response;
        }


        public async Task<ApiResponse<ObservationResponseDTO>> AddObservationResponse(int id, ObservationResponseDTO model)
        {
            var response = new ApiResponse<ObservationResponseDTO>();

            var report = _context.UnitObservations.SingleOrDefault(r => r.Id == id);
            if (report == null || report.Status == 1)
            {
                response.Status = false;
                response.Message = "This Report is not exist!";
                if (report.Status == 1)
                    response.Message = "This Report is Finished Before";
                return response;
            }

            model.Date = DateTime.UtcNow.AddHours(2);
            model.ObservationID = id;
            report.Status = model.Status;

            if (model.Status == 1)
            {
                var compound = await _context.Compounds.FirstOrDefaultAsync();
                ++compound.DoneObservationCounter;
            }

            var reportResponse = _mapper.Map<ObservationResponseDTO, ObservationResponse>(model);
            await _context.ObservationResponses.AddAsync(reportResponse);
            await _context.SaveChangesAsync();

            model.Id = reportResponse.Id;

            response.Status = true;
            response.Message = "Success";
            response.Response = model;

            return response;
        }

        public async Task<ApiResponse> AddObservationResponseImages(int id, IFormFile[] files)
        {
            var response = new ApiResponse();
            var reportResponse = _context.ObservationResponses.SingleOrDefault(o => o.Id == id);
            if (reportResponse == null)
            {
                response.Status = false;
                response.Message = "This Response is not exist";

                return response;
            }

            foreach (var file in files)
            {
                var responseImage = new ObservationResponseImagesDTO()
                {
                    RespnseImage = await _services.UploadPhoto(file),
                    ResponseID = id
                };

                var responseImageInDb = _mapper.Map<ObservationResponseImagesDTO, ObservationResponseImages>(responseImage);
                _context.ObservationResponseImages.Add(responseImageInDb);
                await _context.SaveChangesAsync();
            }

            response.Status = true;
            response.Message = "Success";

            return response;
        }

        public async Task<ApiResponse<List<UnitObservationDTO>>> GetAllObservationsByUnitId(int id)
        {
            var observations = _context.UnitObservations.Where(u => u.UnitID == id).Select(_mapper.Map<UnitObservation, UnitObservationDTO>).ToList();
            var response = new ApiResponse<List<UnitObservationDTO>>();

            if (observations == null)
            {
                response.Status = true;
                response.Message = "There is no Observations for this Unit yet!";

                return response;
            }

            foreach (var observation in observations)
            {
                observation.Images = _context.ObservationImages
                    .Where(oi => oi.ObservationID == observation.Id)
                    .Select(_mapper.Map<ObservationImages, ObservationImagesDTO>)
                    .ToList();
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = observations;

            return response;
        }

        public async Task<ApiResponse<List<UnitObservationDTO>>> GetAllObservations()
        {
            var observations = _context.UnitObservations.Select(_mapper.Map<UnitObservation, UnitObservationDTO>).ToList();
            var response = new ApiResponse<List<UnitObservationDTO>>();

            if (observations == null)
            {
                response.Status = true;
                response.Message = "There is no Observations yet!";

                return response;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = observations;

            return response;
        }


        public async Task<ApiResponse<GetObservationById>> GetObservationById(int id)
        {
            var observation = await _context.UnitObservations.Include(u => u.Unit.AppUser).SingleOrDefaultAsync(o => o.Id == id);
            var response = new ApiResponse<GetObservationById>();

            if (observation == null)
            {
                response.Status = false;
                response.Message = "This report is not exist";

                return response;
            }

            var observationDTO = _mapper.Map<UnitObservation, GetObservationById>(observation);


            observationDTO.Images = _context.ObservationImages.Where(i => i.ObservationID == id).Select(_mapper.Map<ObservationImages, ObservationImagesDTO>).ToList();
            observationDTO.Responses = _context.ObservationResponses.Where(r => r.ObservationID == id).Select(_mapper.Map<ObservationResponse, ObservationResponseDTO>).ToList();
            foreach (var Response in observationDTO.Responses)
            {
                var reportResponseImages = _context.ObservationResponseImages.Where(i => i.ResponseID == Response.Id).Select(_mapper.Map<ObservationResponseImages, ObservationResponseImagesDTO>).ToList();
                Response.ObservationResponseImages = reportResponseImages;
            }

            var userUnit = observation.Unit.AppUser;

            observationDTO.UserID = userUnit.Id;
            observationDTO.UserName = userUnit.Name;
            observationDTO.UserImage = userUnit.Image;
            observationDTO.UserEmail = userUnit.Email;
            observationDTO.UserPhone = userUnit.PhoneNumber;

            response.Status = true;
            response.Message = "Success";
            response.Response = observationDTO;

            return response;
        }

        //Section Part
        public async Task<ApiResponse<SectionDTO>> AddSection(SectionDTO model)
        {
            var response = new ApiResponse<SectionDTO>();

            var sections = _context.Sections.Select(_mapper.Map<Section, SectionDTO>).ToList();
            foreach (var oneSection in sections)
            {
                if (oneSection.Name.Trim().ToUpper().Equals(model.Name.Trim().ToUpper()))
                {
                    response.Status = false;
                    response.Message = "This Section is already exist!";

                    return response;
                }
            }

            var section = _mapper.Map<SectionDTO, Section>(model);
            await _context.Sections.AddAsync(section);
            await _context.SaveChangesAsync();

            model.Id = section.Id;

            response.Status = true;
            response.Message = "Success";
            response.Response = model;

            return response;
        }


        public async Task<ApiResponse> DeleteSection(int id)
        {
            var response = new ApiResponse();
            var section = await _context.Sections.FindAsync(id);
            if (section == null)
            {
                response.Status = false;
                response.Message = "This Section is not exist";
                return response;
            }

            _context.Sections.Remove(section);
            await _context.SaveChangesAsync();

            response.Status = true;
            response.Message = "Section is successfully deleted";
            return response;
        }


        public async Task<ApiResponse<List<SectionDTO>>> GetAllSections()
        {
            var sections = _context.Sections.Select(_mapper.Map<Section, SectionDTO>).ToList();
            var response = new ApiResponse<List<SectionDTO>>();

            if (sections == null)
            {
                response.Status = true;
                response.Message = "There is no Sections yet!";

                return response;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = sections;

            return response;
        }


        public async Task<ApiResponse<SectionDTO>> UpdateSection(int id, SectionDTO model)
        {
            var response = new ApiResponse<SectionDTO>();

            var section = _context.Sections.SingleOrDefault(p => p.Id == id);
            if (section == null)
            {
                response.Status = false;
                response.Message = "This Section is not exist";

                return response;
            }

            model.Id = id;

            _mapper.Map(model, section);

            _context.Sections.Update(section);
            await _context.SaveChangesAsync();


            response.Status = true;
            response.Message = "Success";
            response.Response = model;

            return response;
        }


        public async Task<ApiResponse<List<UnitDTO>>> GetAllSectionUnits(int id)
        {
            var response = new ApiResponse<List<UnitDTO>>();
            var section = await _context.Sections.FirstOrDefaultAsync(p => p.Id == id);
            if (section == null)
            {
                response.Status = false;
                response.Message = "This section is not exist!";

                return response;
            }

            var units = _context.Units.Include(u => u.Section)
                .Include(u => u.UnitType)
                .Include(u => u.Building)
                .Include(u => u.Floar)
                .Where(a => a.SectionID == id)
                .Select(_mapper.Map<Unit, UnitDTO>).ToList();

            if (units == null)
            {
                response.Status = true;
                response.Message = "There is no units yet in this section!";

                return response;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = units;

            return response;
        }


        public async Task<ApiResponse<List<UnitDTO>>> GetAllDoneSectionUnits(int id)
        {
            var response = new ApiResponse<List<UnitDTO>>();
            var section = await _context.Sections.FirstOrDefaultAsync(p => p.Id == id);
            if (section == null)
            {
                response.Status = false;
                response.Message = "This section is not exist!";

                return response;
            }

            var units = _context.Units.Include(u => u.Section)
                .Include(u => u.UnitType)
                .Include(u => u.Building)
                .Include(u => u.Floar)
                .Where(a => a.SectionID == id && a.Status == 1)
                .Select(_mapper.Map<Unit, UnitDTO>).ToList();

            if (units == null)
            {
                response.Status = true;
                response.Message = "There is no units yet in this section!";

                return response;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = units;

            return response;
        }


        public async Task<ApiResponse<List<AppUser>>> GetAllUsersPerSection(int id)
        {
            var response = new ApiResponse<List<AppUser>>();
            var section = await _context.Sections.FirstOrDefaultAsync(p => p.Id == id);
            if (section == null)
            {
                response.Status = false;
                response.Message = "This section is not exist!";

                return response;
            }

            var units = _context.Units.Include(U => U.AppUser).Where(a => a.SectionID == id).ToList();

            if (units == null)
            {
                response.Status = true;
                response.Message = "There is no units yet in this section!";

                return response;
            }

            List<AppUser> usersPerSection = new();
            foreach (var unit in units)
            {
                usersPerSection.Add(unit.AppUser);
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = usersPerSection;

            return response;
        }



        //Building Part
        public async Task<ApiResponse<BuildingDTO>> AddBuilding(int id, BuildingDTO model)
        {
            var response = new ApiResponse<BuildingDTO>();

            var section = _context.Sections.SingleOrDefault(p => p.Id == id);
            if (section == null)
            {
                response.Status = false;
                response.Message = "This Section is not exist";

                return response;
            }


            var buildings = _context.Buildings.Where(b => b.SectionID == id).Select(_mapper.Map<Building, BuildingDTO>).ToList();
            foreach (var oneBuilding in buildings)
            {
                if (oneBuilding.Name.Trim().ToUpper().Equals(model.Name.Trim().ToUpper()))
                {
                    response.Status = false;
                    response.Message = "This Building is already exist!";

                    return response;
                }
            }

            model.SectionID = id;
            var building = _mapper.Map<BuildingDTO, Building>(model);
            await _context.Buildings.AddAsync(building);
            await _context.SaveChangesAsync();

            model.Id = building.Id;

            response.Status = true;
            response.Message = "Success";
            response.Response = model;

            return response;
        }


        public async Task<ApiResponse> DeleteBuilding(int id)
        {
            var response = new ApiResponse();
            var building = await _context.Buildings.FindAsync(id);
            if (building == null)
            {
                response.Status = false;
                response.Message = "This Building is not exist";
                return response;
            }

            _context.Buildings.Remove(building);
            await _context.SaveChangesAsync();

            response.Status = true;
            response.Message = "Building is successfully deleted";
            return response;
        }


        public async Task<ApiResponse<List<BuildingDTO>>> GetAllBuildings()
        {
            var buildings = _context.Buildings.Select(_mapper.Map<Building, BuildingDTO>).ToList();
            var response = new ApiResponse<List<BuildingDTO>>();

            if (buildings == null)
            {
                response.Status = true;
                response.Message = "There is no Buildings yet!";

                return response;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = buildings;

            return response;
        }


        public async Task<ApiResponse<BuildingDTO>> UpdateBuilding(int id, BuildingDTO model)
        {
            var response = new ApiResponse<BuildingDTO>();

            var building = _context.Buildings.SingleOrDefault(p => p.Id == id);
            if (building == null)
            {
                response.Status = false;
                response.Message = "This Building is not exist";

                return response;
            }

            model.Id = id;

            _mapper.Map(model, building);

            _context.Buildings.Update(building);
            await _context.SaveChangesAsync();


            response.Status = true;
            response.Message = "Success";
            response.Response = model;

            return response;
        }


        public async Task<ApiResponse<List<BuildingDTO>>> GetAllBuildingsPerSection(int id)
        {
            var response = new ApiResponse<List<BuildingDTO>>();

            var section = await _context.Sections.FirstOrDefaultAsync(s => s.Id == id);

            if (section == null)
            {
                response.Status = true;
                response.Message = "This section is not exist!";

                return response;
            }

            var buildings = _context.Buildings.Where(b => b.SectionID == id).Select(_mapper.Map<Building, BuildingDTO>).ToList();

            if (buildings == null)
            {
                response.Status = true;
                response.Message = "There is no  buildings in this Section yet!";

                return response;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = buildings;

            return response;
        }


        public async Task<ApiResponse<List<UnitDTO>>> GetAllBuildingUnits(int id)
        {
            var response = new ApiResponse<List<UnitDTO>>();
            var building = await _context.Buildings.FirstOrDefaultAsync(p => p.Id == id);
            if (building == null)
            {
                response.Status = false;
                response.Message = "This building is not exist!";

                return response;
            }

            var units = _context.Units.Include(u => u.Section)
                .Include(u => u.UnitType)
                .Include(u => u.Building)
                .Include(u => u.Floar)
                .Where(a => a.BuildingID == id)
                .Select(_mapper.Map<Unit, UnitDTO>).ToList();

            if (units == null)
            {
                response.Status = true;
                response.Message = "There is no units yet in this building!";

                return response;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = units;

            return response;
        }


        public async Task<ApiResponse<List<UnitDTO>>> GetAllDoneBuildingUnits(int id)
        {
            var response = new ApiResponse<List<UnitDTO>>();
            var building = await _context.Buildings.FirstOrDefaultAsync(p => p.Id == id);
            if (building == null)
            {
                response.Status = false;
                response.Message = "This building is not exist!";

                return response;
            }

            var units = _context.Units.Include(u => u.Section)
                .Include(u => u.UnitType)
                .Include(u => u.Building)
                .Include(u => u.Floar)
                .Where(a => a.BuildingID == id && a.Status == 1)
                .Select(_mapper.Map<Unit, UnitDTO>).ToList();

            if (units == null)
            {
                response.Status = true;
                response.Message = "There is no units yet in this section!";

                return response;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = units;

            return response;
        }


        //Floar Part
        public async Task<ApiResponse<FloarDTO>> AddFloar(int id, FloarDTO model)
        {
            var response = new ApiResponse<FloarDTO>();

            var building = _context.Buildings.SingleOrDefault(p => p.Id == id);
            if (building == null)
            {
                response.Status = false;
                response.Message = "This building is not exist";

                return response;
            }

            var floars = _context.Floars.Where(b => b.BuildingID == id).Select(_mapper.Map<Floar, FloarDTO>).ToList();
            foreach (var oneFloar in floars)
            {
                if (oneFloar.Name.Trim().ToUpper().Equals(model.Name.Trim().ToUpper()))
                {
                    response.Status = false;
                    response.Message = "This Floar is already exist!";

                    return response;
                }
            }

            model.BuildingID = id;
            var floar = _mapper.Map<FloarDTO, Floar>(model);
            await _context.Floars.AddAsync(floar);
            await _context.SaveChangesAsync();

            model.Id = building.Id;

            response.Status = true;
            response.Message = "Success";
            response.Response = model;

            return response;
        }


        public async Task<ApiResponse> DeleteFloar(int id)
        {
            var response = new ApiResponse();
            var floar = await _context.Floars.FindAsync(id);
            if (floar == null)
            {
                response.Status = false;
                response.Message = "This floar is not exist";
                return response;
            }

            _context.Floars.Remove(floar);
            await _context.SaveChangesAsync();

            response.Status = true;
            response.Message = "Floar is successfully deleted";
            return response;
        }


        public async Task<ApiResponse<List<FloarDTO>>> GetAllFloars()
        {
            var floars = _context.Floars.Select(_mapper.Map<Floar, FloarDTO>).ToList();
            var response = new ApiResponse<List<FloarDTO>>();

            if (floars == null)
            {
                response.Status = true;
                response.Message = "There is no floars yet!";

                return response;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = floars;

            return response;
        }


        public async Task<ApiResponse<FloarDTO>> UpdateFloar(int id, FloarDTO model)
        {
            var response = new ApiResponse<FloarDTO>();

            var floar = _context.Floars.SingleOrDefault(p => p.Id == id);
            if (floar == null)
            {
                response.Status = false;
                response.Message = "This floar is not exist";

                return response;
            }

            model.Id = id;

            _mapper.Map(model, floar);

            _context.Floars.Update(floar);
            await _context.SaveChangesAsync();


            response.Status = true;
            response.Message = "Success";
            response.Response = model;

            return response;
        }


        public async Task<ApiResponse<List<FloarDTO>>> GetAllFloarsPerBuilding(int id)
        {
            var response = new ApiResponse<List<FloarDTO>>();

            var building = await _context.Buildings.FirstOrDefaultAsync(s => s.Id == id);

            if (building == null)
            {
                response.Status = true;
                response.Message = "This building is not exist!";

                return response;
            }

            var floars = _context.Floars.Where(b => b.BuildingID == id).Select(_mapper.Map<Floar, FloarDTO>).ToList();

            if (floars == null)
            {
                response.Status = true;
                response.Message = "There is no  floars in this Building yet!";

                return response;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = floars;

            return response;
        }


        public async Task<ApiResponse<List<UnitDTO>>> GetAllFloarUnits(int id)
        {
            var response = new ApiResponse<List<UnitDTO>>();
            var floar = await _context.Floars.FirstOrDefaultAsync(p => p.Id == id);
            if (floar == null)
            {
                response.Status = false;
                response.Message = "This floar is not exist!";

                return response;
            }

            var units = _context.Units.Include(u => u.Section)
                .Include(u => u.UnitType)
                .Include(u => u.Building)
                .Include(u => u.Floar)
                .Where(a => a.FloarID == id)
                .Select(_mapper.Map<Unit, UnitDTO>).ToList();

            if (units == null)
            {
                response.Status = true;
                response.Message = "There is no units yet in this Floar!";

                return response;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = units;

            return response;
        }


        public async Task<ApiResponse<List<UnitDTO>>> GetAllDoneFloarUnits(int id)
        {
            var response = new ApiResponse<List<UnitDTO>>();
            var floar = await _context.Floars.FirstOrDefaultAsync(p => p.Id == id);
            if (floar == null)
            {
                response.Status = false;
                response.Message = "This floar is not exist!";

                return response;
            }

            var units = _context.Units.Include(u => u.Section)
                .Include(u => u.UnitType)
                .Include(u => u.Building)
                .Include(u => u.Floar)
                .Where(a => a.FloarID == id && a.Status == 1)
                .Select(_mapper.Map<Unit, UnitDTO>).ToList();

            if (units == null)
            {
                response.Status = true;
                response.Message = "There is no units yet in this section!";

                return response;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = units;

            return response;
        }


        //Rooms Part
        public async Task<ApiResponse<List<RoomDTO>>> AddRoomsToUnit(int id, List<RoomDTO> models)
        {
            var response = new ApiResponse<List<RoomDTO>>();
            var unit = _context.Units.SingleOrDefault(p => p.Id == id);
            if (unit == null)
            {
                response.Status = false;
                response.Message = "This unit is not exist!";

                return response;
            }

            foreach (var model in models)
            {

                model.UnitID = id;
                //if(decimal.Parse(model.Area) > decimal.Parse(unit.TotalArea))
                //{
                //    response.Status = false;
                //    response.Message = "Wrong Area";

                //    return response;
                //}
                var room = _mapper.Map<RoomDTO, Room>(model);
                await _context.Rooms.AddAsync(room);
                await _context.SaveChangesAsync();

                model.Id = room.Id;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = models;

            return response;
        }


        public async Task<ApiResponse<List<RoomDTO>>> GetAllUnitRooms(int id)
        {
            var response = new ApiResponse<List<RoomDTO>>();

            var unit = await _context.Units.FirstOrDefaultAsync(s => s.Id == id);

            if (unit == null)
            {
                response.Status = true;
                response.Message = "This unit is not exist!";

                return response;
            }

            var rooms = _context.Rooms.Include(r => r.RoomType).Where(b => b.UnitID == id).Select(_mapper.Map<Room, RoomDTO>).ToList();

            if (rooms == null)
            {
                response.Status = true;
                response.Message = "There is no  rooms in this Unit yet!";

                return response;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = rooms;

            return response;
        }


        public async Task<ApiResponse<RoomDTO>> UpdateRoom(int id, RoomDTO model)
        {
            var response = new ApiResponse<RoomDTO>();

            var room = _context.Rooms.SingleOrDefault(p => p.Id == id);
            if (room == null)
            {
                response.Status = false;
                response.Message = "This room is not exist";

                return response;
            }

            model.Id = id;

            _mapper.Map(model, room);

            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();


            response.Status = true;
            response.Message = "Success";
            response.Response = model;

            return response;
        }


        public async Task<ApiResponse> DeleteRoom(int id)
        {
            var response = new ApiResponse();
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                response.Status = false;
                response.Message = "This room is not exist";
                return response;
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            response.Status = true;
            response.Message = "Room is successfully deleted";
            return response;
        }


        //Room Type Part
        public async Task<ApiResponse<RoomTypeDTO>> AddRoomType(RoomTypeDTO model)
        {
            var response = new ApiResponse<RoomTypeDTO>();
            var roomType = _mapper.Map<RoomTypeDTO, RoomType>(model);
            await _context.RoomTypes.AddAsync(roomType);
            await _context.SaveChangesAsync();

            model.Id = roomType.Id;

            response.Status = true;
            response.Message = "Success";
            response.Response = model;

            return response;
        }


        public async Task<ApiResponse> DeleteRoomType(int id)
        {
            var response = new ApiResponse();
            var roomType = await _context.RoomTypes.SingleOrDefaultAsync(r => r.Id == id);
            if (roomType == null)
            {
                response.Status = false;
                response.Message = "This Room type is not exist";

                return response;
            }

            _context.RoomTypes.Remove(roomType);
            await _context.SaveChangesAsync();

            response.Status = true;
            response.Message = "Room type is successfully deleted";

            return response;
        }


        public async Task<ApiResponse<RoomTypeDTO>> UpdateRoomType(int id, RoomTypeDTO model)
        {
            var response = new ApiResponse<RoomTypeDTO>();

            var reportType = _context.RoomTypes.SingleOrDefault(p => p.Id == id);
            if (reportType == null)
            {
                response.Status = false;
                response.Message = "This Room Type is not exist";

                return response;
            }


            model.Id = id;

            _mapper.Map(model, reportType);

            _context.RoomTypes.Update(reportType);
            await _context.SaveChangesAsync();


            response.Status = true;
            response.Message = "Success";
            response.Response = model;


            return response;
        }


        public async Task<ApiResponse<List<RoomTypeDTO>>> GetAllRoomTypes()
        {
            var types = _context.RoomTypes.Select(_mapper.Map<RoomType, RoomTypeDTO>).ToList();
            var response = new ApiResponse<List<RoomTypeDTO>>();

            if (types == null)
            {
                response.Status = false;
                response.Message = "There is no room types yet!";

                return response;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = types;

            return response;
        }



        //Unit Type Part
        public async Task<ApiResponse<UnitTypeDTO>> AddUnitType(UnitTypeDTO model)
        {
            var response = new ApiResponse<UnitTypeDTO>();

            var unitType = _mapper.Map<UnitTypeDTO, UnitType>(model);
            await _context.UnitTypes.AddAsync(unitType);
            await _context.SaveChangesAsync();

            model.Id = unitType.Id;

            response.Status = true;
            response.Message = "Success";
            response.Response = model;

            return response;
        }


        public async Task<ApiResponse> DeleteUnitType(int id)
        {
            var response = new ApiResponse();
            var unitType = await _context.UnitTypes.FindAsync(id);
            if (unitType == null)
            {
                response.Status = false;
                response.Message = "This unitType is not exist";
                return response;
            }

            _context.UnitTypes.Remove(unitType);
            await _context.SaveChangesAsync();

            response.Status = true;
            response.Message = "unitType is successfully deleted";
            return response;
        }


        public async Task<ApiResponse<List<UnitTypeDTO>>> GetAllUnitTypes()
        {
            var unitTypes = _context.UnitTypes.Select(_mapper.Map<UnitType, UnitTypeDTO>).ToList();
            var response = new ApiResponse<List<UnitTypeDTO>>();

            if (unitTypes == null)
            {
                response.Status = true;
                response.Message = "There is no unitTypes yet!";

                return response;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = unitTypes;

            return response;
        }


        public async Task<ApiResponse<UnitTypeDTO>> UpdateUnitType(int id, UnitTypeDTO model)
        {
            var response = new ApiResponse<UnitTypeDTO>();

            var unitType = _context.UnitTypes.SingleOrDefault(p => p.Id == id);
            if (unitType == null)
            {
                response.Status = false;
                response.Message = "This unitType is not exist";

                return response;
            }

            model.Id = id;

            _mapper.Map(model, unitType);

            _context.UnitTypes.Update(unitType);
            await _context.SaveChangesAsync();


            response.Status = true;
            response.Message = "Success";
            response.Response = model;

            return response;
        }


        public async Task<ApiResponse<List<UnitDTO>>> GetAllUnitsByTypeId(int id)
        {
            var response = new ApiResponse<List<UnitDTO>>();
            var unitType = await _context.UnitTypes.FirstOrDefaultAsync(p => p.Id == id);
            if (unitType == null)
            {
                response.Status = false;
                response.Message = "This unitType is not exist!";

                return response;
            }

            var units = _context.Units
                .Include(u => u.Section)
                .Include(u => u.UnitType)
                .Include(u => u.Building)
                .Include(u => u.Floar)
                .Where(a => a.UnitTypeID == id)
                .Select(_mapper.Map<Unit, UnitDTO>).ToList();

            if (units == null)
            {
                response.Status = true;
                response.Message = "There is no units yet in this Type!";

                return response;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = units;

            return response;
        }

        public async Task<ApiResponse<UnitStatusDTO>> AddUnitStatus(UnitStatusDTO model)
        {
            var response = new ApiResponse<UnitStatusDTO>();


            var unit = _context.Units.SingleOrDefault(u => u.Id == model.UnitID);
            if (unit == null)
            {
                response.Status = false;
                response.Message = "This unit is not exist";

                return response;
            }

            var admin = _context.Users.SingleOrDefault(u => u.Id == model.UserID);

            if (model.Type == 0)
            {
                unit.Status = 0;
            }
            else if (model.Type == 1)
            {
                unit.Status = 1;
            }
            else if (model.Type == 2)
            {
                unit.Status = 2;
                var compound = await _context.Compounds.FirstOrDefaultAsync();
                ++compound.DoneUnitsCounter;
            }
            else
            {
                //if (model.Reason == null)
                //{
                unit.Status = 3;
                //}
                //else
                //{
                //unit.Status = 0;

                //}

            }

            if (admin != null)
                model.UserID = admin.Id;

            model.AcceptanceDate = DateTime.UtcNow.AddHours(2);

            var unitStatus = _mapper.Map<UnitStatusDTO, UnitStatus>(model);
            await _context.UnitStatus.AddAsync(unitStatus);
            await _context.SaveChangesAsync();

            _context.Units.Update(unit);
            await _context.SaveChangesAsync();

            model.Id = unitStatus.Id;


            response.Status = true;
            response.Message = "Success";
            response.Response = model;

            return response;
        }

        public async Task<ApiResponse<List<UnitStatusDTO>>> GetAllUnitStatus(int id)
        {
            var response = new ApiResponse<List<UnitStatusDTO>>();
            var unit = await _context.Units.FirstOrDefaultAsync(p => p.Id == id);
            if (unit == null)
            {
                response.Status = false;
                response.Message = "This unit is not exist!";

                return response;
            }

            var unitStatus = _context.UnitStatus.Where(us => us.UnitID == id).Select(_mapper.Map<UnitStatus, UnitStatusDTO>).ToList();
            foreach (var status in unitStatus)
            {
                var admin = _context.Users.SingleOrDefault(u => u.Id == status.UserID);
                if (admin != null)
                {
                    status.Name = admin.Name;
                    status.Image = admin.Image;
                    status.Email = admin.Email;
                    status.NationalID = admin.NationalID;
                    status.PhoneNumber = admin.PhoneNumber;
                }
            }


            response.Status = true;
            response.Message = "Success";
            response.Response = unitStatus;

            return response;
        }

        //units statistics
        public async Task<ApiResponse<StatisticsModel>> UnitsStatistics()
        {
            var response = new ApiResponse<StatisticsModel>();
            var compound = await _context.Compounds.FirstOrDefaultAsync();
            var result = (compound.DoneUnitsCounter / compound.UnitsCounter) * 100;
            var statisticsModel = new StatisticsModel()
            {
                Statistics = result + "%"
            };
            response.Status = true;
            response.Message = "Success";
            response.Response = statisticsModel;

            return response;
        }

        //observations statistics 
        public async Task<ApiResponse<StatisticsModel>> ObservationsStatistics()
        {
            var response = new ApiResponse<StatisticsModel>();
            var compound = await _context.Compounds.FirstOrDefaultAsync();
            var result = (compound.DoneObservationCounter / compound.ObservationCounter) * 100;
            var statisticsModel = new StatisticsModel()
            {
                Statistics = result + "%"
            };
            response.Status = true;
            response.Message = "Success";
            response.Response = statisticsModel;

            return response;
        }

    }
}
