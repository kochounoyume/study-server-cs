namespace MagicOnionDemo.Shared
{
    // client
    public interface IGreeterHubReceiver
    {
        void OnMessageReceived(string message);
    }
}