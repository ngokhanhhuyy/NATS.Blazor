using Microsoft.AspNetCore.Components;
using NATS.Helpers;
using NATS.Models;
using NATS.Services.Dtos.ResponseDtos;
using NATS.Services.Interfaces;

namespace NATS.Components.Layout.Public;

public partial class Footer : ComponentBase
{
    #region DependencyInjection
    [Inject]
    public IGeneralSettingsService GeneralSettingsService { get; set; }
    
    [Inject]
    public IContactService ContactService { get; set; }
    
    [Inject]
    public IRouteHelper RouteHelper { get; set; }
    #endregion
    
    #region States
    private FooterModel Model { get; set; }
    #endregion

    #region LifeCycleEventHandlers
    protected override async Task OnInitializedAsync()
    {
        Task<GeneralSettingsResponseDto> generalSettingsTask;
        generalSettingsTask = GeneralSettingsService.GetAsync();

        Task<List<ContactResponseDto>> contactsTask;
        contactsTask = ContactService.GetListAsync();

        await Task.WhenAll(generalSettingsTask, contactsTask);

        Model = new FooterModel
        {
            GeneralSettings = new GeneralSettingsDetailModel(generalSettingsTask.Result),
            Contacts = contactsTask.Result.Select(dto => new ContactDetailModel(dto)).ToList()
        };
    }
    #endregion
    
    #region ModelStructures
    private class FooterModel
    {
        public GeneralSettingsDetailModel GeneralSettings { get; set; }
        public List<ContactDetailModel> Contacts { get; set; } = new();
    }
    #endregion
}