using ElegantGlamour.Api.Dtos;
using ElegantGlamour.Core.Error;
using FluentValidation;

namespace ElegantGlamour.Api.Validators
{
    public class UpdateCategoryDtoValidator : AbstractValidator<UpdatePrestationCategoryDto>
    {
        public UpdateCategoryDtoValidator()
        {
            RuleFor(c => c.Title)
                .NotEmpty().WithMessage(ErrorMessage.Err_Category_Title_Not_Empty)
                .MaximumLength(50).WithMessage(ErrorMessage.Err_Category_Title_Max_Size);
        }
    }
}