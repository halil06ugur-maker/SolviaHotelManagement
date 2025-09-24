using AutoMapper;
using HotelReservation.Domain;
using Microsoft.EntityFrameworkCore;
using SolviaHotelManagement.Domainn.Infrastructure.Interface.CustomerHotelRate;
using SolviaHotelManagement.Models.Entities;
using SolviaHotelManagement.Models.ServiceResult;
using SolviaHotelManagement.Models.ViewModels.CustomerHotelRate;

namespace SolviaHotelManagement.Domainn.Infrastructure.Service.CustomerHotelRateService
{
    public class CustomerHotelRateService : ICustomerHotelRateService
    {
        private readonly SolviaHotelManagementDbContext _context;
        private readonly IMapper _mapper;

        public CustomerHotelRateService(SolviaHotelManagementDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResult> AddRateAsync(CustomerHotelRateViewModel viewModel)
        {
            var entity = _mapper.Map<CustomerHotelRate>(viewModel);
            await _context.CustomerHotelRates.AddAsync(entity);
            await _context.SaveChangesAsync();
            return new ServiceResult(entity, "Rate başarıyla eklendi.");
        }

        public async Task<ServiceResult> DeleteRateAsync(int id)
        {
            var rate = await _context.CustomerHotelRates.FindAsync(id);
            if (rate is null)
                return new ServiceResult("Rate bulunamadı.");

            _context.CustomerHotelRates.Remove(rate);
            await _context.SaveChangesAsync();
            return new ServiceResult(rate, "Rate başarıyla silindi.");
        }

        public async Task<ServiceResult> GetRateByIdAsync(int id)
        {
            var rate = await _context.CustomerHotelRates
                                     .Include(r => r.Customer)
                                     .Include(r => r.Hotel)
                                     .FirstOrDefaultAsync(r => r.Id == id);

            if (rate is null)
                return new ServiceResult(null, "Rate bulunamadı.");

            var result = _mapper.Map<CustomerHotelRateViewModel>(rate);
            return new ServiceResult(result, "Rate başarıyla getirildi.");
        }

        public async Task<ServiceResult> GetRateListAsync()
        {
            var rates = await _context.CustomerHotelRates
                                      .Include(r => r.Customer)
                                      .Include(r => r.Hotel)
                                      .ToListAsync();
            if (rates.Any())
            {
                var result = _mapper.Map<List<CustomerHotelRateViewModel>>(rates);
                return new ServiceResult(result, "Rate listesi başarıyla getirildi.");
            }
            return new ServiceResult("Rate listesi bulunamadı.");
        }

        public async Task<ServiceResult> UpdateRateAsync(CustomerHotelRateViewModel viewModel)
        {
            if (viewModel.Id <= 0)
                return new ServiceResult("Geçersiz Id değeri");
            var existing = await _context.CustomerHotelRates.FindAsync(viewModel.Id);
            if (existing is null)
                return new ServiceResult("Rate bulunamadı.");
            var entity = _mapper.Map<CustomerHotelRate>(viewModel);
            _context.ChangeTracker.Clear();
            _context.CustomerHotelRates.Update(entity);
            await _context.SaveChangesAsync();
            return new ServiceResult(entity, "Rate başarıyla güncellendi.");
        }
    }
}
