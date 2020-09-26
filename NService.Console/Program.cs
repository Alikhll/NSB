using NServiceBus;
using System;
using System.Threading.Tasks;

namespace NService.Client
{
    //localhost:9090
    class Program
    {
        static async Task Main(string[] args)
        {
            var endpointInstance = await Init();

            while (true)
            {
                Console.WriteLine("Your message: ");
                string s = Console.ReadLine();

                await endpointInstance.Send(new FanInCommand { SomeProperty = s });

                var resp = await endpointInstance.Request<int>(new ReqResMessage { SomeProperty = s });
            }
        }

        private static async Task<IEndpointInstance> Init()
        {
            Console.Title = "ClientUI";

            var endpointConfiguration = new EndpointConfiguration("ClientUI");

            var transport = endpointConfiguration.UseTransport<SqlServerTransport>();
            transport.ConnectionString(CONNECTION_STRING);

            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(FanInCommand), "Sales");
            routing.RouteToEndpoint(typeof(ReqResMessage), "Sales");

            endpointConfiguration.EnableCallbacks();
            endpointConfiguration.MakeInstanceUniquelyAddressable("uniqueid");
            endpointConfiguration.EnableInstallers();

            var endpointInstance = await Endpoint.Start(endpointConfiguration);

            return endpointInstance;
        }

        private const string CONNECTION_STRING = "Data Source=.;Initial Catalog=NServiceBus;Integrated Security=True;";
        //private const string CONNECTION_STRING = "Data Source=host.docker.internal;Initial Catalog=NServiceBus;User Id=Sa;Password=ali;";

        //https://github.com/Particular/NServiceBus.Persistence.Sql/issues/193
    }
}
