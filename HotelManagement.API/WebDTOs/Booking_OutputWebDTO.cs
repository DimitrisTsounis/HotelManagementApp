namespace HotelManagement.API.WebDTOs;

public class Booking_OutputWebDTO
{
    public int Id { get; set; }
    public int HotelId { get; set; }
    public string CustomerName { get; set; }
    public int? NumberOfPax { get; set; }

}
