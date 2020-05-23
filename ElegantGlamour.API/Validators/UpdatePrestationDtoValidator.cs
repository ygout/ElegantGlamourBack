using ElegantGlamour.Core.Dtos;
using FluentValidation;

namespace ElegantGlamour.Api.Validators
{
    public class UpdatePrestationDtoValidator : AbstractValidator<UpdatePrestationDto>
    {
        public UpdatePrestationDtoValidator()
        {
            RuleFor(p => p.Description).NotEmpty().NotNull();
            RuleFor(p => p.Title).NotEmpty().NotNull();
            RuleFor(p => p.Price).NotEmpty().NotNull().NotEqual(0);
            RuleFor(p => p.Duration).NotEmpty().NotNull().NotEqual(0);
            RuleFor(p => p.CategoryId).NotEmpty().NotNull();
        }
    }
}