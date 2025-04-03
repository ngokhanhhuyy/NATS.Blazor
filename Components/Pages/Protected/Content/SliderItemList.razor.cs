using Microsoft.AspNetCore.Components;
using NATS.Helpers;
using NATS.Models;

namespace NATS.Components.Pages.Protected.Home;

public partial class SliderItemList : ComponentBase
{
    #region InjectedDependencies
    private readonly IRouteHelper _routeHelper;
    #endregion
    
    #region Parameters
    [Parameter]
    public List<SliderItemDetailModel> Model { get; init; }
    
    [Parameter]
    public string Class { get; init; }
    #endregion

    public SliderItemList(IRouteHelper routeHelper)
    {
        _routeHelper = routeHelper;
    }
}