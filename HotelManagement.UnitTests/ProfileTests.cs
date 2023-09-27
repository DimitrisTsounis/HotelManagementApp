using AutoMapper;
using HotelManagement.API.Mappers;

namespace HotelManagement.UnitTests;
public class ProfileTests
{
    [Fact]
    public void HotelProfile_Configuration_IsValid()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<HotelProfile>());
        config.AssertConfigurationIsValid();
    }

    [Fact]
    public void BookingProfile_Configuration_IsValid()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<BookingProfile>());
        config.AssertConfigurationIsValid();
    }
}
