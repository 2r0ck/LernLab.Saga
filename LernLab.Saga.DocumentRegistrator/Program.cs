using MassTransit;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LernLab.Saga.DocumentRegistrator
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(configure =>
            {

                configure.Durable = true;
                configure.PrefetchCount = 1;
                configure.PurgeOnStartup = true;
                configure.Host(new Uri(RabbitMqConstants.RabbitMqUri), hst =>
                {
                    hst.Username(RabbitMqConstants.UserName);
                    hst.Password(RabbitMqConstants.Password);
                });

                configure.ReceiveEndpoint(RabbitMqConstants.DocumentRegistratorQueue, e =>
                {
                    e.Consumer<DocumentRegisterCommandConsumer>();
                });
            });

            await bus.StartAsync(CancellationToken.None);
            Console.WriteLine("Listening for report requests.. Press enter to exit");
            Console.ReadLine();
            await bus.StopAsync(CancellationToken.None);
        }
    }
}
