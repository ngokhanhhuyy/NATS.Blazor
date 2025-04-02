using Microsoft.AspNetCore.Components;
using NATS.Helpers;

namespace NATS.Components.Layout.Public;

public partial class NavigationBarItem : ComponentBase
{
    #region DependencyInjection
    [Inject]
    public IRouteHelper RouteHelper { get; set; }
    
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    #endregion
    
    #region Parameters
    [Parameter, EditorRequired]
    public string Href { get; set; }
    
    [Parameter, EditorRequired]
    public RenderFragment ChildContent { get; set; }
    #endregion
    
    #region Computed
    private string ClassName
    {
        get
        {
            string currentPathName = new Uri(NavigationManager.Uri).AbsolutePath;
            Console.WriteLine(currentPathName == "/" && Href == "/");
            if (currentPathName == "/" && Href == "/")
            {
                return "active";
            }

            if (currentPathName.StartsWith(Href))
            {
                return "active";
            }

            return string.Empty;
        }
    }
    #endregion

    #region LifeCycleEvents
    protected override void OnInitialized()
    {
        Console.WriteLine(new Uri(NavigationManager.Uri).AbsolutePath);
        Console.WriteLine(Href);
        base.OnInitialized();
    }
    #endregion
}