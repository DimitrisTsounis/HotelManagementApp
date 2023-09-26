using AutoMapper;
using HotelManagement.API.WebDTOs;
using HotelManagement.Infrastructure.Models;

namespace HotelManagement.API.Mappers;

public class BookingProfile : Profile
{
    public BookingProfile()
    {
        CreateMap<BookingManipulation_WebDTO, Booking>();
    }
}
