using Microsoft.EntityFrameworkCore;
using NATS.Services.Dtos.RequestDtos;
using NATS.Services.Dtos.ResponseDtos;
using NATS.Services.Entities;
using NATS.Services.Interfaces;

namespace NATS.Services;

/// <inheritdoc cref="ISliderItemService"/>
public class SliderItemService
        :
            AbstractHasThumbnailService<SliderItem, SliderItemUpsertRequestDto>,
            ISliderItemService
{
    private readonly IDbContextFactory<DatabaseContext> _contextFactory;
    public SliderItemService(
            DatabaseContext context,
            IDbContextFactory<DatabaseContext> contextFactory,
            IPhotoService photoService) : base(context, photoService)
    {
        _contextFactory = contextFactory;
    }

    /// <inheritdoc />
    public async Task<List<SliderItemResponseDto>> GetListAsync()
    {
        await using DatabaseContext context = await _contextFactory.CreateDbContextAsync();
        return await context.SliderItems
            .OrderBy(sliderItem => sliderItem.Index)
            .Select(sliderItem => new SliderItemResponseDto(sliderItem))
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<SliderItemResponseDto> GetSingleAsync(int id)
    {
        await using DatabaseContext context = await _contextFactory.CreateDbContextAsync();
        return await context.SliderItems
            .Select(sliderItem => new SliderItemResponseDto(sliderItem))
            .SingleOrDefaultAsync(sliderItem => sliderItem.Id == id)
            ?? throw GetResourceNotFoundExceptionById(id);
    }

    /// <inheritdoc />
    public async Task<int> CreateAsync(SliderItemUpsertRequestDto requestDto)
    {
        // Initialize a new entity.
        SliderItem sliderItem = new SliderItem
        {
            Title = requestDto.Title,
            Index = (await Context.SliderItems.MaxAsync(i => i.Index)) + 1
        };

        return await base.SaveCreatedEntityAsync(sliderItem, requestDto);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(int id, SliderItemUpsertRequestDto requestDto)
    {
        // Fetch the entity from the database and ensure it exists.
        SliderItem sliderItem = await Context.SliderItems
            .SingleOrDefaultAsync(i => i.Id == id)
            ?? throw GetResourceNotFoundExceptionById(id);

        // Save changes.
        await base.SaveUpdatedEntityAsync(sliderItem, requestDto);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(int id)
    {
        // Fetch the entity from the database and ensure it exists.
        SliderItem sliderItem = await Context.SliderItems
            .SingleOrDefaultAsync(i => i.Id == id)
            ?? throw GetResourceNotFoundExceptionById(id);

        await base.SaveDeletedEntityAsync(sliderItem);
    }

    /// <inheritdoc />
    protected override sealed DbSet<SliderItem> GetRepository(DatabaseContext context)
    {
        return context.SliderItems;
    }
}