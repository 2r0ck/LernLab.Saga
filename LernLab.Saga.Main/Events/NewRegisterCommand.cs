using LernLab.Saga.Main.Data;
using System;

namespace LernLab.Saga.Main
{
    public class NewRegisterCommand  
    {
        public Guid CorrelationId { get; set; }

        public NewRegisterCommand(Data.Person data)
        {
            Data = data;
        }

        public Person Data { get; }
    }
}