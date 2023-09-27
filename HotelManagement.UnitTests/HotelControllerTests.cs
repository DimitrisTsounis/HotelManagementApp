namespace HotelManagement.UnitTests;

public class HotelsControllerTests
{
    private readonly HotelsController controller;
    private readonly Mock<IHotelRepository> hotelRepositoryMock;
    private readonly Mock<IMapper> mapperMock;
    private readonly Mock<IValidator<Hotel>> validatorMock;

    public HotelsControllerTests()
    {
        hotelRepositoryMock = new Mock<IHotelRepository>();
        mapperMock = new Mock<IMapper>();
        validatorMock = new Mock<IValidator<Hotel>>();

        controller = new HotelsController(
            hotelRepositoryMock.Object,
            mapperMock.Object,
            validatorMock.Object);
    }

    [Fact]
    public async Task GivenGetHotels_WhenIsCalled_ThenReturnsOkResult()
    {
        // Given
        var hotels = new List<Hotel>();
        hotelRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(hotels);
        mapperMock.Setup(mapper => mapper.Map<IEnumerable<Hotel_OutputWebDTO>>(hotels)).Returns(new List<Hotel_OutputWebDTO>());

        // When
        var result = await controller.GetHotels();

        // Then
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GivenGetHotelById_WhenWithValidId_ThenReturnsOkResult()
    {
        // Given
        const int hotelId = 1;
        var hotel = new Hotel();
        hotelRepositoryMock.Setup(repo => repo.GetByIdAsync(hotelId)).ReturnsAsync(hotel);
        mapperMock.Setup(mapper => mapper.Map<Hotel_OutputWebDTO>(hotel)).Returns(new Hotel_OutputWebDTO());

        // When
        var result = await controller.GetHotelById(hotelId);

        // Then
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GivenGetHotelByName_WhenWithInValidName_ThenReturnsBadRequest()
    {
        // Given
        const string name = "";

        // When
        var result = await controller.GetHotelByName(name);

        // Then
        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task GivenGetHotelByName_WhenWithValidName_ThenReturnsOkResult()
    {
        // Given
        const string name = "TestHotel";
        var hotels = new List<Hotel>();
        hotelRepositoryMock.Setup(repo => repo.SearchHotelsByNameAsync(name)).ReturnsAsync(hotels);
        mapperMock.Setup(mapper => mapper.Map<IEnumerable<Hotel_OutputWebDTO>>(hotels)).Returns(new List<Hotel_OutputWebDTO>());

        // When
        var result = await controller.GetHotelByName(name);

        // Then
        result.Should().BeOfType<OkObjectResult>();
    }
}
