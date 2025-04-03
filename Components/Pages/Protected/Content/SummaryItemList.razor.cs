using Microsoft.AspNetCore.Components;
using NATS.Helpers;
using NATS.Models;

namespace NATS.Components.Pages.Protected.Home;

public partial class SummaryItemList : ComponentBase
{
    #region InjectedDepdencies
    private readonly IRouteHelper _routeHelper;
    #endregion
    
    #region Parameters
    [Parameter]
    public List<SummaryItemDetailModel> Model { get; init; }
    
    [Parameter]
    public string Class { get; init; }
    #endregion

    #region Constructor
    public SummaryItemList(IRouteHelper routeHelper)
    {
        _routeHelper = routeHelper;
    }
    #endregion
}