using AutoMapper;
using HotelReservation.Domain;
using Microsoft.EntityFrameworkCore;
using SolviaHotelManagement.Domainn.Infrastructure.Interface.EmployeeHotelRoleService;
using SolviaHotelManagement.Models.Entities;
using SolviaHotelManagement.Models.ServiceResult;
using SolviaHotelManagement.Models.ViewModels.EmployeeHotelRole;

namespace SolviaHotelManagement.Domainn.Infrastructure.Service.EmployeeHotelRoleService
{
    public class EmployeeHotelRoleService : IEmployeeHotelRoleService
    {
        private readonly SolviaHotelManagementDbContext _context;
        private readonly IMapper _mapper;

        public EmployeeHotelRoleService(SolviaHotelManagementDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResult> AddEmployeeHotelRoleAsync(EmployeeHotelRoleViewModel viewModel)
        {
            var entity = _mapper.Map<EmployeeHotelRole>(viewModel);
            await _context.EmployeeHotelRoles.AddAsync(entity);
            await _context.SaveChangesAsync();
            return new ServiceResult(entity, "Atama başarıyla eklendi.");
        }

        public async Task<ServiceResult> DeleteEmployeeHotelRoleAsync(int id)
        {
            var entity = await _context.EmployeeHotelRoles.FindAsync(id);
            if (entity is null)
                return new ServiceResult("Sistemde böyle bir kayıt bulunamadı.");
            _context.EmployeeHotelRoles.Remove(entity);
            await _context.SaveChangesAsync();
            return new ServiceResult(entity, "Atama başarıyla silindi.");
        }

        public async Task<ServiceResult> GetEmployeeHotelRoleByIdAsync(int id)
        {
            var entity = await _context.EmployeeHotelRoles
                .Include(ehr => ehr.Employee)
                .Include(ehr => ehr.Hotel)
                .Include(ehr => ehr.Role)
                .FirstOrDefaultAsync(ehr => ehr.Id == id);
            if (entity is null)
                return new ServiceResult(null, "Atama sistemde bulunamadı.");
            return new ServiceResult(entity, "Atama başarıyla gerçekleştirildi.");
        }

        public async Task<ServiceResult> GetEmployeeHotelRoleListAsync()
        {
            var list = await _context.EmployeeHotelRoles
                .Include(ehr => ehr.Employee)
                .Include(ehr => ehr.Hotel)
                .Include(ehr => ehr.Role)
                .ToListAsync();
            if (list.Any())
                return new ServiceResult(list, "Atama listesi başarıyla getirildi.");
            else
                return new ServiceResult("Atama listesi sistemde bulunamadı.");
        }

        public async Task<ServiceResult> UpdateEmployeeHotelRoleAsync(EmployeeHotelRoleViewModel viewModel)
        {
            if (viewModel.Id <= 0)
                return new ServiceResult("Id değeri geçersizdir.");
            var existing = await _context.EmployeeHotelRoles.FindAsync(viewModel.Id);
            if (existing is null)
                return new ServiceResult("Sistemde böyle bir atama bulunamadı.");

            var entity = _mapper.Map<EmployeeHotelRole>(viewModel);
            _context.ChangeTracker.Clear();
            _context.EmployeeHotelRoles.Update(entity);
            await _context.SaveChangesAsync();
            return new ServiceResult(entity, "Atama başarıyla güncellendi.");
        }
    }
}
