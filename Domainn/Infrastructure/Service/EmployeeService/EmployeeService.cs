using AutoMapper;
using HotelReservation.Domain;
using Microsoft.EntityFrameworkCore;
using SolviaHotelManagement.Domainn.Infrastructure.Interface.EmployeeService;
using SolviaHotelManagement.Models.Entities;
using SolviaHotelManagement.Models.ServiceResult;
using SolviaHotelManagement.Models.ViewModels.Employee;

namespace SolviaHotelManagement.Domainn.Infrastructure.Service.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        private readonly SolviaHotelManagementDbContext _context;
        private readonly IMapper _mapper;

        public EmployeeService(SolviaHotelManagementDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResult> AddEmployeeAsync(EmployeeViewModel viewModel)
        {
            var entity = _mapper.Map<Employee>(viewModel);
            await _context.Employees.AddAsync(entity);
            await _context.SaveChangesAsync();
            return new ServiceResult(entity, "Çalışan başarıyla eklendi.");
        }

        public async Task<ServiceResult> DeleteEmployeeAsync(int id)
        {
            var entity = await _context.Employees.FindAsync(id);
            if (entity is null)
                return new ServiceResult("Sistemde böyle bir kayıt bulunamadı");
            _context.Employees.Remove(entity);
            await _context.SaveChangesAsync();
            return new ServiceResult(entity, "Çalışan başarıyla silindi");
        }

        public async Task<ServiceResult> GetEmployeeByIdAsync(int id)
        {
            var employee = await _context.Employees
         .Include(e => e.EmployeeHotelRoles)
         .FirstOrDefaultAsync(e => e.Id == id);

            // Eğer çalışan bulunamazsa, uygun mesajla hata döndürüyoruz
            if (employee is null)
                return new ServiceResult(null, "Çalışan sistemde bulunamadı.");

            // Çalışan ve ilişkili veriler başarıyla getirilmişse, sonucu döndürüyoruz
            return new ServiceResult(employee, "Çalışan başarıyla getirildi.");
        }

        public async Task<ServiceResult> GetEmployeeListAsync()
        {
            var employeeList = await _context.Employees.ToListAsync();
            if (employeeList.Any())
                return new ServiceResult(employeeList, "Çalışan listesi başarıyla getirildi.");
            else
                return new ServiceResult("Çalışan listesi sistemde bulunamadı.");
        }

        public async Task<ServiceResult> UpdateEmployeeAsync(EmployeeViewModel viewModel)
        {
            if (viewModel.Id <= 0)
                return new ServiceResult("Id değeri geçersizdir.");
            var employee = await _context.Employees.FindAsync(viewModel.Id);
            if (employee is null)
                return new ServiceResult("Sistemde böyle bir çalışan bulunamadı.");
            var entity = _mapper.Map<Employee>(viewModel);
            _context.ChangeTracker.Clear();
            _context.Employees.Update(entity);
            await _context.SaveChangesAsync();
            return new ServiceResult(entity, "Çalışan başarıyla güncellendi");
        }
    }
}
