using Automatonymous;
using System;

namespace LernLab.Automat
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");

            var relationship = new Relationship();
            var relationship2 = new Relationship();
            var relationship3 = new Relationship();
            var person = new Person { Name = "Joe" };
            var relationship4 = new Relationship() {CurrentState = "Friend" };

            var machine = new RelationshipStateMachine();
            machine.RaiseEvent(relationship4, machine.PissOff);

            machine.RaiseEvent(relationship, machine.Hello);

            machine.RaiseEvent(relationship, machine.PissOff);

            machine.RaiseEvent(relationship2, machine.PissOff);


            machine.RaiseEvent(relationship3, machine.Introduce, person);

            Console.WriteLine("Done");

        }
    }
}
