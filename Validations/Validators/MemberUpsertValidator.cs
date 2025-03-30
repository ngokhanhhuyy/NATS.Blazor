using NATS.Services.Dtos.RequestDtos;
using NATS.Services.Localization;
using FluentValidation;

namespace NATS.Validation.Validators;

[JetBrains.Annotations.UsedImplicitly]
public class MemberUpsertValidator : Validator<MemberUpsertRequestDto>
{
    public MemberUpsertValidator()
    {
        RuleFor(dto => dto.ThumbnailFile)
            .Must(IsValidImage)
            .WithMessage(ErrorMessages.Invalid)
            .When(dto => dto.ThumbnailChanged && dto.ThumbnailFile != null)
            .WithName(DisplayNames.Photo);
        RuleFor(dto => dto.FullName)
            .NotEmpty()
            .MaximumLength(50)
            .WithName(DisplayNames.FullName);
        RuleFor(dto => dto.RoleName)
            .NotEmpty()
            .MaximumLength(50)
            .WithName(DisplayNames.RoleName);
        RuleFor(dto => dto.Description)
            .NotEmpty()
            .MaximumLength(400)
            .WithName(DisplayNames.Description);
    }
}