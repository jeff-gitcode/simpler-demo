using SimpleR;
using System.Security.Claims;

public class ChatMessageDispatcher : IWebSocketMessageDispatcher<ChatMessage>
{
    private readonly ILogger<ChatMessageDispatcher> _logger;

    public ChatMessageDispatcher(ILogger<ChatMessageDispatcher> logger)
    {
        _logger = logger;
    }

    public Task OnConnectedAsync(IWebsocketConnectionContext<ChatMessage> connection)
    {
        _logger.LogInformation("Charger with id {ChargerId} connected", connection.User.FindFirstValue(ClaimTypes.NameIdentifier));
        return Task.CompletedTask;
    }

    public Task OnDisconnectedAsync(IWebsocketConnectionContext<ChatMessage> connection, Exception? exception)
    {
        _logger.LogInformation("Charger with id {ChargerId} disconnected", connection.User.FindFirstValue(ClaimTypes.NameIdentifier));
        return Task.CompletedTask;
    }

    public async Task DispatchMessageAsync(IWebsocketConnectionContext<ChatMessage> connection, ChatMessage message)
    {
        _logger.LogInformation("Received message from charger with id {ChargerId}: {Message}", connection.User.FindFirstValue(ClaimTypes.NameIdentifier), message.Content);
        // switch (message)
        // {
        //     //case OcppCall call:
        //     //    // process the call
        //     //    break;
        //case OcppCallResult callResult:
        //    // process the call result
        //    break;
        //case OcppCallError callError:
        // process the call error
        // break;
        // }

        await connection.WriteAsync(message);

        // return Task.CompletedTask;
    }
}