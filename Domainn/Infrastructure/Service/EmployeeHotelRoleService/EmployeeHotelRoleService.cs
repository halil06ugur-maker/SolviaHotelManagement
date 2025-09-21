using AutoMapper;
using HotelReservation.Domain;
using Microsoft.EntityFrameworkCore;
using SolviaHotelManagement.Domainn.Infrastructure.Interface.EmployeeHotelRoleService;
using SolviaHotelManagement.Models.Entities;
using SolviaHotelManagement.Models.ServiceResult;
using SolviaHotelManagement.Models.ViewModels.EmployeeHotelRole;
using SolviaHotelManagement.Models.ViewModels.HotelRoom;

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
      
            var isAssignedAnotherHotel = await _context.EmployeeHotelRoles.AnyAsync(x =>
                x.EmployeeId == viewModel.EmployeeId &&
                x.HotelId != viewModel.HotelId // Çalışanın farklı bir otelde atanmış olup olmadığını kontrol et
            );

            if (isAssignedAnotherHotel)
            {
                return new ServiceResult("Bu çalışan, belirtilen otelde ve rolde zaten atanmış. Aynı otelde ve rolde birden fazla atama yapılamaz.");
            }

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

        public async Task<ServiceResult> GetEmployeeHotelRoleByIdAsync(int hotelId)
        {
            var employeeHotelRoles = await _context.EmployeeHotelRoles
                .Where(hr => hr.HotelId == hotelId)
                .Include(ehr => ehr.Employee)
                .Include(ehr => ehr.Hotel)
                .Include(ehr => ehr.Role)
                .ToListAsync();

            if (employeeHotelRoles == null || !employeeHotelRoles.Any())
                return new ServiceResult("Atama bulunamadı.");

            var viewModels = _mapper.Map<List<EmployeeHotelRoleViewModel>>(employeeHotelRoles);

            var responseViewModel = viewModels
                .GroupBy(x => new { x.HotelId, HotelName = x.Hotel.name })  // Otel bazlı grup
                .Select(hotelGroup => new
                {
                    HotelId = hotelGroup.Key.HotelId,
                    HotelName = hotelGroup.Key.HotelName,
                    Roles = hotelGroup
                        .GroupBy(x => new { x.RoleId, RoleName = x.Role.Name })  // Altında roller
                        .Select(roleGroup => new
                        {
                            RoleId = roleGroup.Key.RoleId,
                            RoleName = roleGroup.Key.RoleName,
                            Employees = roleGroup.Select(e => new
                            {
                                EmployeeHotelRoleId = e.Id,
                                EmployeeId = e.Employee.Id,
                                EmployeeName = e.Employee.Name,
                                EmployeeSurname = e.Employee.Surname,
                                EmployeePhone = e.Employee.PhoneNumber,
                                EmployeeEmail = e.Employee.Email,
                                EmployeeIsActive = e.Employee.IsActive,  
                            }).ToList()
                        }).ToList()
                }).ToList();

            return new ServiceResult(responseViewModel, "Atamalar başarıyla listelendi.");
        }





        public async Task<ServiceResult> GetEmployeeHotelRoleListAsync()
        {
            // Veritabanındaki EmployeeHotelRoles'u al
            var employeeHotelRoles = await _context.EmployeeHotelRoles
                .Include(ehr => ehr.Employee)
                .Include(ehr => ehr.Hotel)
                .Include(ehr => ehr.Role)
                .ToListAsync();

            // Verinin olup olmadığını kontrol et
            if (employeeHotelRoles == null || !employeeHotelRoles.Any())
                return new ServiceResult("Atama listesi sistemde bulunamadı.");

            var viewModels = _mapper.Map<List<EmployeeHotelRoleViewModel>>(employeeHotelRoles);

            // Grup işlemi sırasında EmployeeHotelRole Id'yi de dahil ediyoruz
            var responseViewModel = viewModels
                .GroupBy(x => new { x.HotelId, HotelName = x.Hotel.name })  // Otel bazlı grup
                .Select(hotelGroup => new
                {
                    HotelId = hotelGroup.Key.HotelId,
                    HotelName = hotelGroup.Key.HotelName,
                    Roles = hotelGroup
                        .GroupBy(x => new { x.RoleId, RoleName = x.Role.Name })  // Altında roller
                        .Select(roleGroup => new
                        {
                            RoleId = roleGroup.Key.RoleId,
                            RoleName = roleGroup.Key.RoleName,
                            Employees = roleGroup.Select(e => new
                            {
                                EmployeeHotelRoleId = e.Id,  // Burada EmployeeHotelRole.Id'yi ekliyoruz
                                EmployeeId = e.Employee.Id,
                                EmployeeName = e.Employee.Name,
                                EmployeeSurname = e.Employee.Surname,
                                EmployeePhone = e.Employee.PhoneNumber,
                                EmployeeEmail = e.Employee.Email,
                                EmployeeIsActive = e.Employee.IsActive
                            }).ToList()
                        }).ToList()
                }).ToList();

            // Burada responseViewModel'i döndürebiliriz
            return new ServiceResult(responseViewModel, "Atama listesi başarıyla getirildi.");
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
