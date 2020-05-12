using ElegantGlamour.Core.Dtos;
using FluentValidation;

namespace ElegantGlamour.Api.Validators
{
    public class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryDtoValidator()
        {
            RuleFor(c => c.Title)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}