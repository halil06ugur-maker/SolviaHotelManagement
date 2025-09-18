using AutoMapper;
using HotelReservation.Domain;
using Microsoft.EntityFrameworkCore;
using SolviaHotelManagement.Domainn.Infrastructure.Interface.RoomService;
using SolviaHotelManagement.Models.Entities;
using SolviaHotelManagement.Models.ServiceResult;
using SolviaHotelManagement.Models.ViewModels.Hotel;
using SolviaHotelManagement.Models.ViewModels.Room;

namespace SolviaHotelManagement.Domainn.Infrastructure.Service.RoomService
{
    public class RoomService : IRoomService
    {
        private readonly SolviaHotelManagementDbContext _context;
        private readonly IMapper _mapper;

        public RoomService(SolviaHotelManagementDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResult> AddRoomAsync(RoomViewModel viewModel)
        {
            var entity = _mapper.Map<Room>(viewModel);
            await _context.Rooms.AddAsync(entity);
            await _context.SaveChangesAsync();
            return new ServiceResult(entity, "Oda başarıyla eklendi.");
        }

        public async Task<ServiceResult> DeleteRoomAsync(int id)
        {
            var entity =  await _context.Rooms.FindAsync(id);
            if (entity is null) 
                return new ServiceResult("Sistemde böyle bir kayıt bulunamadı.");
            _context.Rooms.Remove(entity);
            await _context.SaveChangesAsync();
            return new ServiceResult(entity!, "İşlem başarıyla gerçekleştirildi.");
        }

        public async Task<ServiceResult> GetRoomByIdAsync(int id)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == id);
            if (room is null)
                return new ServiceResult(null, "Oda sistemde bulunamadı.");
            else
                return new ServiceResult(room, "Oda başarıyla bulundu.");
        }

        public async Task<ServiceResult> GetRoomListAsync()
        {
            var RoomList = await _context.Rooms.ToListAsync();
            if (RoomList.Any())
                return new ServiceResult(RoomList, "Room listesi başarıyla oluşturuldu.");
            else
                return new ServiceResult("Room listesi sistemde bulunamadı.");
        }

        public async Task<ServiceResult> UpdateRoomAsync(RoomViewModel viewModel)
        {
            if (viewModel.Id <= 0)
                return new ServiceResult("Id değeri geçersizdir.");
            var room = await _context.Rooms.FindAsync(viewModel.Id);
            if (room is null)
                return new ServiceResult("Veritabanında böyle bir oda bulunamadı.");
            var entity = _mapper.Map<Room>(viewModel);
            _context.ChangeTracker.Clear();
            _context.Rooms.Update(entity);
            await _context.SaveChangesAsync();
            return new ServiceResult(room, "Oda başarıyla güncellendi.");
        }
    
    }
}
