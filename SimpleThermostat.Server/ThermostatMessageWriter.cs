using SimpleR.Protocol;
using System.Buffers;
using System.Text.Json;

public class ThermostatMessageWriter : IMessageWriter<ThermostatCommand>
{
    public void WriteMessage(ThermostatCommand message, IBufferWriter<byte> output)
    {
        var jsonWriter = new Utf8JsonWriter(output);
        JsonSerializer.Serialize(jsonWriter, message);
    }
}
