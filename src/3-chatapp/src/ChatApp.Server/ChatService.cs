using ChatApp.Shared.Services;
using MagicOnion;
using MagicOnion.Server;

namespace ChatApp.Server;

public class ChatService(ILogger<ChatService> logger) : ServiceBase<IChatService>, IChatService
{
    public UnaryResult GenerateException(string message)
    {
        throw new System.NotImplementedException();
    }

    public UnaryResult SendReportAsync(string message)
    {
        logger.LogDebug($"{message}");

        return UnaryResult.CompletedResult;
    }
}