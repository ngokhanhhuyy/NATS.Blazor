using NATS.Services.Dtos.RequestDtos;
using NATS.Services.Localization;
using FluentValidation;

namespace NATS.Validation.Validators;

[JetBrains.Annotations.UsedImplicitly]
public class EnquiryUpsertValidator : Validator<EnquiryCreateRequestDto>
{
    private const string phoneNumberRegex = @"^[^\-+][\d\-+]+$";
    private const string emailRegex = @"^\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}\b$";
    
    public EnquiryUpsertValidator()
    {
        RuleFor(dto => dto.FullName)
            .NotEmpty()
            .MaximumLength(50)
            .WithName(DisplayNames.FullName);
        RuleFor(dto => dto.Email)
            .MaximumLength(255)
            .Matches(emailRegex).WithMessage(ErrorMessages.Invalid)
            .WithName(DisplayNames.Email);
        RuleFor(dto => dto.PhoneNumber)
            .NotEmpty()
            .MaximumLength(15)
            .Matches(phoneNumberRegex).WithMessage(ErrorMessages.Invalid)
            .WithName(DisplayNames.PhoneNumber);
        RuleFor(dto => dto.Content)
            .NotEmpty()
            .MaximumLength(1000)
            .WithName(DisplayNames.Content);
    }
}