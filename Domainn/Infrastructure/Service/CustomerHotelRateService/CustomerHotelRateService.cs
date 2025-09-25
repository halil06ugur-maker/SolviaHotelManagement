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

        public async Task<ServiceResult> GetRateByIdAsync(int hotelId)
        {
            var rates = await _context.CustomerHotelRates
                .Where(r => r.HotelId == hotelId)
                .Include(r => r.Customer)
                .Include(r => r.Hotel)
                .ToListAsync();

            if (!rates.Any())
                return new ServiceResult("Otele ait müşteri puanı bulunamadı.");

            var viewModels = _mapper.Map<List<CustomerHotelRateViewModel>>(rates);

            var response = viewModels
                .Select(r => new
                {
                    RateId = r.Id,
                    HotelId = r.HotelId,
                    HotelName = r.Hotel?.name,
                    CustomerId = r.CustomerId,
                    CustomerName = r.Customer?.Name,
                    r.Rate,
                    r.Description,
                    r.CreatedDate
                })
                .GroupBy(r => new { r.HotelId, r.HotelName })
                .Select(g => new
                {
                    HotelId = g.Key.HotelId,
                    HotelName = g.Key.HotelName,
                    CustomerRatings = g.Select(x => new
                    {
                        x.RateId,
                        x.CustomerId,
                        x.CustomerName,
                        x.Rate,
                        x.Description,
                        x.CreatedDate
                    }).ToList()
                })
                .ToList();

            return new ServiceResult(response, "Müşteri puanları başarıyla getirildi.");
        }

        public async Task<ServiceResult> GetRateListAsync()
        {
            var rates = await _context.CustomerHotelRates
                             .Include(r => r.Customer)
                             .Include(r => r.Hotel)
                             .ToListAsync();

            if (rates.Any())
            {
                var viewModels = _mapper.Map<List<CustomerHotelRateViewModel>>(rates);

                var responseViewModel = viewModels
                    .Select(r => new
                    {
                        RateId = r.Id,
                        HotelId = r.HotelId,
                        HotelName = r.Hotel?.name,
                        CustomerId = r.CustomerId,
                        CustomerName = r.Customer?.Name,
                        r.Rate,
                        r.Description,
                        r.CreatedDate
                    })
                    .GroupBy(x => new { x.HotelId, x.HotelName })
                    .Select(g => new
                    {
                        HotelId = g.Key.HotelId,
                        HotelName = g.Key.HotelName,
                        CustomerRates = g.Select(r => new
                        {
                            r.RateId,
                            r.CustomerId,
                            r.CustomerName,
                            r.Rate,
                            r.Description,
                            r.CreatedDate
                        }).ToList()
                    })
                    .ToList();

                return new ServiceResult(responseViewModel, "Rate listesi başarıyla getirildi.");
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
