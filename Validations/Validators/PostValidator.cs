using NATS.Services.Dtos.RequestDtos;
using NATS.Services.Localization;
using FluentValidation;

namespace NATS.Validation.Validators;

[JetBrains.Annotations.UsedImplicitly]
public class PostValidator : Validator<PostUpsertRequestDto>
{
    public PostValidator()
    {
        RuleFor(dto => dto.Title)
            .NotEmpty()
            .MaximumLength(150)
            .WithName(DisplayNames.Title);
        RuleFor(dto => dto.Content)
            .NotEmpty()
            .MaximumLength(10000)
            .WithName(DisplayNames.Content);
        RuleFor(dto => dto.ThumbnailFile)
            .Must(IsValidImage)
            .When(dto => dto.ThumbnailFile != null)
            .WithName(DisplayNames.ThumbnailFile);
    }
}