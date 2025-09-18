using AutoMapper;
using HotelReservation.Domain;
using Microsoft.EntityFrameworkCore;
using SolviaHotelManagement.Domainn.Infrastructure.Interface.HotelRoomService;
using SolviaHotelManagement.Models.Entities;
using SolviaHotelManagement.Models.ServiceResult;
using SolviaHotelManagement.Models.ViewModels.HotelRoom;



public class HotelRoomService : IHotelRoomService
{
    private readonly SolviaHotelManagementDbContext _solviaHotelManagementDbContext;
    private readonly IMapper _mapper;

    public HotelRoomService(SolviaHotelManagementDbContext dbContext, IMapper mapper)
    {
        _solviaHotelManagementDbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ServiceResult> GetByIdAsync(int id)
    {
        var entity = await _solviaHotelManagementDbContext.HotelRooms
            .Include(hr => hr.Room)
            .FirstOrDefaultAsync(hr => hr.Id == id);

        if (entity == null)
            return new ServiceResult("Kayıt bulunamadı.");

        var viewModel = _mapper.Map<HotelRoomViewModel>(entity);
        return new ServiceResult(viewModel);
    }

    public async Task<ServiceResult> GetListAsync()
    {
        var list = await _solviaHotelManagementDbContext.HotelRooms.ToListAsync();
        if (list.Any())
        {
            var viewModels = _mapper.Map<List<HotelRoomViewModel>>(list);
            return new ServiceResult(viewModels);
        }
        return new ServiceResult("Liste sistemde bulunamadı.");
    }

    public async Task<ServiceResult> GetListByHotelIdAsync(int hotelId)
    {
        var list = await _solviaHotelManagementDbContext.HotelRooms
            .Where(hr => hr.HotelId == hotelId)
            .ToListAsync();

        if (list.Any())
        {
            var viewModels = _mapper.Map<List<HotelRoomViewModel>>(list);
            return new ServiceResult(viewModels);
        }
        return new ServiceResult("Liste sistemde bulunamadı.");
    }

    public async Task<ServiceResult> GetListByRoomIdAsync(int roomId)
    {
        var list = await _solviaHotelManagementDbContext.HotelRooms
            .Where(hr => hr.RoomId == roomId)
            .ToListAsync();

        if (list.Any())
        {
            var viewModels = _mapper.Map<List<HotelRoomViewModel>>(list);
            return new ServiceResult(viewModels);
        }
        return new ServiceResult("Liste sistemde bulunamadı.");
    }

    public async Task<ServiceResult> AddAsync(HotelRoomViewModel viewModel)
    {
        var entity = _mapper.Map<HotelRoom>(viewModel);
        entity.CreatedDate = DateTime.UtcNow;

        await _solviaHotelManagementDbContext.HotelRooms.AddAsync(entity);
        await _solviaHotelManagementDbContext.SaveChangesAsync();

        return new ServiceResult(entity, "İşlem başarıyla gerçekleştirildi.");
    }

    public async Task<ServiceResult> UpdateAsync(HotelRoomViewModel viewModel)
    {
        if (viewModel.Id <= 0)
            return new ServiceResult("Id değeri geçersiz.");

        var entity = await _solviaHotelManagementDbContext.HotelRooms.FindAsync(viewModel.Id);
        if (entity == null)
            return new ServiceResult("Veritabanında böyle bir kayıt bulunamadı.");

        _mapper.Map(viewModel, entity);
        _solviaHotelManagementDbContext.HotelRooms.Update(entity);
        await _solviaHotelManagementDbContext.SaveChangesAsync();

        return new ServiceResult(entity, "İşlem başarıyla gerçekleştirildi.");
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var entity = await _solviaHotelManagementDbContext.HotelRooms.FindAsync(id);
        if (entity == null)
            return new ServiceResult("Veritabanında böyle bir kayıt bulunamadı.");

        _solviaHotelManagementDbContext.HotelRooms.Remove(entity);
        await _solviaHotelManagementDbContext.SaveChangesAsync();

        return new ServiceResult(entity, "İşlem başarıyla gerçekleştirildi.");
    }
}
