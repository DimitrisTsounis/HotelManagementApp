using AutoMapper;

namespace HotelManagement.API.Mappers;

public class BookingProfile : Profile
{
    public BookingProfile()
    {
        CreateMap<Booking_ManipulationWebDTO, Booking>(MemberList.Source);
        CreateMap<Booking, Booking_OutputWebDTO>(MemberList.Destination);
    }
}
