using System;
using FluentValidation;
using Vileve.Domain.Commands.Property;

namespace Vileve.Domain.Validations.Property
{
    public abstract class PropertyValidation<T> : AbstractValidator<T> where T : PropertyCommand
    {
        protected void ValidateId()
        {
            RuleFor(p => p.Id)
                .NotEqual(Guid.Empty).WithMessage("Please ensure you have entered the Id");
        }

        protected void ValidateName()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Please ensure you have entered the Name")
                .Length(2, 100).WithMessage("The Name must have between 2 and 100 characters");
        }

        protected void ValidateType()
        {
            RuleFor(p => p.Type)
                .NotNull().WithMessage("Please ensure you have entered the Type")
                .IsInEnum().WithMessage("The Type must be either double(0), string(1) or bool(2)");
        }

        protected void ValidateIsRequired()
        {
            RuleFor(p => p.IsRequired)
                .NotNull().WithMessage("Please ensure you have entered the IsRequired");
        }
    }
}