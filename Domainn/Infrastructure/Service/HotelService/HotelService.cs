using AutoMapper;
using HotelReservation.Domain;
using Microsoft.EntityFrameworkCore;
using SolviaHotelManagement.Domainn.Infrastructure.Interface.HotelService;
using SolviaHotelManagement.Models.Entities;
using SolviaHotelManagement.Models.ServiceResult;
using SolviaHotelManagement.Models.ViewModels.Hotel;

namespace SolviaHotelManagement.Domainn.Infrastructure.Service.HotelService
{
    public class HotelService : IHotelService
    {
        private readonly SolviaHotelManagementDbContext _SolviaHotelManagementDbContext;
        private readonly IMapper _Mapper;

        public HotelService(SolviaHotelManagementDbContext solviaHotelManagementDbContext, IMapper mapper)
        {
            _SolviaHotelManagementDbContext = solviaHotelManagementDbContext;
            _Mapper = mapper;
        }
        //Ekleme İşlemi yapılır
        public async Task<ServiceResult> AddHotelAsync(HotelViewModel ViewModel)
        {
            var Entity = _Mapper.Map<Hotel>(ViewModel);
            await _SolviaHotelManagementDbContext.AddAsync(Entity);
            await _SolviaHotelManagementDbContext.SaveChangesAsync();
            return new ServiceResult(Entity, "İşlem başarıyla gerçekleştirildi.");         
        }
        //Girilen Id'ye göre silme işlemi yapılır
        public async Task<ServiceResult> DeleteHotelAsync(int id)
        {
            var entity = await _SolviaHotelManagementDbContext.Hotels.FindAsync(id);
            if (entity is null)
                return new ServiceResult("Veritabanında böyle bir kayıt bulunamadı");
            _SolviaHotelManagementDbContext.Hotels.Remove(entity!);
            await _SolviaHotelManagementDbContext.SaveChangesAsync();
            return new ServiceResult(entity!, "İşlem başarıyla gerçekleştirildi.");
        }
        //Tek bir Idye göre listemele yapar.
        public async Task<ServiceResult> GetHotelByIdAsync(int id)
        {
            var hotel = await _SolviaHotelManagementDbContext.Hotels
              .Include(h => h.HotelAddresses)
              .Include(h => h.HotelProperty)
              .FirstOrDefaultAsync(h => h.Id == id);

            if (hotel == null)
                return new ServiceResult(null, "Otel bulunamadı.");

            var result = _Mapper.Map<HotelViewModel>(hotel);
            return new ServiceResult(result, "Otel başarıyla getirildi.");
        }
        //Listeleme İşlemi yapılır
        public async Task<ServiceResult> GetHotelListAsync()
        {
            var HotelList = await _SolviaHotelManagementDbContext.Hotels.ToListAsync();
            if (HotelList.Any())
                return new ServiceResult(HotelList, "Hotel listesi başarıyla oluşturuldu.");
            else
                return new ServiceResult("Hotel listesi sistemde bulunamadı.");
        }
        //Güncelleme işlemi yapılır.
        public async Task<ServiceResult> UpdateHotelAsync(HotelViewModel ViewModel)
        {
            if (ViewModel.Id <= 0)
                return new ServiceResult("Id değeri geçersiz.");
            var Hotel = await _SolviaHotelManagementDbContext.Hotels.FindAsync(ViewModel.Id);
            if (Hotel is null)
                return new ServiceResult("Veritabanında böyle bir kayıt bulunamadı");
            var entity = _Mapper.Map<Hotel>(ViewModel);
            _SolviaHotelManagementDbContext.ChangeTracker.Clear();
            _SolviaHotelManagementDbContext.Hotels.Update(entity);
            await _SolviaHotelManagementDbContext.SaveChangesAsync();
            return new ServiceResult(entity, "İşlem başarıyla gerçekletirildi.");
        }
    }
}

