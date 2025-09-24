using AutoMapper;
using HotelReservation.Domain;
using Microsoft.EntityFrameworkCore;
using SolviaHotelManagement.Domainn.Infrastructure.Interface.CustomerService;
using SolviaHotelManagement.Models.Entities;
using SolviaHotelManagement.Models.ServiceResult;
using SolviaHotelManagement.Models.ViewModels.Customer;

namespace SolviaHotelManagement.Domainn.Infrastructure.Service.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly SolviaHotelManagementDbContext _context;
        private readonly IMapper _mapper;

        public CustomerService(SolviaHotelManagementDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResult> AddCustomerAsync(CustomerViewModel viewModel)
        {
            var entity = _mapper.Map<Customer>(viewModel);
            await _context.Customers.AddAsync(entity);
            await _context.SaveChangesAsync();
            return new ServiceResult(entity, "Müşteri başarıyla eklendi.");
        }

        public async Task<ServiceResult> DeleteCustomerAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer is null)
                return new ServiceResult("Müşteri bulunamadı.");
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return new ServiceResult(customer, "Müşteri başarıyla silindi.");
        }

        public async Task<ServiceResult> GetCustomerByIdAsync(int id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
            if (customer is null)
                return new ServiceResult(null, "Müşteri bulunamadı.");
            var result = _mapper.Map<CustomerViewModel>(customer);
            return new ServiceResult(result, "Müşteri başarıyla getirildi.");
        }

        public async Task<ServiceResult> GetCustomerListAsync()
        {
            var customers = await _context.Customers.ToListAsync();
            if (customers.Any())
            {
                var result = _mapper.Map<List<CustomerViewModel>>(customers);
                return new ServiceResult("Customer listesi başarıyla getirildi.");
            }
            return new ServiceResult("Customer listesi bulunamadı.");
        }

        public async Task<ServiceResult> UpdateCustomerAsync(CustomerViewModel viewModel)
        {
            if (viewModel.Id <= 0)
                return new ServiceResult("Geçersiz Id değeri.");
            var existing = await _context.Customers.FindAsync(viewModel.Id);
            if (existing is null)
                return new ServiceResult("Müşteri bulunamadı.");
            var entity = _mapper.Map<Customer>(viewModel);
            _context.ChangeTracker.Clear();
            _context.Customers.Update(entity);
            await _context.SaveChangesAsync();
            return new ServiceResult(entity, "Müşteri başarıyla güncellendi."); 
        }
    }
}
