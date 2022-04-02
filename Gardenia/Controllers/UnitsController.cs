using Gardenia.DTOs;
using Gardenia.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitsController : ControllerBase
    {
        private readonly IUnitRepository _repo;
        public UnitsController(IUnitRepository repo)
        {
            _repo = repo;
        }


        [HttpPost]
        [Route("addunit")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddUnits(List<UnitDTO> models)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddUnits(models));
            }

            return BadRequest();
        }


        [HttpDelete]
        [Route("deleteunit/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUnit(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.DeleteUnit(id));
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("getallunits")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUnits()
        {
            if (ModelState.IsValid)
            {
                try
                {
                return Ok(await _repo.GetAllUnits());

                }
                catch (Exception e)
                {
                    return (IActionResult)e;
                }

            }
            return BadRequest();
        }


        [HttpGet]
        [Route("getunitbyid/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUnitById(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetUnitById(id));
            }
            return BadRequest();
        }


        [HttpPut]
        [Route("updateunit/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUnit(UnitDTO model, int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.UpdateUnit(id, model));
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("unitacceptconfirmation/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UnitAcceptConfirmation(int id, NewsStatus status)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.UnitAcceptConfirmation(id, status));
            }
            return BadRequest();
        }


        [HttpPost]
        [Route("addreport")]
        //[Authorize(Roles = "User")]
        public async Task<IActionResult> AddObservation(UnitObservationDTO model , [FromForm] IFormFile[] files)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddObservation(model));
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("addreportimages/{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddObservationImages(int id, [FromForm] IFormFile[] files)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddObservationImages(id, files));
            }
            return BadRequest();
        }


        [HttpDelete]
        [Route("deleteobservation/{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteObservation(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.DeleteObservation(id));
            }
            return BadRequest();
        }


        [HttpPost]
        [Route("addreporttype")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddObservationType(ObservationTypeDTO model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddObservationType(model));
            }
            return BadRequest();
        }


        [HttpDelete]
        [Route("deletereporttype/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteObservationType(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.DeleteObservationType(id));
            }
            return BadRequest();
        }


        [HttpPut]
        [Route("updatereporttype/{id}")]
        [Authorize(Roles = UserRole.Admin)]
        public async Task<IActionResult> UpdateObservationType(ObservationTypeDTO model, int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.UpdateObservationType(id, model));
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("getallreporttypes")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetAllObservationTypes()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllObservationTypes());
            }
            return BadRequest();
        }


        [HttpPost]
        [Route("addreportresponse/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddObservationResponse(int id, ObservationResponseDTO model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddObservationResponse(id, model));
            }
            return BadRequest();
        }


        [HttpPost]
        [Route("addreportresponseimages/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddObservationResponseImages(int id, [FromForm] IFormFile[] files)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddObservationResponseImages(id, files));
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("getallunitobservations/{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetAllUnitObservations(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllObservationsByUnitId(id));
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("getallobservations")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllObservations()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllObservations());
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("getobservationbyid/{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetObservationById(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetObservationById(id));
            }
            return BadRequest();
        }

        // Section Part

        [HttpPost]
        [Route("addsection")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddSection(SectionDTO model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddSection(model));
            }

            return BadRequest();
        }


        [HttpDelete]
        [Route("deletesection/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSection(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.DeleteSection(id));
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("getallsections")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllSections()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllSections());
            }
            return BadRequest();
        }


        [HttpPut]
        [Route("updatesection/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateSection(SectionDTO model, int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.UpdateSection(id, model));
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("getallsectionunits/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllSectionUnits(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllSectionUnits(id));
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("getalldonesectionunits/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllDoneSectionUnits(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllDoneSectionUnits(id));
            }
            return BadRequest();
        }


        //Building Part

        [HttpPost]
        [Route("addbuilding/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddBuilding(int id, BuildingDTO model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddBuilding(id, model));
            }

            return BadRequest();
        }


        [HttpDelete]
        [Route("deletebuilding/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBuilding(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.DeleteBuilding(id));
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("getallbuildings")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllBuildings()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllBuildings());
            }
            return BadRequest();
        }


        [HttpPut]
        [Route("updatebuilding/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBuilding(BuildingDTO model, int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.UpdateBuilding(id, model));
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("getallbuildingspersections/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllBuildingsPerSection(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllBuildingsPerSection(id));
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("getallbuildingunits/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllBuildingUnits(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllBuildingUnits(id));
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("getalldonebuildingunits/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllDoneBuildingUnits(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllDoneBuildingUnits(id));
            }
            return BadRequest();
        }


        //Floar Part

        [HttpPost]
        [Route("addfloar/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddFloar(int id, FloarDTO model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddFloar(id, model));
            }

            return BadRequest();
        }


        [HttpDelete]
        [Route("deletefloar/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteFloar(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.DeleteFloar(id));
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("getallfloars")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllFloars()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllFloars());
            }
            return BadRequest();
        }


        [HttpPut]
        [Route("updatefloar/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateFloar(FloarDTO model, int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.UpdateFloar(id, model));
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("getallfloarsperbuilding/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllFloarsPerBuilding(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllFloarsPerBuilding(id));
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("getallfloarunits/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllFloarUnits(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllFloarUnits(id));
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("getalldonefloarunits/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllDoneFloarUnits(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllDoneFloarUnits(id));
            }
            return BadRequest();
        }


        //Rooms Part
        [HttpPost]
        [Route("addroomstounit/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRoomsToUnit(int id, List<RoomDTO> models)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddRoomsToUnit(id, models));
            }

            return BadRequest();
        }


        [HttpGet]
        [Route("getallunitrooms/{id}")]
        [Authorize(Roles = "User,Follower,Admin")]
        public async Task<IActionResult> GetAllUnitRooms(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllUnitRooms(id));
            }
            return BadRequest();
        }


        [HttpPut]
        [Route("updateroom/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRoom(RoomDTO model, int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.UpdateRoom(id, model));
            }
            return BadRequest();
        }


        [HttpDelete]
        [Route("deleteroom/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.DeleteRoom(id));
            }
            return BadRequest();
        }



        //Room Type Part
        [HttpPost]
        [Route("addroomtype")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRoomType(RoomTypeDTO model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddRoomType(model));
            }
            return BadRequest();
        }


        [HttpDelete]
        [Route("deleteroomtype/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRoomType(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.DeleteRoomType(id));
            }
            return BadRequest();
        }


        [HttpPut]
        [Route("updateroomtype/{id}")]
        [Authorize(Roles = UserRole.Admin)]
        public async Task<IActionResult> UpdateRoomType(RoomTypeDTO model, int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.UpdateRoomType(id, model));
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("getallroomtypes")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetAllRoomTypes()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllRoomTypes());
            }
            return BadRequest();
        }


        //Unit Type Part
        [HttpPost]
        [Route("addunittype")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddUnitType(UnitTypeDTO model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddUnitType(model));
            }

            return BadRequest();
        }


        [HttpDelete]
        [Route("deleteunittype/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUnitType(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.DeleteUnitType(id));
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("getallunittypes")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUnitTypes()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllUnitTypes());
            }
            return BadRequest();
        }


        [HttpPut]
        [Route("updateunittype/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUnitType(UnitTypeDTO model, int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.UpdateUnitType(id, model));
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("getallunitsbytypeid/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUnitsByTypeId(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllUnitsByTypeId(id));
            }
            return BadRequest();
        }


        //Unit Status Part
        [HttpPost]
        [Route("addunitStatus")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> AddUnitStatus(UnitStatusDTO model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddUnitStatus(model));
            }

            return BadRequest();
        }

        
        [HttpGet]
        [Route("getallunitstatus/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUnitStatus(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.GetAllUnitStatus(id));
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("getallunitstatistics")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUnitsStatistics()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.UnitsStatistics());
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("getallobservationstatistics")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllObservationsStatistics()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.ObservationsStatistics());
            }
            return BadRequest();
        }
    }
}
