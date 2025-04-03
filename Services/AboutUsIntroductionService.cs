using Microsoft.EntityFrameworkCore;
using NATS.Services.Dtos.RequestDtos;
using NATS.Services.Dtos.ResponseDtos;
using NATS.Services.Entities;
using NATS.Services.Interfaces;

namespace NATS.Services;

/// <inheritdoc cref="IAboutUsIntroductionService" />
public class AboutUsIntroductionService
        :
            AbstractHasThumbnailService<
                AboutUsIntroduction,
                AboutUsIntroductionUpdateRequestDto>,
            IAboutUsIntroductionService
{
    private readonly IDbContextFactory<DatabaseContext> _contextFactory;

    public AboutUsIntroductionService(
            DatabaseContext context,
            IDbContextFactory<DatabaseContext> contextFactory,
            IPhotoService photoService) : base(context, photoService)
    {
        _contextFactory = contextFactory;
    }

    /// <inheritdoc />
    public async Task<AboutUsIntroductionResponseDto> GetAsync()
    {
        await using DatabaseContext context = await _contextFactory.CreateDbContextAsync();
        return await context.AboutUsIntroductions
            .Select(aui => new AboutUsIntroductionResponseDto(aui))
            .SingleAsync();
    }

    /// <inheritdoc />
    public async Task UpdateAsync(AboutUsIntroductionUpdateRequestDto requestDto)
    {
        // Fetch the entity from the database.
        AboutUsIntroduction introduction = await Context.AboutUsIntroductions.SingleAsync();

        // Update the entity's properties.
        introduction.MainQuoteContent = requestDto.MainQuoteContent;
        introduction.AboutUsContent = requestDto.AboutUsContent;
        introduction.WhyChooseUsContent = requestDto.WhyChooseUsContent;
        introduction.OurDifferenceContent = requestDto.OurDifferenceContent;
        introduction.OurCultureContent = requestDto.OurCultureContent;

        await base.SaveUpdatedEntityAsync(introduction, requestDto);
    }

    protected override sealed DbSet<AboutUsIntroduction> GetRepository(DatabaseContext context)
    {
        return context.AboutUsIntroductions;
    }
}