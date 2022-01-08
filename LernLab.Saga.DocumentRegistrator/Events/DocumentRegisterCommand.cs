using LernLab.Saga.Main.Data;
using System;

namespace LernLab.Saga.Main
{
    public class DocumentRegisterCommand
    {
        public Person Person { get; set; }
        public Guid CorrelationId { get; set; }
    }
}