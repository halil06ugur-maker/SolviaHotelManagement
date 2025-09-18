using HotelReservation.Domain;
using Microsoft.EntityFrameworkCore;
using SolviaHotelManagement.Domainn.Infrastructure.Interface.HotelAdressService;
using SolviaHotelManagement.Domainn.Infrastructure.Interface.HotelImageService;
using SolviaHotelManagement.Domainn.Infrastructure.Interface.HotelProperty;
using SolviaHotelManagement.Domainn.Infrastructure.Interface.HotelRoomService;
using SolviaHotelManagement.Domainn.Infrastructure.Interface.HotelService;
using SolviaHotelManagement.Domainn.Infrastructure.Interface.RoomService;
using SolviaHotelManagement.Domainn.Infrastructure.Service.HotelAddresservice;
using SolviaHotelManagement.Domainn.Infrastructure.Service.HotelImageService;
using SolviaHotelManagement.Domainn.Infrastructure.Service.HotelService;
using SolviaHotelManagement.Domainn.Infrastructure.Service.RoomService;
using SolviaHotelManagement.Models.Utilities.AutoMapper;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SolviaHotelManagementDbContext>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));


//Dependency Injection

//builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IHotelService, HotelService>();
//builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IHotelAddressService, HotelAddressService>();
builder.Services.AddScoped<IHotelPropertyService, HotelPropertyService>();
builder.Services.AddScoped<IHotelImageService, HotelImageService>();
builder.Services.AddScoped<IHotelRoomService, HotelRoomService>();
builder.Services.AddScoped<IRoomService, RoomService>();

//Cors Setting
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy
        .AllowAnyOrigin() //Herhangi bir origin(kaynak) izinli
        .AllowAnyMethod() //GET,POST,PUT,DELETE vb.
        .AllowAnyHeader(); //Deader bilgilerine izin
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
