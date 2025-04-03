using Microsoft.EntityFrameworkCore;
using NATS.Services.Dtos.RequestDtos;
using NATS.Services.Dtos.ResponseDtos;
using NATS.Services.Entities;
using NATS.Services.Interfaces;

namespace NATS.Services;

/// <inheritdoc cref="ISummaryItemService" />
public class SummaryItemService
    :
        AbstractHasThumbnailService<SummaryItem, SummaryItemUpdateRequestDto>,
        ISummaryItemService
{
    private readonly IDbContextFactory<DatabaseContext> _contextFactory;

    public SummaryItemService(
            DatabaseContext context,
            IDbContextFactory<DatabaseContext> contextFactory,
            IPhotoService photoService) : base(context, photoService)
    {
        _contextFactory = contextFactory;
    }

    /// <inheritdoc />
    public async Task<List<SummaryItemResponseDto>> GetListAsync()
    {
        await using DatabaseContext context = await _contextFactory.CreateDbContextAsync();
        return await context.SummaryItems
            .OrderBy(summaryItem => summaryItem.Id)
            .Take(4)
            .Select(summaryItem => new SummaryItemResponseDto(summaryItem))
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<SummaryItemResponseDto> GetSingleAsync(int id)
    {
        await using DatabaseContext context = await _contextFactory.CreateDbContextAsync();
        return await context.SummaryItems
            .Select(summaryItem => new SummaryItemResponseDto(summaryItem))
            .SingleOrDefaultAsync(ii => ii.Id == id)
            ?? throw GetResourceNotFoundExceptionById(id);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(int id, SummaryItemUpdateRequestDto requestDto)
    {
        // Fetch the entity from the database and ensure it exists.
        SummaryItem item = await Context.SummaryItems
            .SingleOrDefaultAsync(ii => ii.Id == id)
            ?? throw GetResourceNotFoundExceptionById(id);

        // Update the entity's properties.
        item.Name = requestDto.Name;
        item.SummaryContent = requestDto.SummaryContent;
        item.DetailContent = requestDto.DetailContent;

        // Save changes.
        await base.SaveUpdatedEntityAsync(item, requestDto);
    }

    /// <inheritdoc />
    protected override sealed DbSet<SummaryItem> GetRepository(DatabaseContext context)
    {
        return context.SummaryItems;
    }
}