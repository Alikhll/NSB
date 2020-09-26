using NService.Client;
using NServiceBus;
using System;
using System.Threading.Tasks;

namespace NService.Spare
{
    public class DoSomethingHandler: IHandleMessages<FanOutEvent>
    {
        public Task Handle(FanOutEvent message, IMessageHandlerContext context)
        {
            Console.WriteLine("Spare event: " + message.SomeProperty);

            return Task.CompletedTask;
        }
    }
}
