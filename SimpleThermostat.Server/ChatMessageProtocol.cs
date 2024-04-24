using SimpleR.Protocol;
using System.Buffers;
using System.Text;

public class ChatMessageProtocol : IMessageProtocol<ChatMessage>
{

    public void WriteMessage(ChatMessage message, IBufferWriter<byte> output)
    {
        var span = output.GetSpan(Encoding.UTF8.GetByteCount(message.Content));

        var bytesWritten = Encoding.UTF8.GetBytes(message.Content, span);

        output.Advance(bytesWritten);
    }

    public bool TryParseMessage(ref ReadOnlySequence<byte> input, out ChatMessage message)
    {
        var reader = new SequenceReader<byte>(input);

        if (reader.TryReadTo(out ReadOnlySequence<byte> payload, delimiter: 0, advancePastDelimiter: true))
        {
            message = new ChatMessage { Content = Encoding.UTF8.GetString(payload) };
            input = reader.UnreadSequence;
            return true;
        }

        message = default;
        return false;
    }
}