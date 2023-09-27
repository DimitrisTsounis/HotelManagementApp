using FluentValidation.TestHelper;
using HotelManagement.Infrastructure.Models;
using HotelManagement.Infrastructure.Validators;
using Xunit;

namespace HotelManagement.UnitTests;
public class ValidatorTests
{
    private HotelValidator HotelValidator {  get; }
    private BookingValidator BookingValidator {  get; }

    public ValidatorTests()
    {
        HotelValidator = new HotelValidator();
        BookingValidator = new BookingValidator();
    }

    [Fact]
    public async Task GivenHotelNameIsEmpty_WhenValidate_ThenValidatorErrorShouldBeThrown()
    {
        // Given
        Hotel hotel = new()
        {
            Id = 1,
            Name = "",
            Address = "TestAddress",
            StarRating = 1
        };

        // When 
        var validationResult = await HotelValidator.TestValidateAsync(hotel);

        // Then
        validationResult.ShouldHaveValidationErrorFor(it => it.Name);
    }

    [Fact]
    public async Task GivenHotelStarRatingIsOutOfRange_WhenValidate_ThenValidatorErrorShouldBeThrown()
    {
        // Given
        Hotel firstHotel = new()
        {
            Id = 1,
            Name = "TestName",
            Address = "TestAddress",
            StarRating = 0
        };

        Hotel secondHotel = new()
        {
            Id = 1,
            Name = "TestName",
            Address = "TestAddress",
            StarRating = 6
        };

        // When 
        var firstValidationResult = await HotelValidator.TestValidateAsync(firstHotel);
        var secondValidationResult = await HotelValidator.TestValidateAsync(secondHotel);

        // Then
        firstValidationResult.ShouldHaveValidationErrorFor(it => it.StarRating);
        secondValidationResult.ShouldHaveValidationErrorFor(it => it.StarRating);
    }

    [Fact]
    public async Task GivenBookingHasNotHotelId_WhenValidate_ThenValidatorErrorShouldBeThrown()
    {
        // Given
        Booking booking = new()
        {
            Id = 1,
            CustomerName = "test",
            NumberOfPax = 1
        };

        // When 
        var validationResult = await BookingValidator.TestValidateAsync(booking);

        // Then
        validationResult.ShouldHaveValidationErrorFor(it => it.HotelId);
    }

    [Fact]
    public async Task GivenBookingHasEmptyCustomerName_WhenValidate_ThenValidatorErrorShouldBeThrown()
    {
        // Given
        Booking booking = new()
        {
            Id = 1,
            HotelId = 1,
            CustomerName = "",
            NumberOfPax = 1
        };

        // When 
        var validationResult = await BookingValidator.TestValidateAsync(booking);

        // Then
        validationResult.ShouldHaveValidationErrorFor(it => it.CustomerName);
    }
}
