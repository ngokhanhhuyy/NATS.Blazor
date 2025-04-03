using Microsoft.AspNetCore.Components;

namespace NATS.Components.Layout.Protected;

public partial class MainBlock : ComponentBase
{
    #region Parameters
    [Parameter, EditorRequired]
    public string Title { get; set; }
    
    [Parameter, CanBeNull]
    public Padding HeaderPadding { get; set; }
    
    [Parameter]
    public string HeaderClass { get; set; }
    
    [Parameter]
    public RenderFragment HeaderContent { get; set; }
    
    [Parameter, CanBeNull]
    public Padding BodyPadding { get; set; }
    
    [Parameter]
    public string BodyClass { get; set; }
    
    [Parameter]
    public RenderFragment ChildContent { get; set; }
    #endregion
    
    #region Computed
    private string HeaderComputedClass
    {
        get
        {
            string className = HeaderClass ?? string.Empty;
            if (HeaderPadding != null)
            {
                className += " " + string.Join(
                    " ",
                    $"pt-{HeaderPadding.Top}",
                    $"pb-{HeaderPadding.Bottom}",
                    $"ps-{HeaderPadding.Left}",
                    $"pe-{HeaderPadding.Right}");
            }

            return className;
        }
    }
    
    private string BodyComputedClass
    {
        get
        {
            string className = BodyClass ?? string.Empty;
            if (BodyPadding != null)
            {
                className += " " + string.Join(
                    " ",
                    $"pt-{BodyPadding.Top}",
                    $"pb-{BodyPadding.Bottom}",
                    $"ps-{BodyPadding.Left}",
                    $"pe-{BodyPadding.Right}");
            }

            return className;
        }
    }
    #endregion

    public MainBlock()
    {
        HeaderPadding ??= new Padding
        {
            Top = 2,
            Right = 2,
            Bottom = 2,
            Left = 3
        };

        BodyPadding ??= new Padding
        {
            Top = 3,
            Right = 3,
            Bottom = 3,
            Left = 3
        };
    }
    
    #region ParameterStructures
    public class Padding
    {
        public int Top { get; init; }
        public int Right { get; init; }
        public int Bottom { get; init; }
        public int Left { get; init; }
    }
    #endregion
}