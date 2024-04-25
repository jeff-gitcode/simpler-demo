using SimpleR.Protocol;
using System.Buffers;
using System.Text.Json;

public class ChatMessageWriteProtocol : IMessageWriter<ChatMessage>
{
    public void WriteMessage(ChatMessage message, IBufferWriter<byte> output)
    {
        var jsonWriter = new Utf8JsonWriter(output);
        JsonSerializer.Serialize(jsonWriter, message);
        //var span = output.GetSpan(Encoding.UTF8.GetByteCount(message.Content));

        //var bytesWritten = Encoding.UTF8.GetBytes(message.Content, span);

        //output.Advance(bytesWritten);
    }
}

public class ChatMessageReadProtocol : IDelimitedMessageReader<ChatMessage>
{
    public ChatMessage ParseMessage(ref ReadOnlySequence<byte> input)
    {
        var jsonReader = new Utf8JsonReader(input);

        return JsonSerializer.Deserialize<ChatMessage>(ref jsonReader)!;
    }
    //public bool TryParseMessage(ref ReadOnlySequence<byte> input, out ChatMessage message)
    //{
    //    var reader = new SequenceReader<byte>(input);

    //    if (reader.TryReadTo(out ReadOnlySequence<byte> payload, delimiter: 0, advancePastDelimiter: true))
    //    {
    //        message = new ChatMessage { Content = Encoding.UTF8.GetString(payload) };
    //        input = reader.UnreadSequence;
    //        return true;
    //    }

    //    message = default;
    //    return false;
    //}
}