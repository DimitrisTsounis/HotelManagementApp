using AutoMapper;
using HotelManagement.API.WebDTOs;
using HotelManagement.Infrastructure.Models;
using HotelManagement.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HotelManagement.Controllers;

[ApiController]
[Route("api/hotels")]
public class HotelsController : ControllerBase
{
    private readonly IHotelRepository hotelRepository;
    private readonly IMapper mapper;

    public HotelsController(IHotelRepository hotelRepository, IMapper mapper)
    {
        this.hotelRepository = hotelRepository;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetHotels()
    {
        IEnumerable<Hotel> hotels = await hotelRepository.GetAllAsync();

        return Ok(hotels);
    }

    [HttpGet("id/{id}")]
    public async Task<IActionResult> GetHotelById([FromRoute] int id)
    {
        Hotel hotel = await hotelRepository.GetByIdAsync(id);

        if (hotel == null)
            return NotFound();
        
        return Ok(hotel);
    }

    [HttpGet("name/{name}")]
    public async Task<IActionResult> GetHotelByName([FromRoute] string name)
    {
        if (name.IsNullOrEmpty())
            return BadRequest("Search term 'name' should have a valid value.");

        IEnumerable<Hotel> hotels = await hotelRepository.SearchHotelsByNameAsync(name);

        return Ok(hotels);
    }

    [HttpPost]
    public async Task<IActionResult> CreateHotel([FromBody] Hotel hotel)
    {
        hotelRepository.Create(hotel);
        await hotelRepository.SaveAsync();

        return Created(string.Empty, hotel);
    }

    [HttpPatch("id/{id}")]
    public async Task<IActionResult> UpdateHotel([FromRoute] int id, [FromBody] HotelManipulation_WebDTO hotelDTO)
    {
        Hotel existingHotel = await hotelRepository.GetByIdAsync(id);

        if (existingHotel is null)
            return NotFound($"No hotel was found with the request Id: {id}");

        mapper.Map(hotelDTO, existingHotel);
        hotelRepository.Update(existingHotel);
        await hotelRepository.SaveAsync();

        return Ok();
    }

    [HttpDelete("id/{id}")]
    public async Task<IActionResult> DeleteHotel([FromRoute] int id)
    {
        Hotel existingHotel = await hotelRepository.GetByIdAsync(id);

        if (existingHotel is null)
            return NotFound($"No hotel was found with the request Id: {id}");

        await hotelRepository.DeleteAsync(id);
        await hotelRepository.SaveAsync();

        return Ok();
    }
}