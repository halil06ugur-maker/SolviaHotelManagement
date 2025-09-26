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
            // 1) Otelde oda var mı?
            var hotelRoom = await _context.HotelRooms
                .FirstOrDefaultAsync(hr =>
                    hr.HotelId == viewModel.HotelId &&
                    hr.RoomId == viewModel.RoomId
                );

            if (hotelRoom == null)
            {
                return new ServiceResult("Seçilen oda bu otelde bulunmuyor.");
            }

            // 2) Aynı müşteri aynı otelde aynı tarihlerde var mı?
            var conflictCustomer = await _context.CustomerHotelRooms
                .AnyAsync(r =>
                    r.CustomerId == viewModel.CustomerId &&
                    r.HotelId == viewModel.HotelId &&
                    (
                        (viewModel.StartDate >= r.StartDate && viewModel.StartDate < r.EndDate) ||
                        (viewModel.EndDate > r.StartDate && viewModel.EndDate <= r.EndDate) ||
                        (viewModel.StartDate <= r.StartDate && viewModel.EndDate >= r.EndDate)
                    )
                );

            if (conflictCustomer)
            {
                return new ServiceResult("Bu müşteri aynı otelde belirtilen tarihlerde zaten bir rezervasyona sahip.");
            }

            // 3) Oda başka müşteriye aynı tarihlerde verilmiş mi?
            var conflictRoom = await _context.CustomerHotelRooms
                .AnyAsync(r =>
                    r.RoomId == viewModel.RoomId &&
                    r.HotelId == viewModel.HotelId &&
                    (
                        (viewModel.StartDate >= r.StartDate && viewModel.StartDate < r.EndDate) ||
                        (viewModel.EndDate > r.StartDate && viewModel.EndDate <= r.EndDate) ||
                        (viewModel.StartDate <= r.StartDate && viewModel.EndDate >= r.EndDate)
                    )
                );

            if (conflictRoom)
            {
                return new ServiceResult("Seçilen oda belirtilen tarihlerde zaten rezerve edilmiş.");
            }

            // 4) Rezervasyonu kaydet
            var entity = _mapper.Map<CustomerHotelRoom>(viewModel);
            await _context.CustomerHotelRooms.AddAsync(entity);

            // Oda rezervasyona girdiği için işaretle
            hotelRoom.IsReserved = true;
            _context.HotelRooms.Update(hotelRoom);

            await _context.SaveChangesAsync();

            return new ServiceResult(entity, "Rezervasyon başarıyla eklendi. Oda belirtilen tarihler için rezerve edildi.");

        }

        // ==================== DELETE ====================
        public async Task<ServiceResult> DeleteReservationAsync(int id)
        {
            var reservation = await _context.CustomerHotelRooms.FindAsync(id);
            if (reservation is null)
                return new ServiceResult("Rezervasyon bulunamadı.");

            _context.CustomerHotelRooms.Remove(reservation);
            await _context.SaveChangesAsync();
            return new ServiceResult(reservation, "Rezervasyon başarıyla silindi.");
        }

        // ==================== GET BY HOTEL ====================
        public async Task<ServiceResult> GetReservationByIdAsync(int HotelId)
        {
            var CustomerHotelRooms = await _context.CustomerHotelRooms
                .Where(hr => hr.HotelId == HotelId)
                .Include(chr => chr.Customer)
                .Include(chr => chr.Hotel)
                .Include(chr => chr.Room)
                .ToListAsync();

            if (CustomerHotelRooms == null || !CustomerHotelRooms.Any())
                return new ServiceResult("Atama Bulunamadı.");

            var viewmodels = _mapper.Map<List<CustomerHotelRoomViewModel>>(CustomerHotelRooms);

            var responseViewModel = viewmodels
                .GroupBy(x => new { x.HotelId, HotelName = x.Hotel.name })
                .Select(hotelgroup => new
                {
                    HotelId = hotelgroup.Key.HotelId,
                    HotelName = hotelgroup.Key.HotelName,
                    Rooms = hotelgroup
                        .GroupBy(x => new { x.RoomId, RoomNumber = x.Room.Number })
                        .Select(roomgroup => new
                        {
                            RoomId = roomgroup.Key.RoomId,
                            RoomNumber = roomgroup.Key.RoomNumber,
                            Customer = roomgroup.Select(c => new
                            {
                                CustomerHotelRoomId = c.Id,
                                customerId = c.Customer.Id,
                                CustomerIdentityNumber = c.Customer.IdentityNumber,
                                CustomerName = c.Customer.Name,
                                CustomerSurname = c.Customer.Surname,
                                CustomerPhoneNumber = c.Customer.PhoneNumber,
                                CustomerAge = c.Customer.Age,
                                CustomerGender = c.Customer.Gender,
                                Startdate = c.StartDate,
                                Enddate = c.EndDate
                            }).ToList()
                        }).ToList()
                }).ToList();

            return new ServiceResult(responseViewModel, "Rezervasyonlar başarıyla getirildi.");
        }

        // ==================== LIST ====================
    
        public async Task<ServiceResult> GetReservationListAsync()
        {
            var reservations = await _context.CustomerHotelRooms
                .Include(r => r.Customer)
                .Include(r => r.Hotel)
                .Include(r => r.Room)
                .ToListAsync();

            if (!reservations.Any())
            {
                return new ServiceResult("Rezervasyon listesi bulunamadı.");
            }

            var viewmodels = _mapper.Map<List<CustomerHotelRoomViewModel>>(reservations);

            var groupedResult = viewmodels
                .GroupBy(x => new { x.HotelId, HotelName = x.Hotel.name })
                .Select(hotelGroup => new
                {
                    HotelId = hotelGroup.Key.HotelId,
                    HotelName = hotelGroup.Key.HotelName,
                    Rooms = hotelGroup
                        .GroupBy(x => new { x.RoomId, RoomNumber = x.Room.Number })
                        .Select(roomGroup => new
                        {
                            RoomId = roomGroup.Key.RoomId,
                            RoomNumber = roomGroup.Key.RoomNumber,
                            Customers = roomGroup.Select(c => new
                            {
                                CustomerHotelRoomId = c.Id,
                                CustomerId = c.Customer.Id,
                                CustomerIdentityNumber = c.Customer.IdentityNumber,
                                CustomerName = c.Customer.Name,
                                CustomerSurname = c.Customer.Surname,
                                CustomerPhoneNumber = c.Customer.PhoneNumber,
                                CustomerAge = c.Customer.Age,
                                CustomerGender = c.Customer.Gender,
                                StartDate = c.StartDate,
                                EndDate = c.EndDate
                            }).ToList()
                        }).ToList()
                }).ToList();

            return new ServiceResult(groupedResult, "Rezervasyon listesi başarıyla getirildi.");
        }

  

        // ==================== UPDATE ====================
        public async Task<ServiceResult> UpdateReservationAsync(CustomerHotelRoomViewModel viewModel)
        {
            if (viewModel.Id <= 0)
                return new ServiceResult("Geçersiz Id değeri.");

            var existing = await _context.CustomerHotelRooms.FindAsync(viewModel.Id);
            if (existing is null)
                return new ServiceResult("Rezervasyon bulunamadı.");

            // 1) Otelde oda var mı?
            var hotelRoom = await _context.HotelRooms
                .FirstOrDefaultAsync(hr =>
                    hr.HotelId == viewModel.HotelId &&
                    hr.RoomId == viewModel.RoomId
                );

            if (hotelRoom == null)
            {
                return new ServiceResult("Seçilen oda bu otelde bulunmuyor.");
            }

            // 2) Aynı müşteri aynı otelde aynı tarihlerde başka rezervasyona sahip mi?
            var conflictCustomer = await _context.CustomerHotelRooms
                .AnyAsync(r =>
                    r.Id != viewModel.Id && // kendi kaydını hariç tut
                    r.CustomerId == viewModel.CustomerId &&
                    r.HotelId == viewModel.HotelId &&
                    (
                        (viewModel.StartDate >= r.StartDate && viewModel.StartDate < r.EndDate) ||
                        (viewModel.EndDate > r.StartDate && viewModel.EndDate <= r.EndDate) ||
                        (viewModel.StartDate <= r.StartDate && viewModel.EndDate >= r.EndDate)
                    )
                );

            if (conflictCustomer)
            {
                return new ServiceResult("Bu müşteri aynı otelde belirtilen tarihlerde zaten başka bir rezervasyona sahip.");
            }

            // 3) Oda başka müşteri tarafından aynı tarihlerde rezerve edilmiş mi?
            var conflictRoom = await _context.CustomerHotelRooms
                .AnyAsync(r =>
                    r.Id != viewModel.Id && // kendi kaydını hariç tut
                    r.RoomId == viewModel.RoomId &&
                    r.HotelId == viewModel.HotelId &&
                    (
                        (viewModel.StartDate >= r.StartDate && viewModel.StartDate < r.EndDate) ||
                        (viewModel.EndDate > r.StartDate && viewModel.EndDate <= r.EndDate) ||
                        (viewModel.StartDate <= r.StartDate && viewModel.EndDate >= r.EndDate)
                    )
                );

            if (conflictRoom)
            {
                return new ServiceResult("Seçilen oda belirtilen tarihlerde başka bir müşteri tarafından rezerve edilmiş.");
            }

            // 4) Rezervasyonu güncelle
            var entity = _mapper.Map<CustomerHotelRoom>(viewModel);
            _context.ChangeTracker.Clear();
            _context.CustomerHotelRooms.Update(entity);

            // 5) Odayı rezerve et
            hotelRoom.IsReserved = true;
            _context.HotelRooms.Update(hotelRoom);

            await _context.SaveChangesAsync();

            return new ServiceResult(entity, "Rezervasyon başarıyla güncellendi. Oda rezerve edildi.");
        }
    }
}
