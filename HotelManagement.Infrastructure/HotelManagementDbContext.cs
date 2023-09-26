using HotelManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Infrastructure;

public class HotelManagementDbContext : DbContext
{
    public HotelManagementDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Booking> Bookings { get; set; }
}
