using Automatonymous;
using LernLab.Saga.Main.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LernLab.Saga.Main
{
    public class TravelStateMachine : MassTransitStateMachine<TravelSaga>
    {
        public Event<PersonRegisterCommand> PersonRegistringEvent { get; private set; } //происходят на оркестраторе 
        public Event<PersonRegisterCompleteEvent> PersonRegisterCompleteEvent { get; private set; }
        public Event<DocumentRegisterCommand> DocumentRegistingEvent { get; private set; } //происходят в стороннем сервисе
        public Event<DocumentRegisterCompleteEvent> DocumentRegisterCompleteEvent { get; private set; }


         
        public State PersonWaitRegister { get; set; }
        public State PersonRegistred { get; set; }
        public State DocumentWaitRegister { get; set; }
        public State DocumentRegistred { get; set; }
    
        public TravelStateMachine()
        {
            InstanceState(c => c.CurrentState);
            Event(() => PersonRegistringEvent, x => x.CorrelateById(context => context.Message.CorrelationId));
            Event(() => PersonRegisterCompleteEvent, x => x.CorrelateById(context => context.Message.CorrelationId));
            Event(() => DocumentRegistingEvent, x => x.CorrelateById(context => context.Message.CorrelationId));
            Event(() => DocumentRegisterCompleteEvent, x => x.CorrelateById(context => context.Message.CorrelationId));

            Initially(
                When(PersonRegistringEvent)
                .Then(x =>
                {
                    x.Instance.ApplyData(x.Data.Person);
                })
                .Publish(ctx => ctx.Instance.CreateNewRegisterCommand())
                .TransitionTo(PersonWaitRegister));

            During(PersonWaitRegister,
                 When(PersonRegisterCompleteEvent)
                  .Then(x => x.Instance.ApplyData(x.Data.Person))
                  .TransitionTo(PersonRegistred)
                  .Publish(contextCallback => contextCallback.Instance.CreateDocumentRegisterCommand())
                  .TransitionTo(DocumentWaitRegister));

            During(DocumentWaitRegister,
                When(DocumentRegisterCompleteEvent)
                .Then(x => x.Instance.ApplyData(x.Data.TravelDocument))
                .TransitionTo(DocumentRegistred)
                .Then(x =>
                {
                    Console.WriteLine($"Register person [{x.Instance.CorrelationId}] compleate");
                })
                .Finalize());
        }
    }
}
