using System.Threading.Tasks;
using MagicOnion;

namespace MagicOnionDemo.Shared
{
    // server
    public interface IGreeterHub : IStreamingHub<IGreeterHub, IGreeterHubReceiver>
    {
        ValueTask<string> HelloAsync(string name);
    }
}