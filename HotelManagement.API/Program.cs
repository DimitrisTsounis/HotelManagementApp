using FluentValidation;
using HotelManagement.API.Middlewares;
using HotelManagement.Infrastructure;
using HotelManagement.Infrastructure.Models;
using HotelManagement.Infrastructure.Repositories;
using HotelManagement.Infrastructure.Validators;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<HotelManagementDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));
builder.Logging.ClearProviders();
builder.Logging.AddConsole();


builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IHotelRepository), typeof(HotelRepository));
builder.Services.AddScoped(typeof(IBookingRepository), typeof(BookingRepository));

builder.Services.AddScoped<IValidator<Hotel>, HotelValidator>();
builder.Services.AddScoped<IValidator<Booking>, BookingValidator>();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseDatabaseMigrationMiddleware();

app.MapControllers();

app.Run();