using System;
using NF.StateMachine;

namespace Sample01.States
{
    partial class Sample01DState
    {
        private void OnSample01ErrorAction(object arg1, Action<StateToken> arg2)
        {
            throw new NotImplementedException();
        }

        private void OnSample01RestartAction(object arg1, Action<StateToken> arg2)
        {
            throw new NotImplementedException();
        }
    }
}