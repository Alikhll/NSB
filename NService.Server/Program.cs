using NServiceBus;
using System;
using System.Threading.Tasks;

namespace NService.Server
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var endpointConfiguration = await Init();

            await Endpoint.Start(endpointConfiguration);

            Console.ReadLine();
        }

        private static async Task<EndpointConfiguration> Init()
        {
            Console.Title = "Server";

            var endpointConfiguration = new EndpointConfiguration("Sales");

            var transport = endpointConfiguration.UseTransport<SqlServerTransport>();
            transport.ConnectionString(
    "Data Source=.;Initial Catalog=NServiceBus;Integrated Security=True;Max Pool Size=80");

            var recoverability = endpointConfiguration.Recoverability();
            recoverability.Delayed(customizations: delayed =>
                {
                    delayed.NumberOfRetries(0);
                });
            recoverability.Immediate(customizations: delayed =>
            {
                delayed.NumberOfRetries(0);
            });


            endpointConfiguration.EnableCallbacks();
            endpointConfiguration.MakeInstanceUniquelyAddressable("uniqueid");
            endpointConfiguration.EnableInstallers();

            return endpointConfiguration;
        }
    }
}
