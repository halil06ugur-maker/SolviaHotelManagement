using AutoMapper;
using HotelReservation.Domain;
using Microsoft.EntityFrameworkCore;
using SolviaHotelManagement.Domainn.Infrastructure.Interface.HotelRoomService;
using SolviaHotelManagement.Models.Entities;
using SolviaHotelManagement.Models.ServiceResult;
using SolviaHotelManagement.Models.ViewModels.HotelRoom;

public class HotelRoomService : IHotelRoomService
{
    private readonly SolviaHotelManagementDbContext _context;
    private readonly IMapper _mapper;

    public HotelRoomService(SolviaHotelManagementDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    /// Otel ID'sine göre odaları getir
    public async Task<ServiceResult> GetListByHotelIdAsync(int hotelId)
    {
        var hotelRooms = await _context.HotelRooms
            .Where(hr => hr.HotelId == hotelId)
            .Include(hr => hr.Room) // İlgili oda bilgilerini de dahil et
            .Include(hr => hr.Hotel) // İlgili otel bilgilerini de dahil et
            .ToListAsync();

        if (hotelRooms.Any())
        {
            var viewModels = _mapper.Map<List<HotelRoomViewModel>>(hotelRooms);
            var responseViewModel = viewModels
             .Select(x => new
             {
                 HotelRoomId = x.Id,    
                 HotelId = x.HotelId,
                 HotelName = x.Hotel.Name,
                 RoomId = x.RoomId,
                 RoomNumber = x.Room.Number,
                 RoomType = x.Type,
                 IsReserved = x.IsReserved
             })
             .GroupBy(r => new { r.HotelId, r.HotelName })
             .Select(g => new
             {
                 HotelId = g.Key.HotelId,
                 HotelName = g.Key.HotelName,
                 Rooms = g.Select(r => new
                 {
                     HotelRoomId = r.HotelRoomId,  
                     r.RoomId,
                     r.RoomNumber,
                     r.RoomType,
                     r.IsReserved
                 }).ToList()
             })
             .ToList();

            return new ServiceResult(responseViewModel);
        }

        return new ServiceResult("Otele ait oda bulunamadı.");
    }


    // Oda ID'sine göre tek bir odayı getir
    public async Task<ServiceResult> GetHotelRoomByIdAsync(int id)
    {
        var hotelRoom = await _context.HotelRooms
            .Include(hr => hr.Room)
            .Include(hr => hr.Hotel)
            .Include(hr => hr.Hotel.HotelImages)
            .FirstOrDefaultAsync(hr => hr.Id == id);

        if (hotelRoom != null)
        {
            var viewModel = _mapper.Map<HotelRoomViewModel>(hotelRoom);
            var responseViewModel = new
            {
                HotelRoomId = viewModel.Id,         
                HotelId = viewModel.HotelId,
                HotelName = viewModel.Hotel.Name,
                HotelImage = viewModel.Hotel.HotelImages.ToList(),
                RoomId = viewModel.RoomId,
                RoomNumber = viewModel.Room.Number,
                RoomType = viewModel.Type,
                IsReserved = viewModel.IsReserved
            };
            return new ServiceResult(responseViewModel);
        }

        return new ServiceResult("Oda bulunamadı.");
    }

    // Yeni bir HotelRoom oluştur
    public async Task<ServiceResult> CreateHotelRoomAsync(HotelRoomViewModel model)
    {
        var hotelRoom = _mapper.Map<HotelRoom>(model); // ViewModel'den Entity'ye dönüştür
        _context.HotelRooms.Add(hotelRoom);
        await _context.SaveChangesAsync();

        return new ServiceResult("Oda başarıyla oluşturuldu.");
    }

    public async Task<ServiceResult> UpdateHotelRoomAsync(HotelRoomViewModel ViewModel)
    {
        if(ViewModel.Id<=0)
            return new ServiceResult("Id değeri geçersiz");
        var HotelRoom=await _context.HotelRooms.FindAsync(ViewModel.Id);
        if (HotelRoom == null)
            return new ServiceResult("Veri tabanında böyle bir oda alınamadı.");
        var entity=_mapper.Map<HotelRoom>(ViewModel);
        _context.ChangeTracker.Clear();
        _context.HotelRooms.Update(entity);
        await _context.SaveChangesAsync();
        return new ServiceResult(HotelRoom, "Başarıyla güncellendi.");
    
    }

    // HotelRoom'u sil
    public async Task<ServiceResult> DeleteHotelRoomAsync(int id)
    {
        var hotelRoom = await _context.HotelRooms.FindAsync(id);

        if (hotelRoom == null)
        {
            return new ServiceResult("Oda bulunamadı.");
        }

        _context.HotelRooms.Remove(hotelRoom);
        await _context.SaveChangesAsync();
        return new ServiceResult("Oda başarıyla silindi.");
    }

    //List
    public async Task<ServiceResult> GetAllHotelRooms()
    {
        var hotelRooms = await _context.HotelRooms
            .Include(hr => hr.Room) // Oda bilgilerini dahil et
            .Include(hr => hr.Hotel) // Otel bilgilerini dahil et
            .ToListAsync();

        if (hotelRooms.Any())
        {
            var viewModels = _mapper.Map<List<HotelRoomViewModel>>(hotelRooms);
            var responseViewModel = viewModels
                .Select(x => new
                {
                    HotelRoomId = x.Id,      // Burada HotelRoomId ekleniyor
                    HotelId = x.HotelId,
                    HotelName = x.Hotel.Name,
                    RoomId = x.RoomId,
                    RoomNumber = x.Room.Number,
                    RoomType = x.Type,
                    IsReserved = x.IsReserved
                })
                .GroupBy(r => new { r.HotelId, r.HotelName })
                .Select(g => new
                {
                    HotelId = g.Key.HotelId,
                    HotelName = g.Key.HotelName,
                    Rooms = g.Select(r => new
                    {
                        HotelRoomId = r.HotelRoomId,  // Burası da önemli
                        r.RoomId,
                        r.RoomNumber,
                        r.RoomType,
                        r.IsReserved
                    }).ToList()
                })
                .ToList();

            return new ServiceResult(responseViewModel);
        }

        return new ServiceResult("Otel odaları bulunamadı.");
    }


}

