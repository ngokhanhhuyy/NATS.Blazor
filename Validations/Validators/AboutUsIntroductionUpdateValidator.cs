using NATS.Services.Dtos.RequestDtos;
using NATS.Services.Localization;
using FluentValidation;

namespace NATS.Validation.Validators;

[JetBrains.Annotations.UsedImplicitly]
public class AboutUsIntroductionUpdateValidator
        : Validator<AboutUsIntroductionUpdateRequestDto>
{
    public AboutUsIntroductionUpdateValidator()
    {
        RuleFor(dto => dto.ThumbnailFile)
            .Must(IsValidImage)
            .WithMessage(ErrorMessages.Invalid)
            .When(dto => dto.ThumbnailFile != null)
            .WithName(DisplayNames.MainPhoto);
        RuleFor(dto => dto.MainQuoteContent)
            .NotEmpty()
            .MaximumLength(1000)
            .WithName(DisplayNames.MessageFromUs);
        RuleFor(dto => dto.AboutUsContent)
            .NotEmpty()
            .MaximumLength(1500)
            .WithName(DisplayNames.AboutUs);
        RuleFor(dto => dto.WhyChooseUsContent)
            .NotEmpty()
            .MaximumLength(1500)
            .WithName(DisplayNames.WhyChooseUs);
        RuleFor(dto => dto.OurDifferenceContent)
            .NotEmpty()
            .MaximumLength(1500)
            .WithName(DisplayNames.OurDifference);
        RuleFor(dto => dto.OurCultureContent)
            .NotEmpty()
            .MaximumLength(1500)
            .WithName(DisplayNames.OurCulture);
    }
}