using AutoMapper;
using HotelReservation.Domain;
using Microsoft.EntityFrameworkCore;
using SolviaHotelManagement.Domainn.Infrastructure.Interface.CustomerHotelRoomService;
using SolviaHotelManagement.Models.Entities;
using SolviaHotelManagement.Models.ServiceResult;
using SolviaHotelManagement.Models.ViewModels.CustomerHotelRoom;

namespace SolviaHotelManagement.Domainn.Infrastructure.Service.CustomerHotelRoomService
{
    public class CustomerHotelRoomService : ICustomerHotelRoomService
    {
        private readonly SolviaHotelManagementDbContext _context;
        private readonly IMapper _mapper;

        public CustomerHotelRoomService(SolviaHotelManagementDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResult> AddReservationAsync(CustomerHotelRoomViewModel viewModel)
        {
            var entity = _mapper.Map<CustomerHotelRoom>(viewModel);
            await _context.CustomerHotelRooms.AddAsync(entity);
            await _context.SaveChangesAsync();
            return new ServiceResult(entity, "Rezervasyon başarıyla eklendi.");
        }

        public async Task<ServiceResult> DeleteReservationAsync(int id)
        {
            var reservation = await _context.CustomerHotelRooms.FindAsync(id);
            if (reservation is null)
                return new ServiceResult("Rezervasyon bulunamadı.");

            _context.CustomerHotelRooms.Remove(reservation);
            await _context.SaveChangesAsync();
            return new ServiceResult(reservation, "Rezervasyon başarıyla silindi.");
        }

        public async Task<ServiceResult> GetReservationByIdAsync(int id)
        {
            var reservation = await _context.CustomerHotelRooms
                                            .Include(r => r.Customer)
                                            .Include(r => r.Hotel)
                                            .Include(r => r.Room)
                                            .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation is null)
                return new ServiceResult(null, "Rezervasyon bulunamadı.");

            var result = _mapper.Map<CustomerHotelRoomViewModel>(reservation);
            return new ServiceResult(result, "Rezervasyon başarıyla getirildi.");
        }

        public async Task<ServiceResult> GetReservationListAsync()
        {
            var reservations = await _context.CustomerHotelRooms
                                             .Include(r => r.Customer)
                                             .Include(r => r.Hotel)
                                             .Include(r => r.Room)
                                             .ToListAsync();
            if (reservations.Any())
            {
                var result = _mapper.Map<List<CustomerHotelRoomViewModel>>(reservations);
                return new ServiceResult(result, "Rezervasyon listesi başarıyla getirildi.");
            }
            return new ServiceResult("Rezervasyon listesi bulunamadı.");
        }

        public async Task<ServiceResult> UpdateReservationAsync(CustomerHotelRoomViewModel viewModel)
        {
            if (viewModel.Id <= 0)
                return new ServiceResult("Geçersiz Id değeri.");
            var existing = await _context.CustomerHotelRooms.FindAsync(viewModel.Id);
            if (existing is null)
                return new ServiceResult("Rezervasyon bulunamadı.");
        var entity = _mapper.Map<CustomerHotelRoom>(viewModel);
            _context.ChangeTracker.Clear();
            _context.CustomerHotelRooms.Update(entity);
            await _context.SaveChangesAsync();
            return new ServiceResult(entity, "Rezervasyon başarıyla güncellendi.");
        }
    }
}
