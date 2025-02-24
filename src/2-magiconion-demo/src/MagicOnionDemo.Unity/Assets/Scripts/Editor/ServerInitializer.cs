using MinimalUtility.Editor;
using UnityEditor;
using UnityEngine.UIElements;

[InitializeOnLoad]
public sealed class ServerInitializer
{
    static ServerInitializer()
    {
        UnityEditorToolbarUtils.AddCenter(false, static () =>
        {
            var button = new Button()
            {
                clickable = new Clickable(static _ =>
                {
                    ServerProcessManager.instance.SwitchServer();
                    EditorApplication.isPlaying = !EditorApplication.isPlaying;
                }),
                text = "サーバー同時起動"
            };
            button.schedule.Execute(() =>
            {
                switch (EditorApplication.isPlaying, ServerProcessManager.instance.running)
                {
                    case (true, true):
                        button.text = "サーバー同時停止";
                        button.SetEnabled(true);
                        break;
                    case (true, false):
                        button.text = "サーバー同時起動";
                        button.SetEnabled(false);
                        break;
                    case (false, true):
                        ServerProcessManager.instance.SwitchServer();
                        goto default;
                    default:
                        button.text = "サーバー同時起動";
                        button.SetEnabled(true);
                        break;
                }
            }).Every(500);
            return button;
        });

        EditorApplication.playModeStateChanged += state =>
        {
            if (state == PlayModeStateChange.ExitingPlayMode && ServerProcessManager.instance.running)
            {
                ServerProcessManager.instance.SwitchServer();
            }
        };

        EditorApplication.quitting += () =>
        {
            if (ServerProcessManager.instance.running)
            {
                ServerProcessManager.instance.SwitchServer();
            }
        };
    }
}