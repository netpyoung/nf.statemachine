using System;
using NF.StateMachine;
using UnityEngine;

namespace Sample01.States
{
    partial class Sample01BState
    {
        private void OnSample01Action1Action(object arg1, Action<StateToken> arg2)
        {
            Debug.Log("Action1");
        }

        private void OnSample01Action2Action(object arg1, Action<StateToken> arg2)
        {
            throw new NotImplementedException();
        }
    }
}