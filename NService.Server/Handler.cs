using NService.Client;
using NServiceBus;
using System;
using System.Threading.Tasks;

namespace NService.Server
{
    public class DoSomethingHandler : IHandleMessages<FanInCommand>, IHandleMessages<FanOutEvent>, IHandleMessages<ReqResMessage>
    {
        public async Task Handle(FanInCommand message, IMessageHandlerContext context)
        {
            //throw new Exception("Boom");

            Console.WriteLine("FanIn: " + message.SomeProperty);

            await context.Publish(new FanOutEvent { SomeProperty = message.SomeProperty });
        }

        public Task Handle(FanOutEvent message, IMessageHandlerContext context)
        {
            Console.WriteLine("FanOut: " + message.SomeProperty);

            return Task.CompletedTask;
        }

        public async Task Handle(ReqResMessage message, IMessageHandlerContext context)
        {
            Console.WriteLine("Req/Response: " + message.SomeProperty );

            await Task.Delay(1000);

            await context.Reply(20);
        }
    }
}
