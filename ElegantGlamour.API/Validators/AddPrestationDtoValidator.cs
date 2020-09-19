using FluentValidation;
using ElegantGlamour.Core.Dtos;
using ElegantGlamour.Core.Error;

namespace ElegantGlamour.Api.Validators
{
    public class AddPrestationDtoValidator : AbstractValidator<AddPrestationDto>
    {
        public AddPrestationDtoValidator()
        {
            RuleFor(p => p.Description).NotEmpty().WithMessage(ErrorMessage.Err_Prestation_Description_Not_Empty).NotNull().WithMessage(ErrorMessage.Err_Prestation_Description_Not_Empty);
            RuleFor(p => p.Title).NotEmpty().WithMessage(ErrorMessage.Err_Prestation_Title_Not_Empty).NotNull().WithMessage(ErrorMessage.Err_Prestation_Title_Not_Empty);
            RuleFor(p => p.Price).NotEmpty().WithMessage(ErrorMessage.Err_Prestation_Price_Not_Empty).NotNull().WithMessage(ErrorMessage.Err_Prestation_Price_Not_Empty);
            RuleFor(p => p.Duration).NotEmpty().WithMessage(ErrorMessage.Err_Prestation_Duration_Not_Empty).NotNull().WithMessage(ErrorMessage.Err_Prestation_Duration_Not_Empty).NotEqual(0).WithMessage(ErrorMessage.Err_Prestation_Duration_Not_Equal_To_0);
            RuleFor(p => p.CategoryId).NotEmpty().WithMessage(ErrorMessage.Err_Category_Not_Empty).NotNull().WithMessage(ErrorMessage.Err_Category_Not_Empty);

        }
    }
}