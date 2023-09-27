using AutoMapper;
using HotelManagement.API.WebDTOs;
using HotelManagement.Infrastructure.Models;

namespace HotelManagement.API.Mappers;

public class BookingProfile : Profile
{
    public BookingProfile()
    {
        CreateMap<Booking_ManipulationWebDTO, Booking>();
        CreateMap<Booking, Booking_OutputWebDTO>();
    }
}
