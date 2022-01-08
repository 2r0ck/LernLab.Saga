using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LernLab.Saga.Main.Data
{
    public class Person
    {
        public string Name { get; set; }
        public string Passport { get; set; }

        public Guid ClientId { get; set; }
    }
}
