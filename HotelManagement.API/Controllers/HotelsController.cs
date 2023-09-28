using AutoMapper;
using FluentValidation;
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
    private readonly IValidator<Hotel> validator;

    public HotelsController(IHotelRepository hotelRepository, IMapper mapper, IValidator<Hotel> validator)
    {
        this.hotelRepository = hotelRepository;
        this.mapper = mapper;
        this.validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> GetHotels()
    {
        IEnumerable<Hotel> hotels = await hotelRepository.GetAllAsync();
        var dto = hotels.Select(mapper.Map<Hotel_OutputWebDTO>);

        return Ok(dto);
    }

    [HttpGet("id/{id}")]
    public async Task<IActionResult> GetHotelById([FromRoute] int id)
    {
        Hotel hotel = await hotelRepository.GetByIdAsync(id);

        if (hotel == null)
            return NotFound();
        
        return Ok(mapper.Map<Hotel_OutputWebDTO>(hotel));
    }

    [HttpGet("name/{name}")]
    public async Task<IActionResult> GetHotelByName([FromRoute] string name)
    {
        if (name.IsNullOrEmpty())
            return BadRequest("Search term 'name' should have a valid value.");

        IEnumerable<Hotel> hotels = await hotelRepository.SearchHotelsByNameAsync(name);
        var dto = hotels.Select(mapper.Map<Hotel_OutputWebDTO>);

        return Ok(dto));
    }

    [HttpPost]
    public async Task<IActionResult> CreateHotel([FromBody] Hotel hotel)
    {
        await validator.ValidateAndThrowAsync(hotel);
        hotelRepository.Create(hotel);
        await hotelRepository.SaveAsync();

        Hotel_OutputWebDTO outputHotel = mapper.Map<Hotel_OutputWebDTO>(hotel);

        return Created(string.Empty, outputHotel);
    }

    [HttpPatch("id/{id}")]
    public async Task<IActionResult> UpdateHotel([FromRoute] int id, [FromBody] Hotel_ManipulationWebDTO hotelDTO)
    {
        Hotel existingHotel = await hotelRepository.GetByIdAsync(id);

        if (existingHotel is null)
            return NotFound($"No hotel was found for the requested Id: {id}");

        mapper.Map(hotelDTO, existingHotel);
        await validator.ValidateAndThrowAsync(existingHotel);

        hotelRepository.Update(existingHotel);
        await hotelRepository.SaveAsync();

        return Ok();
    }

    [HttpDelete("id/{id}")]
    public async Task<IActionResult> DeleteHotel([FromRoute] int id)
    {
        Hotel existingHotel = await hotelRepository.GetByIdAsync(id);

        if (existingHotel is null)
            return NotFound($"No hotel was found for the requested Id: {id}");

        await hotelRepository.DeleteAsync(id);
        await hotelRepository.SaveAsync();

        return Ok();
    }
}