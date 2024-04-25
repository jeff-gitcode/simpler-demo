using SimpleR.Protocol;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSimpleR();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapSimpleR<ThermostatMetric, ThermostatCommand>("thermostat/{deviceId}/socket", b =>
{
    b.UseDispatcher<ThermostatMessageDispatcher>()
        .UseEndOfMessageDelimitedProtocol(
            MessageProtocol.From(new ThermostatMessageReader(), new ThermostatMessageWriter()));
});

app.MapSimpleR<ChatMessage>("/chat",
    b =>
    {
        b.UseDispatcher<ChatMessageDispatcher>()
        .UseEndOfMessageDelimitedProtocol(
            MessageProtocol.From(new ChatMessageReadProtocol(), new ChatMessageWriteProtocol()));
    }
);

app.Run();
