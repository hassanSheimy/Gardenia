using AutoMapper;
using Gardenia.Data.Models;
using Gardenia.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<AboutGardenia, AboutGardeniaDTO>().ReverseMap();
            CreateMap<GardeniaData, GardeniaDataDTO>().ReverseMap();
            CreateMap<Media, ImagesDTO>().ReverseMap();
            CreateMap<Media, VideoDTO>().ReverseMap();
            CreateMap<Development, DevelopmentDTO>().ReverseMap();
            CreateMap<DevelopmentCategory, DevelopmentCategoryDTO>().ReverseMap();
            CreateMap<News, NewsDTO>().ReverseMap();
            CreateMap<News, NewsWithAllProperties>().ReverseMap();
            CreateMap<NewsImages, NewsImagesDTO>().ReverseMap();
            CreateMap<NewsComments, ResponseCommentDTO>().ReverseMap();
            CreateMap<AppUser, AddUserModel>().ReverseMap();
            CreateMap<AppUser, UserSearchDTO>().ReverseMap();
            CreateMap<GateLog, GateLogDTO>().ReverseMap();
            CreateMap<UserIdentity, UserIdentityDTO>().ReverseMap();
            //CreateMap<UsersUnit, AddUserToUnitModel>().ReverseMap();
            CreateMap<Unit, UnitDTO>().ReverseMap();
            CreateMap<UnitStatus, UnitStatusDTO>().ReverseMap();
            CreateMap<UnitType, UnitTypeDTO>().ReverseMap();
            CreateMap<Section, SectionDTO>().ReverseMap();
            CreateMap<Building, BuildingDTO>().ReverseMap();
            CreateMap<Floar, FloarDTO>().ReverseMap();
            CreateMap<Room, RoomDTO>().ReverseMap();
            CreateMap<RoomType, RoomTypeDTO>().ReverseMap();
            CreateMap<UnitObservation, UnitObservationDTO>().ReverseMap();
            CreateMap<UnitObservation, GetObservationById>().ReverseMap();
            CreateMap<ObservationType, ObservationTypeDTO>().ReverseMap();
            CreateMap<ObservationImages, ObservationImagesDTO>().ReverseMap();
            CreateMap<ObservationResponse, ObservationResponseDTO>().ReverseMap();
            CreateMap<ObservationResponseImages, ObservationResponseImagesDTO>().ReverseMap();
            CreateMap<PublicTraffic, PublicTrafficDTO>().ReverseMap();
            CreateMap<NormalPublicTraffic, NormalPublicTrafficDTO>().ReverseMap();
            CreateMap<Police, PoliceDTO>().ReverseMap();
            CreateMap<Electricity, ElectricityDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Order, GetOrderById>().ReverseMap();
            CreateMap<OrderImages, OrderImagesDTO>().ReverseMap();
            CreateMap<OrderResponse, OrderResponseDTO>().ReverseMap();
            CreateMap<OrderResponseImages, OrderResponseImagesDTO>().ReverseMap();
            CreateMap<ReportType, ReportTypeDTO>().ReverseMap();
            CreateMap<Report, ReportDTO>().ReverseMap();
            CreateMap<Report, GetReportById>().ReverseMap();
            CreateMap<ReportImages, ReportImagesDTO>().ReverseMap();
            CreateMap<ReportResponse, ReportResponseDTO>().ReverseMap();
            CreateMap<ReportResponseImages, ReportResponseImagesDTO>().ReverseMap();
            CreateMap<MaintainanceType, MaintainanceTypeDTO>().ReverseMap();
            CreateMap<Maintainance, MaintainanceDTO>().ReverseMap();
            CreateMap<Maintainance, GetMaintainanceById>().ReverseMap();
            CreateMap<MaintainanceImages, MaintainanceImagesDTO>().ReverseMap();
            CreateMap<MaintainanceResponse, MaintainanceResponseDTO>().ReverseMap();
            CreateMap<MaintainanceResponseImages, MaintainanceResponseImagesDTO>().ReverseMap();

            CreateMap<Visitor, VisitorDTO>().ReverseMap();
            CreateMap<SMSVisitorInvitation, InvitationDTO>().ReverseMap();
            CreateMap<QRVisitorInvitation, InvitationDTO>().ReverseMap();
            CreateMap<List<QRVisitorInvitation>, List<InvitationDTO>>().ReverseMap();
        }
    }
}
