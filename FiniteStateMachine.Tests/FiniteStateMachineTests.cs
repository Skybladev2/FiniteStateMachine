using Microsoft.VisualStudio.TestTools.UnitTesting;
using Skyblade;

namespace FiniteStateMachine.Tests
{
    [TestClass]
    public class FiniteStateMachineTests
    {
        [TestMethod]
        public void InitialState()
        {
            Prototype prototype = new Prototype();
            Alpha alpha = new Alpha();
            Release release = new Release();

            FsmSystem<State> fsm = new FsmSystem<State>();
            fsm.AddTransition(prototype, Transitions.Fix, alpha);
            fsm.AddTransition(alpha, Transitions.Optimize, release);

            fsm.SetInitialState(prototype);

            fsm.PerformTransition(Transitions.Fix);
            fsm.PerformTransition(Transitions.Optimize);

            Assert.AreEqual(fsm.GetCurrentState(), release);
        }

        private abstract class State
        {
            protected abstract string MakeIt();
        }

        private class Prototype : State
        {
            protected override string MakeIt()
            {
                return "Make it";
            }
        }

        private class Alpha : State
        {
            protected override string MakeIt()
            {
                return "Make it right";
            }
        }

        private class Release : State
        {
            protected override string MakeIt()
            {
                return "Make it fast";
            }
        }

        private enum Transitions
        {
            Fix,
            Optimize
        }
    }
}
