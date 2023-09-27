using FluentValidation;
using HotelManagement.Infrastructure.Models;

namespace HotelManagement.Infrastructure.Validators;
public class BookingValidator : AbstractValidator<Booking>
{
    public BookingValidator()
    {
        RuleFor(it => it.HotelId)
            .NotNull();

        RuleFor(it => it.HotelId)
            .GreaterThan(0);

        RuleFor(it => it.CustomerName)
            .NotEmpty();
    }
}
