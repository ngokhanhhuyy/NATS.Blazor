using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace NATS.Components;

public partial class FormHandler : ComponentBase
{
    #region DependencyInjection
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    #endregion

    #region Parameter
    [SupplyParameterFromForm]
    private FormModel Model { get; set; } = new FormModel();
    #endregion
    
    #region InternalState
    private EditContext EditContext { get; set; }
    private ValidationMessageStore ValidationMessageStore { get; set; }
    private List<int> MyNumbers { get; set; } = [.. Enumerable.Range(0, 10)];
    private int CurrentValue { get; set; }
    #endregion

    #region  LifeCycle
    protected override void OnInitialized()
    {
        EditContext = new EditContext(Model);
        ValidationMessageStore = new ValidationMessageStore(EditContext);
        
        Random random = new();
        CurrentValue = random.Next(0, 100);
    }
    #endregion

    #region Callbacks
    private void Authenticate()
    {
        string password = Model.Password;
        Model.Password = string.Empty;
        if (Model.UserName != "ngokhanhhuyy")
        {
            ValidationMessageStore?.Add(() => Model.UserName, "Username doesn't exist.");
            return;
        }

        if (password != "huy123")
        {
            ValidationMessageStore?.Add(() => Model.Password, "Password is incorrect.");
            return;
        }

        NavigationManager?.NavigateTo("/");
    }
    #endregion

    #region Model
    private class FormModel
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        
        [Required]
        public string Password { get; set; } = string.Empty;
    }
    #endregion
}