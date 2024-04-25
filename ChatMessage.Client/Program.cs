using System.Text.Json;
using Websocket.Client;

using var websocketClient = new WebsocketClient(new Uri("wss://localhost:7215/chat"));

await websocketClient.Start();

websocketClient.MessageReceived.Subscribe(msg =>
{
    var receivedMessage = JsonSerializer.Deserialize<ChatMessage>(msg!);
    Console.WriteLine($"chatroom: {receivedMessage.Content}");
});

while (true)
{
    var sendMessage = Console.ReadLine();
    var message = new ChatMessage()
    {
        Content = sendMessage!
    };

    var json = JsonSerializer.Serialize<ChatMessage>(message);

    //var message = new ThermostatTemperatureMetric(temperature);
    //var json = JsonSerializer.Serialize<ThermostatMetric>(message);
    websocketClient.Send(json);

    //Console.WriteLine("Q: What is the current temperature? ");
    //Console.Write("A: ");
    //var line = Console.ReadLine();
    //if (float.TryParse(line, out var temperature))
    //{
    //    var message = new ThermostatTemperatureMetric(temperature);
    //    var json = JsonSerializer.Serialize<ThermostatMetric>(message);
    //    websocketClient.Send(json);

    //    Console.ReadLine();
    //}
    //else
    //{
    //    Console.WriteLine("Invalid temperature value");
    //}

}
