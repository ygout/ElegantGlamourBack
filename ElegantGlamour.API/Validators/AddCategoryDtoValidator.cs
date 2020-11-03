using FluentValidation;
using ElegantGlamour.Api.Dtos;
using ElegantGlamour.Core.Error;

namespace ElegantGlamour.Api.Validators
{
    public class AddCategoryDtoValidator : AbstractValidator<AddPrestationCategoryDto>
    {
        public AddCategoryDtoValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage(ErrorMessage.Err_Category_Title_Not_Empty)
                .MaximumLength(50).WithMessage(ErrorMessage.Err_Category_Title_Max_Size);
        }
    }
}