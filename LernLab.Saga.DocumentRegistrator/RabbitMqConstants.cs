using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LernLab.Saga.DocumentRegistrator
{
    public static class RabbitMqConstants
    {
        public const string RabbitMqUri = "rabbitmq://localhost/";
        public const string UserName = "guest";
        public const string Password = "guest";
        public const string DocumentRegistratorQueue = "test.travel.registrator.queue";
        public const string SagaQueue = "test.travel.saga";
    }
}
