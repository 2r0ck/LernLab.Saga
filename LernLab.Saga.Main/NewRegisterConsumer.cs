using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LernLab.Saga.Main
{
    public class NewRegisterConsumer : IConsumer<NewRegisterCommand>
    {
        public async Task Consume(ConsumeContext<NewRegisterCommand> context)
        {            
            var person = context.Message.Data;
            await Task.Delay(1000 * 10);
            if (person != null)
            {
                person.ClientId = context.CorrelationId ?? Guid.NewGuid();
                await context.Publish(new PersonRegisterCompleteEvent(person) {CorrelationId = person.ClientId });
            }             
        }
    }
}
