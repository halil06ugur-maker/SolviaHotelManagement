using AutoMapper;
using Microsoft.EntityFrameworkCore;
using HotelReservation.Domain;
using SolviaHotelManagement.Domainn.Infrastructure.Interface.HotelImageService;
using SolviaHotelManagement.Models.Entities;
using SolviaHotelManagement.Models.ServiceResult;
using SolviaHotelManagement.Models.ViewModels.HotelImage;

namespace SolviaHotelManagement.Domainn.Infrastructure.Service.HotelImageService
{
    public class HotelImageService : IHotelImageService
    {
        private readonly SolviaHotelManagementDbContext _SolviaHotelManagementDbContext;
        private readonly IMapper _Mapper;

        public HotelImageService(SolviaHotelManagementDbContext solviaHotelManagementDbContext, IMapper mapper)
        {
            _SolviaHotelManagementDbContext = solviaHotelManagementDbContext;
            _Mapper = mapper;
        }

        // Yeni bir otel resmi ekler
        public async Task<ServiceResult> AddHotelImageAsync(HotelImageViewModel viewModel)
        {
            var entity = _Mapper.Map<HotelImage>(viewModel);
            await _SolviaHotelManagementDbContext.HotelImages.AddAsync(entity);
            await _SolviaHotelManagementDbContext.SaveChangesAsync();
            return new ServiceResult(entity, "İşlem başarıyla gerçekleştirildi.");
        }

        // Bir otel resmini siler
        public async Task<ServiceResult> DeleteHotelImageAsync(int id)
        {
            var entity = await _SolviaHotelManagementDbContext.HotelImages.FindAsync(id);
            if (entity == null)
                return new ServiceResult("Veritabanında böyle bir kayıt bulunamadı.");
            _SolviaHotelManagementDbContext.HotelImages.Remove(entity);
            await _SolviaHotelManagementDbContext.SaveChangesAsync();
            return new ServiceResult(entity, "İşlem başarıyla gerçekleştirildi.");
        }

        // Belirli bir otel resmini ID ile getirir
        public async Task<ServiceResult> GetByHotelImageIdAsync(int hotelImageId)
        {
            var entity = await _SolviaHotelManagementDbContext.HotelImages
                .FirstOrDefaultAsync(i => i.Id == hotelImageId);

            if (entity == null)
                return new ServiceResult("Kayıt bulunamadı.");

            var viewModel = _Mapper.Map<HotelImageViewModel>(entity);
            return new ServiceResult(viewModel);
        }

        // Tüm otel resimlerini listeler
        public async Task<ServiceResult> GetListAsync()
        {
            var list = await _SolviaHotelManagementDbContext.HotelImages.ToListAsync();

            if (list.Any())
            {
                var viewModels = _Mapper.Map<List<HotelImageViewModel>>(list);
                return new ServiceResult(viewModels);
            }
            else
            {
                return new ServiceResult("Liste sistemde bulunamadı.");
            }
        }

        // Belirli bir otele ait tüm resimleri listeler
        public async Task<ServiceResult> GetListByHotelIdAsync(int hotelId)
        {
            var list = await _SolviaHotelManagementDbContext.HotelImages
                .Where(i => i.HotelId == hotelId)
                .ToListAsync();

            if (list.Any())
            {
                var viewModels = _Mapper.Map<List<HotelImageViewModel>>(list);
                return new ServiceResult(viewModels);
            }
            else
            {
                return new ServiceResult("Liste sistemde bulunamadı.");
            }
        }

        // Bir otel resmini günceller
        public async Task<ServiceResult> UpdateHotelImageAsync(HotelImageViewModel viewModel)
        {
            if (viewModel.Id <= 0)
                return new ServiceResult("Id değeri geçersiz.");

            var entity = await _SolviaHotelManagementDbContext.HotelImages.FindAsync(viewModel.Id);

            if (entity is null)
                return new ServiceResult("Veritabanında böyle bir kayıt bulunamadı.");

            _Mapper.Map(viewModel, entity);
            _SolviaHotelManagementDbContext.HotelImages.Update(entity);
            await _SolviaHotelManagementDbContext.SaveChangesAsync();
            return new ServiceResult(entity, "İşlem başarıyla gerçekleştirildi.");
        }
    }

}
