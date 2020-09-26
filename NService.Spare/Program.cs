using NServiceBus;
using System;
using System.Threading.Tasks;

namespace NService.Spare
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
            Console.Title = "Sales Spare";

            var endpointConfiguration = new EndpointConfiguration("Spare");

            var transport = endpointConfiguration.UseTransport<SqlServerTransport>();
            transport.ConnectionString(
    "Data Source=.;Initial Catalog=NServiceBus;Integrated Security=True;Max Pool Size=80");

            endpointConfiguration.EnableInstallers();

            return endpointConfiguration;
        }
    }
}
