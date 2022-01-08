using Automatonymous;
using LernLab.Saga.Main.Data;
using System;

namespace LernLab.Saga.Main
{
    public class TravelSaga : SagaStateMachineInstance
    {
        public string CurrentState { get; set; }
        public Person Person { get; set; }
        public TravelDocument TravelDocument { get; set; }
        public Guid CorrelationId { get; set; }

        public NewRegisterCommand CreateNewRegisterCommand()
        {
            return new NewRegisterCommand(Person)
            {
                CorrelationId = CorrelationId
            };
        }

        public DocumentRegisterCommand CreateDocumentRegisterCommand()
        {
            return new DocumentRegisterCommand()
            {
                CorrelationId = CorrelationId,
                Person = Person
            };
        }

        public void ApplyData(Person person)
        {
            Person = person;
        }
        public void ApplyData(TravelDocument doc)
        {
            TravelDocument = doc;
        }
    }
}
