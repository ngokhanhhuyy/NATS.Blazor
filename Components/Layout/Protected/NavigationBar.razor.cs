using Microsoft.AspNetCore.Components;
using NATS.Models;
using NATS.Services.Dtos.ResponseDtos;
using NATS.Services.Interfaces;
namespace NATS.Components.Layout.Protected;

public partial class NavigationBar : ComponentBase
{
    #region InjectedDpendencies
    private readonly IAuthenticationService _authenticationService;
    private readonly IGeneralSettingsService _generalSettingsService;
    private readonly NavigationManager _navigationManager;
    #endregion

    #region States
    private NavigationBarModel _model;
    #endregion

    public NavigationBar(
            IAuthenticationService authenticationService,
            IGeneralSettingsService generalSettingsService,
            NavigationManager navigationManager)
    {
        _authenticationService = authenticationService;
        _generalSettingsService = generalSettingsService;
        _navigationManager = navigationManager;
    }

    #region LifeCycleMethods
    protected override async Task OnInitializedAsync()
    {
        Task<GeneralSettingsResponseDto> generalSettingsResponseDto;
        generalSettingsResponseDto = _generalSettingsService.GetAsync();

        await Task.WhenAll(generalSettingsResponseDto);

        _model = new NavigationBarModel
        {
            GeneralSettings = new GeneralSettingsDetailModel(generalSettingsResponseDto.Result)
        };
    }
    #endregion

    #region ModelStructure
    private class NavigationBarModel
    {
        public GeneralSettingsDetailModel GeneralSettings { get; set; }
    }
    #endregion
}
