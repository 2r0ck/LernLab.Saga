using LernLab.Saga.Main;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LernLab.Saga.DocumentRegistrator
{
    public class DocumentRegisterCommandConsumer : IConsumer<DocumentRegisterCommand>
    {
        public async Task Consume(ConsumeContext<DocumentRegisterCommand> context)
        {
            Console.WriteLine("DocumentRegisterCommandConsumer start.. ");
            await Task.Delay(1000 * 7);
            Console.WriteLine("DocumentRegisterCommandConsumer process.. ");
            var person = context.Message.Person;
            await context.Publish(new DocumentRegisterCompleteEvent
            {
                CorrelationId = context.CorrelationId ?? context.Message.CorrelationId,
                Person = person,
                TravelDocument = new Main.Data.TravelDocument()
                {
                    CarNumber = $"CarNumber{person.Passport}",
                    FlightNumber = $"FlightNumber{person.Passport}",
                    HotelRoom = $"HotelRoom{person.Passport}",
                }
            });
            Console.WriteLine("DocumentRegisterCommandConsumer done ");
        }
    }
}
