using Gardenia.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.Interfaces
{
    public interface IOrdersRepository
    {
        //Orders
        Task<ApiResponse<OrderDTO>> AddOrder(int id, OrderDTO model);
        Task<ApiResponse> AddOrderImages(int id, IFormFile[] files);
        Task<ApiResponse<OrderResponseDTO>> AddOrderResponse(int id, OrderResponseDTO model);
        Task<ApiResponse> AddOrderResponseImages(int id, IFormFile[] files);
        Task<ApiResponse<List<OrderDTO>>> GetAllOrders(int id);
        Task<ApiResponse<GetOrderById>> GetOrderById(int id);


        
        //Report Types
        Task<ApiResponse<ReportTypeDTO>> AddReportType(ReportTypeDTO model);
        Task<ApiResponse> DeleteReportType(int id);
        Task<ApiResponse<ReportTypeDTO>> UpdateReportType(int id, ReportTypeDTO model);
        Task<ApiResponse<List<ReportTypeDTO>>> GetAllReportTypes();

        
        
        //Reports
        Task<ApiResponse<ReportDTO>> AddReport(ReportDTO model);
        Task<ApiResponse> AddReportImages(int id, IFormFile[] files);
        Task<ApiResponse<ReportResponseDTO>> AddReportResponse(int id, ReportResponseDTO model);
        Task<ApiResponse> AddReportResponseImages(int id, IFormFile[] files);
        Task<ApiResponse<List<ReportDTO>>> GetAllReports();
        Task<ApiResponse<GetReportById>> GetReportById(int id);


        
        //Maintainance Types
        Task<ApiResponse<MaintainanceTypeDTO>> AddMaintainanceType(MaintainanceTypeDTO model);
        Task<ApiResponse> DeleteMaintainanceType(int id);
        Task<ApiResponse<MaintainanceTypeDTO>> UpdateMaintainanceType(int id, MaintainanceTypeDTO model);
        Task<ApiResponse<List<MaintainanceTypeDTO>>> GetAllMaintainanceTypes();


       
        //Maintainance
        Task<ApiResponse<MaintainanceDTO>> AddMaintainance(int id, MaintainanceDTO model);
        Task<ApiResponse> AddMaintainanceImages(int id, IFormFile[] files);
        Task<ApiResponse<MaintainanceResponseDTO>> AddMaintainanceResponse(int id, MaintainanceResponseDTO model);
        Task<ApiResponse> AddMaintainanceResponseImages(int id, IFormFile[] files);
        Task<ApiResponse<List<MaintainanceDTO>>> GetAllMaintainances();
        Task<ApiResponse<GetMaintainanceById>> GetMaintainanceById(int id);


        
        //My Contributories
        Task<ApiResponse<GetAllOrdersForOneUser>> GetAllOrdersForOneUser(string userID);
        Task<ApiResponse<List<OrderDTO>>> GetAllSuggessionsForOneUser(string userID);
        Task<ApiResponse<List<OrderDTO>>> GetAllComplaintsForOneUser(string userID);
        Task<ApiResponse<List<ReportDTO>>> GetAllReportsForOneUser(string userID);
        Task<ApiResponse<List<MaintainanceDTO>>> GetAllMaintainancesForOneUser(string userID);
    }
}
