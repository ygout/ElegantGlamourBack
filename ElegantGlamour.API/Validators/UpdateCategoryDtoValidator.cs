using ElegantGlamour.Core.Dtos;
using ElegantGlamour.Core.Error;
using FluentValidation;

namespace ElegantGlamour.Api.Validators
{
    public class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryDtoValidator()
        {
            RuleFor(c => c.Title)
                .NotEmpty().WithMessage(ErrorMessage.Err_Category_Not_Empty)
                .MaximumLength(50).WithMessage(ErrorMessage.Err_Category_Max_Size);
        }
    }
}