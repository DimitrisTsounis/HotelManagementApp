namespace HotelManagement.Infrastructure.Models;

public class Booking : EntityBase
{
    public int HotelId { get; set; }
    public string CustomerName { get; set; }
    public int NumberOfPax { get; set; }
}
