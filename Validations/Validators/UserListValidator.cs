using NATS.Services.Dtos.RequestDtos;
using NATS.Services.Localization;
using FluentValidation;

namespace NATS.Validation.Validators;

[JetBrains.Annotations.UsedImplicitly]
public class UserListValidator : Validator<UserListRequestDto>
{
    public UserListValidator()
    {
        RuleFor(dto => dto.Page)
            .NotNull()
            .GreaterThanOrEqualTo(1)
            .WithName(dto => DisplayNames.Get(nameof(dto.Page)));
        RuleFor(dto => dto.ResultsByPage)
            .NotNull()
            .GreaterThanOrEqualTo(5)
            .LessThanOrEqualTo(50)
            .WithName(dto => DisplayNames.Get(nameof(dto.ResultsByPage)));
    }
}