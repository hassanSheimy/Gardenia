using AutoMapper;
using Gardenia.Data.DataAccess;
using Gardenia.Data.Models;
using Gardenia.DTOs;
using Gardenia.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicServices _services;
        private readonly UserManager<AppUser> _userManager;

        public OrdersRepository(AppDbContext context, IMapper mapper, IBasicServices services, UserManager<AppUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _services = services;
            _userManager = userManager;
        }

        //Orders
        public async Task<ApiResponse<OrderDTO>> AddOrder(int id, OrderDTO model)
        {
            var response = new ApiResponse<OrderDTO>();

            model.Date = DateTime.UtcNow.AddHours(2);
            model.OrderTypeID = id;

            var user = _context.Users.SingleOrDefault(u => u.Id == model.UserID);
            if (user == null)
            {
                response.Status = false;
                response.Message = "This User is not exist";

                return response;
            }

            //Check if the user has an old order
            var lastOrder = await _context.Orders
                .Where(m => m.UserID == model.UserID)
                .OrderByDescending(m => m.Date)
                .FirstOrDefaultAsync();


            if (lastOrder != null && lastOrder.Status == 0 && lastOrder.OrderTypeID == model.OrderTypeID)
            {
                response.Status = false;
                response.Message = "You already have an order and we still working on it!";

                return response;
            }

            var order = _mapper.Map<OrderDTO, Order>(model);
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            model.Id = order.Id;

            response.Status = true;
            response.Message = "Success";
            response.Response = model;

            return response;
        }


        public async Task<ApiResponse> AddOrderImages(int id, IFormFile[] files)
        {
            var response = new ApiResponse();
            var order = _context.Orders.SingleOrDefault(o => o.Id == id);
            if (order == null)
            {
                response.Status = false;
                response.Message = "This Order is not exist";

                return response;
            }

            if (files.Count() == 0)
            {
                response.Status = false;
                response.Message = "There is no files";

                return response;
            }

            foreach (var file in files)
            {
                var orderImage = new OrderImagesDTO()
                {
                    OrderImage = await _services.UploadPhoto(file),
                    OrderID = id
                };

                var orderImageInDb = _mapper.Map<OrderImagesDTO, OrderImages>(orderImage);
                _context.OrderImages.Add(orderImageInDb);
                await _context.SaveChangesAsync();
            }

            response.Status = true;
            response.Message = "Success";

            return response;
        }


        public async Task<ApiResponse<OrderResponseDTO>> AddOrderResponse(int id, OrderResponseDTO model)
        {
            var response = new ApiResponse<OrderResponseDTO>();

            var order = _context.Orders.SingleOrDefault(r => r.Id == id);
            if (order == null)
            {
                response.Status = false;
                response.Message = "This Order is not exist!";

                return response;
            }

            model.Date = DateTime.UtcNow.AddHours(2);
            model.OrderID = id;
            order.Status = model.Status;

            var orderResponse = _mapper.Map<OrderResponseDTO, OrderResponse>(model);
            await _context.OrderResponses.AddAsync(orderResponse);
            await _context.SaveChangesAsync();

            model.Id = orderResponse.Id;

            response.Status = true;
            response.Message = "Success";
            response.Response = model;

            return response;
        }


        public async Task<ApiResponse> AddOrderResponseImages(int id, IFormFile[] files)
        {
            var response = new ApiResponse();
            var reportResponse = _context.OrderResponses.SingleOrDefault(o => o.Id == id);
            if (reportResponse == null)
            {
                response.Status = false;
                response.Message = "This Response is not exist";

                return response;
            }

            foreach (var file in files)
            {
                var responseImage = new OrderResponseImagesDTO()
                {
                    RespnseImage = await _services.UploadPhoto(file),
                    ResponseID = id
                };

                var responseImageInDb = _mapper.Map<OrderResponseImagesDTO, OrderResponseImages>(responseImage);
                _context.OrderResponseImages.Add(responseImageInDb);
                await _context.SaveChangesAsync();
            }

            response.Status = true;
            response.Message = "Success";

            return response;
        }


        public async Task<ApiResponse<List<OrderDTO>>> GetAllOrders(int id)
        {
            var orders = _context.Orders.Where(r => r.OrderTypeID == id).Select(_mapper.Map<Order, OrderDTO>).ToList();
            var response = new ApiResponse<List<OrderDTO>>();

            if (orders == null)
            {
                response.Status = false;
                response.Message = "There is no orders yet!";

                return response;
            }

            foreach (var order in orders)
            {
                var images = _context.OrderImages.Where(i => i.OrderID == order.Id).Select(_mapper.Map<OrderImages, OrderImagesDTO>).ToList();
                order.Images = images;
            }


            response.Status = true;
            response.Message = "Success";
            response.Response = orders;

            return response;
        }


        public async Task<ApiResponse<GetOrderById>> GetOrderById(int id)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(o => o.Id == id);
            var response = new ApiResponse<GetOrderById>();

            if (order == null)
            {
                response.Status = false;
                response.Message = "This order is not exist";

                return response;
            }

            var user = _context.Users.SingleOrDefault(u => u.Id == order.UserID);

            if (user == null)
            {
                response.Status = false;
                response.Message = "This user is no longer exist";

                return response;
            }

            var orderDTO = _mapper.Map<Order, GetOrderById>(order);
            orderDTO.UserName = user.Name;
            orderDTO.UserImage = user.Image;
            orderDTO.UserEmail = user.Email;
            orderDTO.UserPhone = user.PhoneNumber;

            orderDTO.Images = _context.OrderImages.Where(i => i.OrderID == id).Select(_mapper.Map<OrderImages, OrderImagesDTO>).ToList();
            orderDTO.OrderResponses = _context.OrderResponses.Where(r => r.OrderID == id).Select(_mapper.Map<OrderResponse, OrderResponseDTO>).ToList();
            foreach (var orderResponse in orderDTO.OrderResponses)
            {
                var orderResponseImages = _context.OrderResponseImages.Where(i => i.ResponseID == orderResponse.Id).Select(_mapper.Map<OrderResponseImages, OrderResponseImagesDTO>).ToList();
                orderResponse.OrderResponseImages = orderResponseImages;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = orderDTO;

            return response;
        }


        
        //Report Type
        public async Task<ApiResponse<ReportTypeDTO>> AddReportType(ReportTypeDTO model)
        {
            var response = new ApiResponse<ReportTypeDTO>();
            var report = _mapper.Map<ReportTypeDTO, ReportType>(model);
            await _context.ReportTypes.AddAsync(report);
            await _context.SaveChangesAsync();

            model.Id = report.Id;

            response.Status = true;
            response.Message = "Success";
            response.Response = model;

            return response;
        }

        public async Task<ApiResponse<List<ReportTypeDTO>>> GetAllReportTypes()
        {
            var types = _context.ReportTypes.Select(_mapper.Map<ReportType, ReportTypeDTO>).ToList();
            var response = new ApiResponse<List<ReportTypeDTO>>();

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

        public async Task<ApiResponse> DeleteReportType(int id)
        {
            var response = new ApiResponse();
            var reportType = await _context.ReportTypes.SingleOrDefaultAsync(r => r.Id == id);
            if (reportType == null)
            {
                response.Status = false;
                response.Message = "This Report type is not exist";

                return response;
            }

            _context.ReportTypes.Remove(reportType);
            await _context.SaveChangesAsync();

            response.Status = true;
            response.Message = "Report type is successfully deleted";

            return response;
        }



        public async Task<ApiResponse<ReportTypeDTO>> UpdateReportType(int id, ReportTypeDTO model)
        {
            var response = new ApiResponse<ReportTypeDTO>();

            var reportType = _context.ReportTypes.SingleOrDefault(p => p.Id == id);
            if (reportType == null)
            {
                response.Status = false;
                response.Message = "This Report Type is not exist";

                return response;
            }


            model.Id = id;

            _mapper.Map(model, reportType);

            _context.ReportTypes.Update(reportType);
            await _context.SaveChangesAsync();


            response.Status = true;
            response.Message = "Success";
            response.Response = model;


            return response;
        }


        
        
        //Report
        public async Task<ApiResponse<ReportDTO>> AddReport(ReportDTO model)
        {

            var response = new ApiResponse<ReportDTO>();


            var user = _context.Users.SingleOrDefault(u => u.Id == model.UserID);
            if (user == null)
            {
                response.Status = false;
                response.Message = "This User is not exist";

                return response;
            }


            var type = _context.ReportTypes.SingleOrDefault(r => r.Id == model.ReportTypeID);
            if (type == null)
            {
                response.Status = false;
                response.Message = "This report type is not exist";

                return response;
            }

            //Check if the user has an old order
            var lastOrder = await _context.Reports
                .Where(m => m.UserID == model.UserID)
                .OrderByDescending(m => m.Date)
                .FirstOrDefaultAsync();


            if (lastOrder != null && lastOrder.Status == 0)
            {
                response.Status = false;
                response.Message = "You already have a report and we still working on it!";

                return response;
            }

            model.Date = DateTime.UtcNow.AddHours(2);

            var report = _mapper.Map<ReportDTO, Report>(model);
            await _context.Reports.AddAsync(report);
            await _context.SaveChangesAsync();

            model.Id = report.Id;

            response.Status = true;
            response.Message = "Success";
            response.Response = model;

            return response;
        }


        public async Task<ApiResponse> AddReportImages(int id, IFormFile[] files)
        {
            var response = new ApiResponse();
            var report = _context.Reports.SingleOrDefault(o => o.Id == id);
            if (report == null)
            {
                response.Status = false;
                response.Message = "This report is not exist";

                return response;
            }

            if (files == null)
            {
                response.Status = false;
                response.Message = "There is no files";

                return response;
            }

            foreach (var file in files)
            {
                var reportImage = new ReportImagesDTO()
                {
                    ReportImage = await _services.UploadPhoto(file),
                    ReportID = id
                };

                var reportImageInDb = _mapper.Map<ReportImagesDTO, ReportImages>(reportImage);
                _context.ReportImages.Add(reportImageInDb);
                await _context.SaveChangesAsync();
            }

            response.Status = true;
            response.Message = "Success";

            return response;
        }


        public async Task<ApiResponse<ReportResponseDTO>> AddReportResponse(int id, ReportResponseDTO model)
        {
            var response = new ApiResponse<ReportResponseDTO>();

            var report = _context.Reports.SingleOrDefault(r => r.Id == id);
            if (report == null)
            {
                response.Status = false;
                response.Message = "This Report is not exist!";

                return response;
            }

            model.Date = DateTime.UtcNow.AddHours(2);
            model.ReportID = id;
            report.Status = model.Status;

            var reportResponse = _mapper.Map<ReportResponseDTO, ReportResponse>(model);
            await _context.ReportResponses.AddAsync(reportResponse);
            await _context.SaveChangesAsync();

            model.Id = reportResponse.Id;

            response.Status = true;
            response.Message = "Success";
            response.Response = model;

            return response;
        }

        public async Task<ApiResponse> AddReportResponseImages(int id, IFormFile[] files)
        {
            var response = new ApiResponse();
            var reportResponse = _context.ReportResponses.SingleOrDefault(o => o.Id == id);
            if (reportResponse == null)
            {
                response.Status = false;
                response.Message = "This Response is not exist";

                return response;
            }

            foreach (var file in files)
            {
                var responseImage = new ReportResponseImagesDTO()
                {
                    RespnseImage = await _services.UploadPhoto(file),
                    ResponseID = id
                };

                var responseImageInDb = _mapper.Map<ReportResponseImagesDTO, ReportResponseImages>(responseImage);
                _context.ReportResponseImages.Add(responseImageInDb);
                await _context.SaveChangesAsync();
            }

            response.Status = true;
            response.Message = "Success";

            return response;
        }


        public async Task<ApiResponse<List<ReportDTO>>> GetAllReports()
        {
            var reports = _context.Reports.Include(r => r.ReportType).Select(_mapper.Map<Report, ReportDTO>).ToList();
            var response = new ApiResponse<List<ReportDTO>>();

            if (reports == null)
            {
                response.Status = false;
                response.Message = "There is no reports yet!";

                return response;
            }

            foreach (var report in reports)
            {
                var images = _context.ReportImages.Where(i => i.ReportID == report.Id).Select(_mapper.Map<ReportImages, ReportImagesDTO>).ToList();
                report.Images = images;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = reports;

            return response;
        }


        public async Task<ApiResponse<GetReportById>> GetReportById(int id)
        {
            var report = await _context.Reports.Include(r => r.ReportType).SingleOrDefaultAsync(o => o.Id == id);
            var response = new ApiResponse<GetReportById>();

            if (report == null)
            {
                response.Status = false;
                response.Message = "This report is not exist";

                return response;
            }

            var user = _context.Users.SingleOrDefault(u => u.Id == report.UserID);

            if (user == null)
            {
                response.Status = false;
                response.Message = "This user is no longer exist";

                return response;
            }

            var reportDTO = _mapper.Map<Report, GetReportById>(report);
            reportDTO.UserName = user.Name;
            reportDTO.UserImage = user.Image;
            reportDTO.UserEmail = user.Email;
            reportDTO.UserPhone = user.PhoneNumber;

            reportDTO.Images = _context.ReportImages.Where(i => i.ReportID == id).Select(_mapper.Map<ReportImages, ReportImagesDTO>).ToList();
            reportDTO.ReportResponses = _context.ReportResponses.Where(r => r.ReportID == id).Select(_mapper.Map<ReportResponse, ReportResponseDTO>).ToList();
            foreach (var reportResponse in reportDTO.ReportResponses)
            {
                var reportResponseImages = _context.ReportResponseImages.Where(i => i.ResponseID == reportResponse.Id).Select(_mapper.Map<ReportResponseImages, ReportResponseImagesDTO>).ToList();
                reportResponse.ReportResponseImages = reportResponseImages;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = reportDTO;

            return response;
        }


        
        
        //Maintainance Type
        public async Task<ApiResponse<MaintainanceTypeDTO>> AddMaintainanceType(MaintainanceTypeDTO model)
        {
            var response = new ApiResponse<MaintainanceTypeDTO>();
            var report = _mapper.Map<MaintainanceTypeDTO, MaintainanceType>(model);
            await _context.MaintainanceTypes.AddAsync(report);
            await _context.SaveChangesAsync();

            model.Id = report.Id;

            response.Status = true;
            response.Message = "Success";
            response.Response = model;

            return response;
        }

        public async Task<ApiResponse<List<MaintainanceTypeDTO>>> GetAllMaintainanceTypes()
        {
            var types = _context.MaintainanceTypes.Select(_mapper.Map<MaintainanceType, MaintainanceTypeDTO>).ToList();
            var response = new ApiResponse<List<MaintainanceTypeDTO>>();

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

        public async Task<ApiResponse> DeleteMaintainanceType(int id)
        {
            var response = new ApiResponse();
            var reportType = await _context.MaintainanceTypes.SingleOrDefaultAsync(r => r.Id == id);
            if (reportType == null)
            {
                response.Status = false;
                response.Message = "This Report type is not exist";

                return response;
            }

            _context.MaintainanceTypes.Remove(reportType);
            await _context.SaveChangesAsync();

            response.Status = true;
            response.Message = "Report type is successfully deleted";

            return response;
        }



        public async Task<ApiResponse<MaintainanceTypeDTO>> UpdateMaintainanceType(int id, MaintainanceTypeDTO model)
        {
            var response = new ApiResponse<MaintainanceTypeDTO>();

            var reportType = _context.MaintainanceTypes.SingleOrDefault(p => p.Id == id);
            if (reportType == null)
            {
                response.Status = false;
                response.Message = "This Report Type is not exist";

                return response;
            }


            model.Id = id;

            _mapper.Map(model, reportType);

            _context.MaintainanceTypes.Update(reportType);
            await _context.SaveChangesAsync();


            response.Status = true;
            response.Message = "Success";
            response.Response = model;


            return response;
        }



        
        //Maintainance
        public async Task<ApiResponse<MaintainanceDTO>> AddMaintainance(int id, MaintainanceDTO model)
        {

            var response = new ApiResponse<MaintainanceDTO>();


            var user = _context.Users.SingleOrDefault(u => u.Id == model.UserID);
            if (user == null)
            {
                response.Status = false;
                response.Message = "This User is not exist";

                return response;
            }


            var unit = _context.Units.SingleOrDefault(u => u.Id == id);
            if (unit == null)
            {
                response.Status = false;
                response.Message = "This unit is not exist";

                return response;
            }

            var type = _context.MaintainanceTypes.SingleOrDefault(r => r.Id == model.ReportTypeID);
            if (type == null)
            {
                response.Status = false;
                response.Message = "This report type is not exist";

                return response;
            }

            //Check if the user has an old order
            var lastThreeOrders = _context.Maintainances
                .AsEnumerable()
                .Where(m => m.UserID == model.UserID)
                .OrderBy(m => m.Date).TakeLast(3).ToList();

            int count = 0;

            foreach (var Order in lastThreeOrders)
            {
                if (Order.Status == 0)
                {
                    ++count;
                }
            }

            if (lastThreeOrders.Count == 3 && count == 3)
            {
                response.Status = false;
                response.Message = "You already have a 3 Maintainance Orders and we still working on Them!";

                return response;
            }

            model.UnitID = id;
            model.Date = DateTime.UtcNow.AddHours(2);

            var report = _mapper.Map<MaintainanceDTO, Maintainance>(model);
            await _context.Maintainances.AddAsync(report);
            await _context.SaveChangesAsync();

            model.Id = report.Id;

            response.Status = true;
            response.Message = "Success";
            response.Response = model;

            return response;
        }


        public async Task<ApiResponse> AddMaintainanceImages(int id, IFormFile[] files)
        {
            var response = new ApiResponse();
            var report = _context.Maintainances.SingleOrDefault(o => o.Id == id);
            if (report == null)
            {
                response.Status = false;
                response.Message = "This report is not exist";

                return response;
            }

            if (files == null)
            {
                response.Status = false;
                response.Message = "There is no files";

                return response;
            }

            foreach (var file in files)
            {
                var reportImage = new MaintainanceImagesDTO()
                {
                    ReportImage = await _services.UploadPhoto(file),
                    ReportID = id
                };

                var reportImageInDb = _mapper.Map<MaintainanceImagesDTO, MaintainanceImages>(reportImage);
                _context.MaintainanceImages.Add(reportImageInDb);
                await _context.SaveChangesAsync();
            }

            response.Status = true;
            response.Message = "Success";

            return response;
        }


        public async Task<ApiResponse<MaintainanceResponseDTO>> AddMaintainanceResponse(int id, MaintainanceResponseDTO model)
        {
            var response = new ApiResponse<MaintainanceResponseDTO>();

            var report = _context.Maintainances.SingleOrDefault(r => r.Id == id);
            if (report == null)
            {
                response.Status = false;
                response.Message = "This Report is not exist!";

                return response;
            }

            model.Date = DateTime.UtcNow.AddHours(2);
            model.ReportID = id;
            report.Status = model.Status;

            var reportResponse = _mapper.Map<MaintainanceResponseDTO, MaintainanceResponse>(model);
            await _context.MaintainanceResponses.AddAsync(reportResponse);
            await _context.SaveChangesAsync();

            model.Id = reportResponse.Id;

            response.Status = true;
            response.Message = "Success";
            response.Response = model;

            return response;
        }

        public async Task<ApiResponse> AddMaintainanceResponseImages(int id, IFormFile[] files)
        {
            var response = new ApiResponse();
            var reportResponse = _context.MaintainanceResponses.SingleOrDefault(o => o.Id == id);
            if (reportResponse == null)
            {
                response.Status = false;
                response.Message = "This Response is not exist";

                return response;
            }

            foreach (var file in files)
            {
                var responseImage = new MaintainanceResponseImagesDTO()
                {
                    RespnseImage = await _services.UploadPhoto(file),
                    ResponseID = id
                };

                var responseImageInDb = _mapper.Map<MaintainanceResponseImagesDTO, MaintainanceResponseImages>(responseImage);
                _context.MaintainanceResponseImages.Add(responseImageInDb);
                await _context.SaveChangesAsync();
            }

            response.Status = true;
            response.Message = "Success";

            return response;
        }


        public async Task<ApiResponse<List<MaintainanceDTO>>> GetAllMaintainances()
        {
            var reports = _context.Maintainances.Include(r => r.ReportType).Select(_mapper.Map<Maintainance, MaintainanceDTO>).ToList();
            var response = new ApiResponse<List<MaintainanceDTO>>();

            if (reports == null)
            {
                response.Status = false;
                response.Message = "There is no reports yet!";

                return response;
            }

            foreach (var report in reports)
            {
                var images = _context.MaintainanceImages
                    .Where(i => i.ReportID == report.Id)
                    .Select(_mapper.Map<MaintainanceImages, MaintainanceImagesDTO>)
                    .ToList();
                report.Images = images;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = reports;

            return response;
        }


        public async Task<ApiResponse<GetMaintainanceById>> GetMaintainanceById(int id)
        {
            var report = await _context.Maintainances.Include(r => r.ReportType).SingleOrDefaultAsync(o => o.Id == id);
            var response = new ApiResponse<GetMaintainanceById>();

            if (report == null)
            {
                response.Status = false;
                response.Message = "This report is not exist";

                return response;
            }

            var user = _context.Users.SingleOrDefault(u => u.Id == report.UserID);

            if (user == null)
            {
                response.Status = false;
                response.Message = "This user is no longer exist";

                return response;
            }

            var reportDTO = _mapper.Map<Maintainance, GetMaintainanceById>(report);
            reportDTO.UserName = user.Name;
            reportDTO.UserImage = user.Image;
            reportDTO.UserEmail = user.Email;
            reportDTO.UserPhone = user.PhoneNumber;

            reportDTO.Images = _context.MaintainanceImages
                .Where(i => i.ReportID == id)
                .Select(_mapper.Map<MaintainanceImages, MaintainanceImagesDTO>)
                .ToList();
            reportDTO.ReportResponses = _context.MaintainanceResponses
                .Where(r => r.ReportID == id)
                .Select(_mapper.Map<MaintainanceResponse, MaintainanceResponseDTO>)
                .ToList();

            foreach (var reportResponse in reportDTO.ReportResponses)
            {
                var reportResponseImages = _context.MaintainanceResponseImages
                    .Where(i => i.ResponseID == reportResponse.Id)
                    .Select(_mapper.Map<MaintainanceResponseImages, MaintainanceResponseImagesDTO>)
                    .ToList();
                reportResponse.ReportResponseImages = reportResponseImages;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = reportDTO;

            return response;
        }


        
        
        //My Contributories
        public async Task<ApiResponse<GetAllOrdersForOneUser>> GetAllOrdersForOneUser(string userID)
        {
            var response = new ApiResponse<GetAllOrdersForOneUser>();

            var complaints = _context.Orders
                .Where(o => o.OrderTypeID == 1 && o.UserID == userID)
                .Select(_mapper.Map<Order, OrderDTO>)
                .ToList();
            if (complaints == null)
            {
                response.Status = false;
                response.Message = "There is no comlaints yet!";

                return response;
            }

            foreach (var complaint in complaints)
            {
                var images = _context.OrderImages
                    .Where(i => i.OrderID == complaint.Id)
                    .Select(_mapper.Map<OrderImages, OrderImagesDTO>)
                    .ToList();
                complaint.Images = images;
            }


            var suggestions = _context.Orders
                .Where(o => o.OrderTypeID == 2 && o.UserID == userID)
                .Select(_mapper.Map<Order, OrderDTO>)
                .ToList();
            if (suggestions == null)
            {
                response.Status = false;
                response.Message = "There is no suggestions yet!";

                return response;
            }

            foreach (var suggestion in suggestions)
            {
                var images = _context.OrderImages
                    .Where(i => i.OrderID == suggestion.Id)
                    .Select(_mapper.Map<OrderImages, OrderImagesDTO>)
                    .ToList();
                suggestion.Images = images;
            }


            var reports = _context.Reports
                .Where(o => o.UserID == userID)
                .Include(r => r.ReportType)
                .Select(_mapper.Map<Report, ReportDTO>)
                .ToList();
            if (reports == null)
            {
                response.Status = false;
                response.Message = "There is no reports yet!";

                return response;
            }

            foreach (var report in reports)
            {
                var images = _context.ReportImages
                    .Where(i => i.ReportID == report.Id)
                    .Select(_mapper.Map<ReportImages, ReportImagesDTO>)
                    .ToList();
                report.Images = images;
            }


            var maintainances = _context.Maintainances
                .Where(o => o.UserID == userID)
                .Include(r => r.ReportType)
                .Select(_mapper.Map<Maintainance, MaintainanceDTO>)
                .ToList();
            if (maintainances == null)
            {
                response.Status = false;
                response.Message = "There is no Maintainances yet!";

                return response;
            }

            foreach (var maintainance in maintainances)
            {
                var images = _context.MaintainanceImages
                    .Where(i => i.ReportID == maintainance.Id)
                    .Select(_mapper.Map<MaintainanceImages, MaintainanceImagesDTO>)
                    .ToList();
                maintainance.Images = images;
            }

            var model = new GetAllOrdersForOneUser()
            {
                Reports = reports,
                Complaints = complaints,
                Suggestions = suggestions,
                Maintainances = maintainances
            };


            response.Status = true;
            response.Message = "Success";
            response.Response = model;

            return response;
        }

        public async Task<ApiResponse<List<OrderDTO>>> GetAllSuggessionsForOneUser(string userID)
        {
            var response = new ApiResponse<List<OrderDTO>>();

            var suggestions = _context.Orders
                .Where(o => o.OrderTypeID == 2 && o.UserID == userID)
                .Select(_mapper.Map<Order, OrderDTO>)
                .ToList();

            if (suggestions == null)
            {
                response.Status = false;
                response.Message = "There is no suggestions yet!";

                return response;
            }

            foreach (var suggestion in suggestions)
            {
                var images = _context.OrderImages
                    .Where(i => i.OrderID == suggestion.Id)
                    .Select(_mapper.Map<OrderImages, OrderImagesDTO>)
                    .ToList();
                suggestion.Images = images;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = suggestions;

            return response;
        }


        public async Task<ApiResponse<List<OrderDTO>>> GetAllComplaintsForOneUser(string userID)
        {
            var response = new ApiResponse<List<OrderDTO>>();

            var complaints = _context.Orders
                .Where(o => o.OrderTypeID == 1 && o.UserID == userID)
                .Select(_mapper.Map<Order, OrderDTO>)
                .ToList();

            if (complaints == null)
            {
                response.Status = false;
                response.Message = "There is no comlaints yet!";

                return response;
            }

            foreach (var complaint in complaints)
            {
                var images = _context.OrderImages
                    .Where(i => i.OrderID == complaint.Id)
                    .Select(_mapper.Map<OrderImages, OrderImagesDTO>)
                    .ToList();
                complaint.Images = images;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = complaints;

            return response;
        }


        public async Task<ApiResponse<List<ReportDTO>>> GetAllReportsForOneUser(string userID)
        {
            var response = new ApiResponse<List<ReportDTO>>();

            var reports = _context.Reports
                .Where(o => o.UserID == userID)
                .Include(r => r.ReportType)
                .Select(_mapper.Map<Report, ReportDTO>)
                .ToList();

            if (reports == null)
            {
                response.Status = false;
                response.Message = "There is no reports yet!";

                return response;
            }

            foreach (var report in reports)
            {
                var images = _context.ReportImages
                    .Where(i => i.ReportID == report.Id)
                    .Select(_mapper.Map<ReportImages, ReportImagesDTO>)
                    .ToList();
                report.Images = images;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = reports;

            return response;
        }


        public async Task<ApiResponse<List<MaintainanceDTO>>> GetAllMaintainancesForOneUser(string userID)
        {
            var response = new ApiResponse<List<MaintainanceDTO>>();

            var maintainances = _context.Maintainances
                .Where(o => o.UserID == userID)
                .Include(r => r.ReportType)
                .Select(_mapper.Map<Maintainance, MaintainanceDTO>)
                .ToList();

            if (maintainances == null)
            {
                response.Status = false;
                response.Message = "There is no Maintainances yet!";

                return response;
            }

            foreach (var maintainance in maintainances)
            {
                var images = _context.MaintainanceImages
                    .Where(i => i.ReportID == maintainance.Id)
                    .Select(_mapper.Map<MaintainanceImages, MaintainanceImagesDTO>)
                    .ToList();
                maintainance.Images = images;
            }

            response.Status = true;
            response.Message = "Success";
            response.Response = maintainances;

            return response;
        }
    }
}
