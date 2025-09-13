

using Microsoft.EntityFrameworkCore;
using SolviaHotelManagement.Models.Entities;

namespace HotelReservation.Domain;

public sealed class SolviaHotelManagementDbContext(DbContextOptions<SolviaHotelManagementDbContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Hotel> Hotels => Set<Hotel>();
    public DbSet<EmployeeHotelRole> EmployeeHotelRoles => Set<EmployeeHotelRole>();
    public DbSet<HotelProperty> HotelProperties => Set<HotelProperty>();
    public DbSet<HotelAddress> HotelAddresses => Set<HotelAddress>();
    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<HotelRoom> HotelRooms => Set<HotelRoom>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<CustomerHotelRoom> CustomerHotelRooms => Set<CustomerHotelRoom>();
    public DbSet<CustomerHotelRate> CustomerHotelRates => Set<CustomerHotelRate>();
    public DbSet<HotelImage> HotelImages => Set<HotelImage>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        // Employees
        b.Entity<Employee>(e =>
        {
            e.ToTable("Employees");
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(100);
            e.Property(x => x.Surname).IsRequired().HasMaxLength(100);
            e.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(32);
            e.Property(x => x.Email).IsRequired().HasMaxLength(255);
            e.Property(x => x.IsActive).IsRequired();
            e.Property(x => x.CreatedDate).IsRequired();
        });

        // Roles
        b.Entity<Role>(e =>
        {
            e.ToTable("Roles");
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(100);
            e.Property(x => x.Description).HasMaxLength(500);
            e.Property(x => x.CreatedDate).IsRequired();
        });

        // Hotels
        b.Entity<Hotel>(e =>
        {
            e.ToTable("Hotels");
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
            e.Property(x => x.TaxNumber).IsRequired().HasMaxLength(50);
            e.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(32);
            e.Property(x => x.CreatedDate).IsRequired();
        });

        // EmployeeHotelRole (Join)
        b.Entity<EmployeeHotelRole>(e =>
        {
            e.ToTable("EmployeeHotelRole");
            e.HasKey(x => x.Id);
            e.Property(x => x.CreatedDate).IsRequired();
            e.HasOne(x => x.Employee).WithMany(x => x.EmployeeHotelRoles).HasForeignKey(x => x.EmployeeId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(x => x.Hotel).WithMany(x => x.EmployeeHotelRoles).HasForeignKey(x => x.HotelId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(x => x.Role).WithMany(x => x.EmployeeHotelRoles).HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.Cascade);
            // Avoid duplicate triples
            e.HasIndex(x => new { x.EmployeeId, x.HotelId, x.RoleId }).IsUnique();
        });

        // HotelProperty (1:1 Hotel)
        b.Entity<HotelProperty>(e =>
        {
            e.ToTable("HotelProperty");
            e.HasKey(x => x.Id);
            e.Property(x => x.Capacity).IsRequired();
            e.Property(x => x.IsShuttleTransfer).IsRequired();
            e.Property(x => x.IsAnimalAccept).IsRequired();
            e.Property(x => x.IsConcept).IsRequired();
            e.Property(x => x.IsSpa).IsRequired();
            e.Property(x => x.IsAdult).IsRequired();
            // Column name in diagram is 'CreatedDatae' (typo) -> map explicitly
            e.Property(x => x.CreatedDate).HasColumnName("CreatedDate").IsRequired();

            e.HasOne(x => x.Hotel)
                .WithOne(x => x.HotelProperty)
                .HasForeignKey<HotelProperty>(x => x.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            // Enforce 1:1
            e.HasIndex(x => x.HotelId).IsUnique();
        });

        // HotelAddress (1:N Hotel)
        b.Entity<HotelAddress>(e =>
        {
            e.ToTable("HotelAddress");
            e.HasKey(x => x.Id);
            e.Property(x => x.Address).IsRequired().HasMaxLength(500);
            e.Property(x => x.PostCode).IsRequired().HasMaxLength(50);
            e.Property(x => x.CreatedDate).IsRequired();
            e.HasOne(x => x.Hotel).WithMany(x => x.HotelAddresses).HasForeignKey(x => x.HotelId).OnDelete(DeleteBehavior.Cascade);
        });

        // Rooms
        b.Entity<Room>(e =>
        {
            e.ToTable("Rooms");
            e.HasKey(x => x.Id);
            e.Property(x => x.Number).IsRequired().HasMaxLength(50);
            e.Property(x => x.CreatedDate).IsRequired();
        });

        // HotelRoom (Join with payload)
        b.Entity<HotelRoom>(e =>
        {
            e.ToTable("HotelRoom");
            e.HasKey(x => x.Id);
            e.Property(x => x.Type).IsRequired().HasMaxLength(50);
            e.Property(x => x.IsReserved).IsRequired();
            e.Property(x => x.CreatedDate).IsRequired();
            e.HasOne(x => x.Hotel).WithMany(x => x.HotelRooms).HasForeignKey(x => x.HotelId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(x => x.Room).WithMany(x => x.HotelRooms).HasForeignKey(x => x.RoomId).OnDelete(DeleteBehavior.Cascade);
            e.HasIndex(x => new { x.HotelId, x.RoomId }).IsUnique();
        });

        // Customers
        b.Entity<Customer>(e =>
        {
            e.ToTable("Customers");
            e.HasKey(x => x.Id);
            e.Property(x => x.IdentityNumber).IsRequired().HasMaxLength(50);
            e.Property(x => x.Name).IsRequired().HasMaxLength(100);
            e.Property(x => x.Surname).IsRequired().HasMaxLength(100);
            e.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(32);
            e.Property(x => x.Age).IsRequired();
            e.Property(x => x.Gender).IsRequired().HasMaxLength(20);
            e.Property(x => x.CreatedDate).IsRequired();
        });

        // CustomerHotelRoom (Reservation)
        b.Entity<CustomerHotelRoom>(e =>
        {
            e.ToTable("CustomerHotelRoom");
            e.HasKey(x => x.Id);
            e.Property(x => x.StartDate).IsRequired();
            e.Property(x => x.EndDate).IsRequired();
            e.Property(x => x.CreatedDate).IsRequired();

            e.HasOne(x => x.Customer).WithMany(x => x.CustomerHotelRooms).HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(x => x.Hotel).WithMany(x => x.CustomerHotelRooms).HasForeignKey(x => x.HotelId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(x => x.Room).WithMany(x => x.CustomerHotelRooms).HasForeignKey(x => x.RoomId).OnDelete(DeleteBehavior.Restrict);

            // Avoid overlapping duplicates for same customer/hotel/room/date combination (basic uniqueness)
            e.HasIndex(x => new { x.CustomerId, x.HotelId, x.RoomId, x.StartDate, x.EndDate }).IsUnique();
        });

        // CustomerHotelRate (rating per customer per hotel)
        b.Entity<CustomerHotelRate>(e =>
        {
            e.ToTable("CustomerHotelRate");
            e.HasKey(x => x.Id);
            e.Property(x => x.Rate).IsRequired();
            e.Property(x => x.Description).HasMaxLength(500);
            e.Property(x => x.CreatedDate).IsRequired();

            e.HasOne(x => x.Customer).WithMany(x => x.CustomerHotelRates).HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(x => x.Hotel).WithMany(x => x.CustomerHotelRates).HasForeignKey(x => x.HotelId).OnDelete(DeleteBehavior.Cascade);

            e.HasIndex(x => new { x.CustomerId, x.HotelId }).IsUnique();
        });

        // HotelImages
        b.Entity<HotelImage>(e =>
        {
            e.ToTable("HotelImages");
            e.HasKey(x => x.Id);
            e.Property(x => x.ImgUrl).IsRequired().HasMaxLength(2048);
            e.Property(x => x.CreatedDate).IsRequired();
            e.HasOne(x => x.Hotel).WithMany(x => x.HotelImages).HasForeignKey(x => x.HotelId).OnDelete(DeleteBehavior.Cascade);
        });
    }
}
