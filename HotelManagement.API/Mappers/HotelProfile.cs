using AutoMapper;
using HotelManagement.API.WebDTOs;
using HotelManagement.Infrastructure.Models;

namespace HotelManagement.API.Mappers;

public class HotelProfile : Profile
{
    public HotelProfile()
    {
        CreateMap<Hotel_ManipulationWebDTO, Hotel>(MemberList.Source);
        CreateMap<Hotel, Hotel_OutputWebDTO>(MemberList.Destination);
    }
}
