using MagicOnionDemo.Shared;
using UnityEngine;

public class GreeterHubReceiver : IGreeterHubReceiver
{
    void IGreeterHubReceiver.OnMessageReceived(string message)
    {
        DebugView.instance.message += @$"{message}
";
    }
}