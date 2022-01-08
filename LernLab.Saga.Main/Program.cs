using MassTransit;
using MassTransit.Saga;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace LernLab.Saga.Main
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Init bus..");
            var machine = new TravelStateMachine();
            var repository = new InMemorySagaRepository<TravelSaga>();

            var busControl = Bus.Factory.CreateUsingRabbitMq(configure =>
            {

                configure.Durable = true;
                configure.PrefetchCount = 1;
                configure.PurgeOnStartup = true;
                configure.Host(new Uri(RabbitMqConstants.RabbitMqUri), hst =>
                {
                    hst.Username(RabbitMqConstants.UserName);
                    hst.Password(RabbitMqConstants.Password);
                });               

                configure.ReceiveEndpoint("test-travel-new-register", e =>
                {
                    e.Consumer<NewRegisterConsumer>();
                });

                configure.ReceiveEndpoint(RabbitMqConstants.SagaQueue, e =>
                {
                    e.StateMachineSaga(machine, repository);
                });

            });
            var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            await busControl.StartAsync(source.Token);

            Console.WriteLine("Start test");
            List<Guid> guids = new List<Guid>();
            try
            {
                while (true)
                {
                    string value = await Task.Run(() =>
                    {
                        Console.WriteLine("Enter message (or quit to exit)");
                        Console.Write("> ");
                        return Console.ReadLine();
                    });

                    if ("quit".Equals(value, StringComparison.OrdinalIgnoreCase))
                        break;

                    if ("register".Equals(value, StringComparison.OrdinalIgnoreCase))
                    {
                        var id = Guid.NewGuid();
                        Console.WriteLine($"Invoke PersonRegisterCommand. Guid: {id}");
                        await busControl.Publish<PersonRegisterCommand>(new PersonRegisterCommand()
                        {
                            CorrelationId = id,
                            Person = new Data.Person
                            {
                                Name = "Ivan",
                                Passport = "123456"
                            }
                           
                        });
                        guids.Add(id);
                    } else if ("stat".Equals(value, StringComparison.OrdinalIgnoreCase))
                    {
                        foreach (var id in guids)
                        {
                            Console.WriteLine($"Info record: {id}");
                            var tSaga  = Newtonsoft.Json.JsonConvert.SerializeObject(repository[id]);
                            Console.WriteLine(tSaga);                             
                        }
                    }
                    else
                    {
                        Console.WriteLine("Command not found - try again..");
                    }
                    
                }
            }
            finally
            {
                await busControl.StopAsync();
            }
        }
    }
}
