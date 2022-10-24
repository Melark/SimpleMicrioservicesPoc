namespace MessageBus
{
    public interface IMessageBus
    {
        public void SendMessage<T>(T message);
    }
}