﻿namespace HotelManagement.IntegrationTests;
public class BaseRepository_IntegrationTests
{
    private readonly DbContextOptions<HotelManagementDbContext> options = new DbContextOptionsBuilder<HotelManagementDbContext>()
        .UseInMemoryDatabase(databaseName: "HotelManagementDatabase")
        .Options;

    [Fact]
    public async Task GivenGetByIdAsync_WhenCalled_ThenReturnsEntity()
    {
        // Given
        using (var context = new HotelManagementDbContext(options))
        {
            context.Hotels.Add(new Hotel { Id = 1, Name = "TestName", Address = "TestAddress", StarRating = 1 });
            context.Hotels.Add(new Hotel { Id = 2, Name = "TestName2", Address = "TestAddress2", StarRating = 2 });
            context.SaveChanges();
        }

        // When & Then
        using (var context = new HotelManagementDbContext(options))
        {
            var baseRepository = new BaseRepository<Hotel>(context);
            var hotel = await baseRepository.GetByIdAsync(1);


            hotel.Should().NotBeNull();
            hotel.Name.Should().Be("TestName");
        }
    }

    [Fact]
    public void GivenCreate_WhenCalled_ThenReturnsNewEntity()
    {
        // Given
        Hotel hotelToBeInserted = new() { Id = 3, Name = "TestName", Address = "TestAddress", StarRating = 1 };

        using var context = new HotelManagementDbContext(options);
        var baseRepository = new BaseRepository<Hotel>(context);

        // When 
        var createdHotel = baseRepository.Create(hotelToBeInserted);

        // Then
        createdHotel.Should().NotBeNull();
        createdHotel.Should().Be(hotelToBeInserted);
    }

    [Fact]
    public async Task GivenUpdate_WhenCalled_ThenEntityShouldBeUpdated()
    {
        // Given
        var hotel = new Hotel { Id = 4, Name = "TestName", Address = "TestAddress", StarRating = 1 };

        using (var context = new HotelManagementDbContext(options))
        {
            context.Hotels.Add(hotel);
            context.SaveChanges();
        }
        hotel.Name = "UpdatedTestName";

        // When & Then
        using (var context = new HotelManagementDbContext(options))
        {
            var baseRepository = new BaseRepository<Hotel>(context);
            
            baseRepository.Update(hotel);
            await baseRepository.SaveAsync();

            var updatedHotel = await baseRepository.GetByIdAsync(4);
            updatedHotel.Should().NotBeNull();
            updatedHotel.Name.Should().Be("UpdatedTestName");
        }
    }
}
