using MinimalUtility.Debugging;
using UnityEngine.UIElements;

public class DebugView : DebugViewerBase
{
    public static readonly DebugView instance = new DebugView();

    public string message { get; set; } = "this is:";
    
    private DebugView()
    {
        Start();
    }
    
    public override VisualElement Start()
    {
        var root = base.Start();
        var label = new Label("DebugView")
        {
            text = message
        };
        label.schedule.Execute(() =>
        {
            label.text = message;
        }).Every(500);
        root.Add(label);
        return root;
    }
}