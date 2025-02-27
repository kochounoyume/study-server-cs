using MagicOnion.Server.Hubs;
using MagicOnionDemo.Shared;

namespace MagicOnionDemo.Server;

public class GreeterHub : StreamingHubBase<IGreeterHub, IGreeterHubReceiver>, IGreeterHub
{
    public ValueTask<string> HelloAsync(string name)
    {
        Console.WriteLine($"Hello {name}!");
        Task.Run(async () =>
        {
            await Task.Delay(1000);
            Client.OnMessageReceived("遅れてコンニチハ" + name);
        });
        return ValueTask.FromResult("Hello " + name);
    }
}