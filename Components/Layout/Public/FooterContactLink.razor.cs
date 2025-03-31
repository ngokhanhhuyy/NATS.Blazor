using Microsoft.AspNetCore.Components;
using NATS.Models;
using NATS.Services.Enums;
namespace NATS.Components.Layout.Public;

public partial class FooterContactLink
{
    #region Parameters
    [Parameter]
    public ContactDetailModel Model { get; set; }
    #endregion

    #region Computed
    private string EncodedAddress
    {
        get
        {
            if (Model.Type != ContactType.Address)
            {
                return null;
            }

            return Uri.EscapeDataString(Model.Content);
        }
    }

    private string IconClassName
    {
        get
        {
            Dictionary<ContactType, string> iconClassNamesByType;
            iconClassNamesByType = new Dictionary<ContactType, string>
            {
                { ContactType.PhoneNumber, "bi-telephone-fill" },
                { ContactType.ZaloNumber, "bi-stop-circle-fill" },
                { ContactType.Email, "bi-envelope-at-fill" },
                { ContactType.Address, "bi-geo-alt-fill" }
            };

            return iconClassNamesByType[Model.Type];
        }
    }
    #endregion
}