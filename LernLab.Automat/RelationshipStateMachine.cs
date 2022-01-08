using Automatonymous;

namespace LernLab.Automat
{
    public class RelationshipStateMachine : AutomatonymousStateMachine<Relationship>
    {
        public RelationshipStateMachine()
        {
            // Explicit definition of the events.
            Event(() => Hello);
            Event(() => PissOff);
            Event(() => Introduce);

            // Explicit definition of the states.
            State(() => Friend);
            State(() => Enemy);

            InstanceState(c => c.CurrentState);

            Initially(
                When(Hello)
                    .TransitionTo(Friend),
                When(PissOff)
                    .TransitionTo(Enemy),
                When(Introduce)
                    .Then(ctx => ctx.Instance.Name = ctx.Data.Name)
                    .TransitionTo(Friend)
            );

            During(Friend, When(PissOff)
                              .Then(ctx => ctx.Instance.Name = "Looser")
                              .TransitionTo(Enemy));
            
        }

        public State Friend { get; private set; }
        public State Enemy { get; private set; }

        public Event Hello { get; private set; }
        public Event PissOff { get; private set; }
        public Event<Person> Introduce { get; private set; }
    }
}
