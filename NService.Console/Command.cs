using NServiceBus;

namespace NService.Client
{
    public class FanInCommand : ICommand
    {
        public string SomeProperty { get; set; }
    }

    public class FanOutEvent : IEvent
    {
        public string SomeProperty { get; set; }
    }

    public class ReqResMessage : IMessage
    {
        public string SomeProperty { get; set; }
    }
}
