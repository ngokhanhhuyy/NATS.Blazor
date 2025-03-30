using Microsoft.AspNetCore.Components;
using NATS.Helpers;
using NATS.Models;
using NATS.Services.Dtos.ResponseDtos;
using NATS.Services.Interfaces;

namespace NATS.Components.Layout.Public;

public partial class NavigationBar : ComponentBase
{
    #region DependencyInjection
    [Inject]
    public IGeneralSettingsService GeneralSettingsService { get; init; }
    
    [Inject]
    public IRouteHelper RouteHelper { get; set; }
    #endregion

    #region States
    private NavigationBarModel Model { get; set; }
    #endregion

    protected override async Task OnInitializedAsync()
    {
        GeneralSettingsResponseDto generalSettingsResponseDto;
        generalSettingsResponseDto = await GeneralSettingsService.GetAsync();

        Model = new NavigationBarModel
        {
            GeneralSettings = new GeneralSettingsDetailModel(generalSettingsResponseDto)
        };
    }
    
    #region StateStructures
    private class NavigationBarModel
    {
        public GeneralSettingsDetailModel GeneralSettings { get; init; }
    }
    #endregion
}