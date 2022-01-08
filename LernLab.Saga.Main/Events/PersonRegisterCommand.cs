using LernLab.Saga.Main.Data;
using System;

namespace LernLab.Saga.Main
{
    public class PersonRegisterCommand
    {
        public Guid CorrelationId { get; set; }

        public Person Person { get; set; }
    }
}