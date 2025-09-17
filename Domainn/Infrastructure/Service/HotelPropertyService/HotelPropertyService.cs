using AutoMapper;
using HotelReservation.Domain;
using Microsoft.EntityFrameworkCore;
using SolviaHotelManagement.Domainn.Infrastructure.Interface.HotelProperty;
using SolviaHotelManagement.Models.Entities;
using SolviaHotelManagement.Models.ServiceResult;
using SolviaHotelManagement.Models.ViewModels.HotelProperty;
public class HotelPropertyService : IHotelPropertyService
{
    private readonly SolviaHotelManagementDbContext _SolviaHotelManagementDbContext;
    private readonly IMapper _Mapper;

    public HotelPropertyService(SolviaHotelManagementDbContext solviaHotelManagementDbContext, IMapper mapper)
    {
        _SolviaHotelManagementDbContext = solviaHotelManagementDbContext;
        _Mapper = mapper;
    }

    public async Task<ServiceResult> AddHotelPropertyAsync(HotelPropertyViewModel viewModel)
    {
        var entity = _Mapper.Map<HotelProperty>(viewModel);
        await _SolviaHotelManagementDbContext.HotelProperties.AddAsync(entity);
        await _SolviaHotelManagementDbContext.SaveChangesAsync();
        return new ServiceResult(entity, "İşlem başarıyla gerçekleştirildi.");
    }

    public async Task<ServiceResult> DeleteHotelPropertyAsync(int id)
    {
        var entity = await _SolviaHotelManagementDbContext.HotelProperties.FindAsync(id);
        if (entity == null)
            return new ServiceResult("Veritabanında böyle bir kayıt bulunamadı.");
        _SolviaHotelManagementDbContext.HotelProperties.Remove(entity);
        await _SolviaHotelManagementDbContext.SaveChangesAsync();
        return new ServiceResult(entity, "İşlem başarıyla gerçekleştirildi.");

    }

    public async Task<ServiceResult> GetByHotelPropertyIdAsync(int hotelPropertyId)
    {
        var entity = await _SolviaHotelManagementDbContext.HotelProperties
            .FirstOrDefaultAsync(p => p.Id == hotelPropertyId);

        if (entity == null)
            return new ServiceResult("Kayıt bulunamadı.");

        var viewModel = _Mapper.Map<HotelPropertyViewModel>(entity);
        return new ServiceResult(viewModel);
    }

    public async Task<ServiceResult> GetListAsync()
    {
        var list = await _SolviaHotelManagementDbContext.HotelProperties.ToListAsync();

        if (list.Any())
        {
            var viewModels = _Mapper.Map<List<HotelPropertyViewModel>>(list);
            return new ServiceResult(viewModels);
        }
        else
        {
            return new ServiceResult("Liste sistemde bulunamadı.");
        }
    }

    public async Task<ServiceResult> GetListByHotelIdAsync(int hotelId)
    {
        var list = await _SolviaHotelManagementDbContext.HotelProperties
            .Where(p => p.HotelId == hotelId)
            .ToListAsync();

        if (list.Any())
        {
            var viewModels = _Mapper.Map<List<HotelPropertyViewModel>>(list);
            return new ServiceResult(viewModels);
        }
        else
        {
            return new ServiceResult("Liste sistemde bulunamadı.");
        }
    }

    public async Task<ServiceResult> UpdateHotelPropertyAsync(HotelPropertyViewModel viewModel)
    {
        if (viewModel.Id <= 0)
            return new ServiceResult("Id değeri geçersiz.");

        var entity = await _SolviaHotelManagementDbContext.HotelProperties.FindAsync(viewModel.Id);

        if (entity is null)
            return new ServiceResult("Veritabanında böyle bir kayıt bulunamadı.");

        _Mapper.Map(viewModel, entity);
        _SolviaHotelManagementDbContext.HotelProperties.Update(entity);
        await _SolviaHotelManagementDbContext.SaveChangesAsync();
        return new ServiceResult(entity, "İşlem başarıyla gerçekleştirildi.");
    }
}
    
