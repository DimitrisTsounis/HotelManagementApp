namespace HotelManagement.Infrastructure.Models;

public class Hotel : EntityBase
{
    public string Name { get; set; }
    public string Address { get; set; }
    public short StarRating { get; set; }
}