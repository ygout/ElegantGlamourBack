using FluentValidation;
using ElegantGlamour.Core.Dtos;
using ElegantGlamour.Core.Error;

namespace ElegantGlamour.Api.Validators
{
    public class AddCategoryDtoValidator : AbstractValidator<AddCategoryDto>
    {
        public AddCategoryDtoValidator()
        {
            RuleFor(c => c.Title)
                .NotEmpty().WithMessage(ErrorMessage.Err_Category_Title_Not_Empty)
                .MaximumLength(50).WithMessage(ErrorMessage.Err_Category_Title_Max_Size);
        }
    }
}