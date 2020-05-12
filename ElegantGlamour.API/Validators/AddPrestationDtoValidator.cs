using FluentValidation;
using ElegantGlamour.Core.Dtos;

namespace ElegantGlamour.Api.Validators
{
    public class AddPrestationDtoValidator : AbstractValidator<AddPrestationDto>
    {
        public AddPrestationDtoValidator()
        {
            RuleFor(p => p.Description).NotEmpty().NotNull();
            RuleFor(p => p.Title).NotEmpty().NotNull();
            RuleFor(p => p.Price).NotEmpty().NotNull().NotEqual(0);
            RuleFor(p => p.Duration).NotEmpty().NotNull().NotEqual(0);
            RuleFor(p => p.CategoryId).NotEmpty().NotNull();
            // Todo if category exist into db


        }
    }
}