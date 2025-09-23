using AutoMapper;
using HotelReservation.Domain;
using Microsoft.EntityFrameworkCore;
using SolviaHotelManagement.Domainn.Infrastructure.Interface.RoleService;
using SolviaHotelManagement.Models.Entities;
using SolviaHotelManagement.Models.ServiceResult;
using SolviaHotelManagement.Models.ViewModels.Role;

namespace SolviaHotelManagement.Domainn.Infrastructure.Service.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly SolviaHotelManagementDbContext _context;
        private readonly IMapper _mapper;

        public RoleService(SolviaHotelManagementDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResult> AddRoleAsync(RoleViewModel viewModel)
        {
            var entity = _mapper.Map<Role>(viewModel);
            await _context.Roles.AddAsync(entity);
            await _context.SaveChangesAsync();
            return new ServiceResult(entity, "Rol başarıyla eklendi.");
        }

        public async Task<ServiceResult> DeleteRoleAsync(int id)
        {
            var entity = await _context.Roles.FindAsync(id);
            if (entity is null)
                return new ServiceResult("Sistemde böyle bir kayıt bulunamadı.");

            _context.Roles.Remove(entity);
            await _context.SaveChangesAsync();
            return new ServiceResult(entity, "Rol başarıyla silindi.");
        }

        public async Task<ServiceResult> GetRoleByIdAsync(int id)
        {
            // `Include` ile EmployeeHotelRoles ilişkisini de getiriyoruz.
            var role = await _context.Roles
                .Include(r => r.EmployeeHotelRoles)  // İlişkili EmployeeHotelRoles'ü dahil et
                .FirstOrDefaultAsync(r => r.Id == id);  // İlk rolü getir (id'ye göre)

            if (role is null)
                return new ServiceResult(null, "Rol sistemde bulunamadı.");

            // Eğer rol bulunursa, başarıyla dönüyoruz
            return new ServiceResult(role, "Rol başarıyla getirildi.");
        }

        public async Task<ServiceResult> GetRoleListAsync()
        {
            var roleList = await _context.Roles.ToListAsync();
            if (roleList.Any())
                return new ServiceResult(roleList, "Rol listesi başarıyla getirildi.");
            else
                return new ServiceResult("Rol listesi istemde bulunamadı.");
        }

        public async Task<ServiceResult> UpdateRoleAsync(RoleViewModel viewModel)
        {
            if (viewModel.Id <= 0)
                return new ServiceResult("Id değeri geçersizdir.");

            var role = await _context.Roles.FindAsync(viewModel.Id);
            if (role is null)
                return new ServiceResult("Sistemde böyle bir rol bulunamadı.");

            // AutoMapper ile var olan entity üzerine map et
            _mapper.Map(viewModel, role);

            await _context.SaveChangesAsync();
            return new ServiceResult(role, "Rol başarıyla güncellendi.");
        }
    }
}
