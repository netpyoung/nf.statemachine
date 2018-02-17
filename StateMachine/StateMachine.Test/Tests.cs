using System;
using NF.StateMachine;
using Xunit;
using Sample01;
using Sample01.States;

namespace StateMachine.Test
{
    public class Tests
    {
        [Fact]
        public void Test1()
        {
            StateManager sm = new StateManager();
            sm.StateChanged += OnStateChanged;
            sm.Completed += OnStateCompleted;
            sm.RegisterState(new Sample01AState());
            sm.RegisterState(new Sample01BState());
            sm.RegisterState(new Sample01C1State());
            sm.RegisterState(new Sample01C2State());
            sm.RegisterState(new Sample01DState());
            sm.SetInitialState(Sample01StateTokens.A, null);
            sm.PerformAction(Sample01ActionTokens.Error, 51);
            Assert.True(true);

        }

        private void OnStateCompleted(object sender, EventArgs e)
        {
            Console.WriteLine("done");
        }

        private void OnStateChanged(object sender, StateChangedEventArgs e)
        {
            Console.WriteLine(e);
        }
    }
}