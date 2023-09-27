using FluentValidation;
using HotelManagement.Infrastructure.Models;

namespace HotelManagement.Infrastructure.Validators;
public class HotelValidator : AbstractValidator<Hotel>
{
    public HotelValidator()
    {
        RuleFor(it => it.Name)
            .NotEmpty();

        RuleFor(it => it.StarRating)
            .GreaterThanOrEqualTo((short)1)
            .LessThanOrEqualTo((short)5);
    }
}
