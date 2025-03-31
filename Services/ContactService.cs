using Microsoft.EntityFrameworkCore;
using NATS.Services.Dtos.RequestDtos;
using NATS.Services.Dtos.ResponseDtos;
using NATS.Services.Entities;
using NATS.Services.Exceptions;
using NATS.Services.Interfaces;

namespace NATS.Services;

/// <inheritdoc cref="IContactService"/>
public class ContactService
       :
            AbstractUpsertableService<Contact, ContactUpsertRequestDto>,
            IContactService
{
    private readonly IDbContextFactory<DatabaseContext> _contextFactory;

    public ContactService(
            DatabaseContext context,
            IDbContextFactory<DatabaseContext> contextFactory) : base(context)
    {
        _contextFactory = contextFactory;
    }
    
    /// <inheritdoc />
    public async Task<List<ContactResponseDto>> GetListAsync()
    {
        DatabaseContext context = await _contextFactory.CreateDbContextAsync();
        return await context.Contacts
            .Select(contact => new ContactResponseDto(contact))
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<ContactResponseDto> GetSingleAsync(int id)
    {
        DatabaseContext context = await _contextFactory.CreateDbContextAsync();
        return await context.Contacts
            .Where(c => c.Id == id)
            .Select(dto => new ContactResponseDto(dto))
            .SingleOrDefaultAsync()
            ?? throw GetResourceNotFoundExceptionById(id);
    }

    /// <inheritdoc />
    public async Task<int> CreateAsync(ContactUpsertRequestDto requestDto)
    {
        Contact contact = new Contact
        {
            Type = requestDto.Type,
            Content = requestDto.Content
        };

        return await base.SaveCreatedEntityAsync(contact, requestDto);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(int id, ContactUpsertRequestDto requestDto)
    {
        // Fetch the entity from the database and ensure it exists.
        Contact contact = await Context.Contacts
            .SingleOrDefaultAsync(contact => contact.Id == id)
            ?? throw new ResourceNotFoundException(nameof(Contact), nameof(id), id.ToString());

        // Perform update operation.
        contact.Type = requestDto.Type;
        contact.Content = requestDto.Content;

        // Save changes.
        await base.SaveUpdatedEntityAsync(contact, requestDto);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(int id)
    {
        // Fetch the entity from the database and ensure it exists.
        Contact contact = await Context.Contacts
            .SingleOrDefaultAsync(contact => contact.Id == id)
            ?? throw new ResourceNotFoundException(nameof(Contact), nameof(id), id.ToString());

        await base.SaveDeletedEntityAsync(contact);
    }

    /// <inheritdoc />
    protected override sealed DbSet<Contact> GetRepository(DatabaseContext context)
    {
        return context.Contacts;
    }
}