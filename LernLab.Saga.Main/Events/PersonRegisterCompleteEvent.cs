using LernLab.Saga.Main.Data;
using System;

namespace LernLab.Saga.Main
{
    public class PersonRegisterCompleteEvent
    {
        public Guid CorrelationId { get; set; }
        public PersonRegisterCompleteEvent(Person person)
        {
            Person = person;
        }

        public Person Person { get; }
    }
}