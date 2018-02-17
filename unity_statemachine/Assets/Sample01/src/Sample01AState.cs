using System;
using NF.StateMachine;

namespace Sample01.States
{
    partial class Sample01AState
    {
        private void OnSample01InitializedAction(object arg1, Action<StateToken> arg2)
        {
            arg2(Sample01StateTokens.B);
        }
    }
}