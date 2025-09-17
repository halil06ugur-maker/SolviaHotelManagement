using AutoMapper;
using HotelReservation.Domain;
using Microsoft.EntityFrameworkCore;
using SolviaHotelManagement.Domainn.Infrastructure.Interface.HotelAdressService;
using SolviaHotelManagement.Models.Entities;
using SolviaHotelManagement.Models.ServiceResult;
using SolviaHotelManagement.Models.ViewModels.HotelAddress;

namespace SolviaHotelManagement.Domainn.Infrastructure.Service.HotelAddresservice
{
    public class HotelAddressService : IHotelAddressService
    {
        private readonly SolviaHotelManagementDbContext _SolviaHotelManagementDbContext;
        private readonly IMapper _Mapper;
        
        public HotelAddressService(SolviaHotelManagementDbContext solviaHotelManagementDbContext, IMapper mapper)
        {
            _SolviaHotelManagementDbContext = solviaHotelManagementDbContext;
            _Mapper = mapper;
        }
        public async Task<ServiceResult> AddHotelAddressAsync(HotelAddressViewModel viewModel)
        {
            var entity = _Mapper.Map<HotelAddress>(viewModel);
            await _SolviaHotelManagementDbContext.HotelAddresses.AddAsync(entity);
            await _SolviaHotelManagementDbContext.SaveChangesAsync();
            return new ServiceResult(entity, "İşlem başarıyla gerçekleştirildi.");
        }

        public async Task<ServiceResult> DeleteHotelAddressAsync(int id)
        {
            var entity = await _SolviaHotelManagementDbContext.HotelAddresses.FindAsync(id);
            if (entity == null)
                return new ServiceResult("Veritabanında böyle bir kayıt bulunamadı.");
            _SolviaHotelManagementDbContext.HotelAddresses.Remove(entity);
            await _SolviaHotelManagementDbContext.SaveChangesAsync();
            return new ServiceResult(entity, "İşlem başarıyla gerçekleştirildi.");
        }

        public async Task<ServiceResult> GetByHotelIdAsync(int hotelId)
        {
            var entity = await _SolviaHotelManagementDbContext.HotelAddresses.FirstOrDefaultAsync(a => a.HotelId == hotelId);
            if (entity == null)
                return new ServiceResult("Kayıt bulunamadı.");
            var viewModel = _Mapper.Map<HotelAddressViewModel>(entity);
            return new ServiceResult(viewModel);
        }

        // ✨ Düzeltme: Veri döngüsü sorununu çözmek için Listeyi View Model'e dönüştürdük.
        public async Task<ServiceResult> GetListAsync()
        {
            var list = await _SolviaHotelManagementDbContext.HotelAddresses.ToListAsync();

            if (list.Any())
            {
                var viewModels = _Mapper.Map<List<HotelAddressViewModel>>(list);
                return new ServiceResult(viewModels);
            }
            else
            {
                return new ServiceResult("Liste sistemde bulunamadı.");
            }
        }

        // ✨ Düzeltme: Veri döngüsü sorununu çözmek için Listeyi View Model'e dönüştürdük.
        public async Task<ServiceResult> GetListByHotelIdAsync(int hotelId)
        {
            var list = await _SolviaHotelManagementDbContext.HotelAddresses.Where(a => a.HotelId == hotelId).ToListAsync();
            if (list.Any())
            {
                var viewModels = _Mapper.Map<List<HotelAddressViewModel>>(list);
                return new ServiceResult(viewModels);
            }
            else
            {
                return new ServiceResult("Liste sistemde bulunamadı.");
            }
        }

        // ✨ Düzeltme: Eksik olan adres güncelleme metodunu tamamladık.
        public async Task<ServiceResult> UpdateHotelAddressAsync(HotelAddressViewModel viewModel)
        {
            if (viewModel.Id <= 0)
                return new ServiceResult("Id değeri geçersiz.");

            var entity = await _SolviaHotelManagementDbContext.HotelAddresses.FindAsync(viewModel.Id);

            if (entity is null)
                return new ServiceResult("Veritabanında böyle bir kayıt bulunamadı.");

            // Gelen veriyi var olan nesneye eşleştiriyoruz.
            _Mapper.Map(viewModel, entity);
            _SolviaHotelManagementDbContext.HotelAddresses.Update(entity);
            await _SolviaHotelManagementDbContext.SaveChangesAsync();
            return new ServiceResult(entity, "İşlem başarıyla gerçekleştirildi.");
        }
    }
}
