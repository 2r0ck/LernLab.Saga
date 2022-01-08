using LernLab.Saga.Main.Data;
using System;

namespace LernLab.Saga.Main
{
    public class DocumentRegisterCompleteEvent
    {
        public Person Person { get; set; }
        public Guid CorrelationId { get; set; }

        public TravelDocument TravelDocument { get; set; }
    }
}