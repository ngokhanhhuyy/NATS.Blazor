using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using NATS.Extensions;
using NATS.Models;
using NATS.Services.Dtos.RequestDtos;
using NATS.Services.Dtos.ResponseDtos;
using NATS.Services.Exceptions;
using NATS.Services.Interfaces;
using FluentValidation;
using FluentValidation.Results;

namespace NATS.Components;

public partial class SignIn
{
    #region InjectedDepdencies
    private readonly IAuthenticationService _authenticationService;
    private readonly IGeneralSettingsService _generalSettingsService;
    private readonly IValidator<SignInRequestDto> _validator;
    private readonly NavigationManager _navigationManager;
    #endregion
    
    #region Parameters
    [SupplyParameterFromForm]
    public SignInModel Model { get; set; } = new SignInModel();

    [SupplyParameterFromQuery]
    public string ReturnUrl { get; set; }
    #endregion

    #region States
    private GeneralSettingsDetailModel _generalSettings;
    private EditContext _editContext;
    private ValidationMessageStore _validationMessageStore;
    #endregion
    
    #region Constructor
    public SignIn(
            IAuthenticationService authenticationService,
            IGeneralSettingsService generalSettingsService,
            IValidator<SignInRequestDto> validator,
            NavigationManager navigationManager)
    {
        _authenticationService = authenticationService;
        _generalSettingsService = generalSettingsService;
        _validator = validator;
        _navigationManager = navigationManager;
    }
    #endregion
    
    #region LifeCycleMethods
    protected override void OnInitialized()
    {
        _editContext = new EditContext(Model);
        _validationMessageStore = new ValidationMessageStore(_editContext);
    }

    protected override async Task OnInitializedAsync()
    {
        GeneralSettingsResponseDto generalSettingsResponseDto;
        generalSettingsResponseDto = await _generalSettingsService.GetAsync();
        _generalSettings = new GeneralSettingsDetailModel(generalSettingsResponseDto);
    }
    #endregion

    #region Callbacks
    private async Task HandleSubmitAsync()
    {
        _validationMessageStore.Clear();
        try
        {
            SignInRequestDto requestDto = Model.ToRequestDto();
            ValidationResult validationResult = _validator.Validate(requestDto);
            if (!validationResult.IsValid)
            {
                _validationMessageStore.AddFromValidationErrors(
                    _editContext,
                    validationResult.Errors);
                return;
            }
            await _authenticationService.SignInAsync(requestDto);

            if (!string.IsNullOrWhiteSpace(ReturnUrl))
            {
                _navigationManager.NavigateTo(ReturnUrl);
            }
            else
            {
                _navigationManager.NavigateTo("/quan-tri/bang-dieu");
            }
        }
        catch (OperationException exception)
        {
            _validationMessageStore.AddFromServiceException(_editContext, exception);
        }
        finally
        {
            await Task.CompletedTask;
        }
    }
    #endregion
}