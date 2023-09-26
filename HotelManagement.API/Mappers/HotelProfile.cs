using AutoMapper;
using HotelManagement.API.WebDTOs;
using HotelManagement.Infrastructure.Models;

namespace HotelManagement.API.Mappers;

public class HotelProfile : Profile
{
    public HotelProfile()
    {
        CreateMap<HotelManipulation_WebDTO, Hotel>();
    }
}
