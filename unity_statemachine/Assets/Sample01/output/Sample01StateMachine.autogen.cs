/*
 ###########################################################
 ### Warning: this file has been generated automatically ###
 ###                    DO NOT MODIFY                    ###
 ###########################################################
*/

using System;
using NF.StateMachine;
using Sample01.States;

namespace Sample01
{
    partial class Sample01StateMachine : StateManager
    {
        public Sample01StateMachine() : this(null)
        {
        }

        public Sample01StateMachine(object context) : base(context)
        {
            PreHandlersRegistration();

            RegisterState(new Sample01BState());
            RegisterState(new Sample01C1State());
            RegisterState(new Sample01C2State());
            RegisterState(new Sample01DState());
            RegisterState(new Sample01ErrorState());
            RegisterState(new Sample01AState());

            PostHandlersRegistration();

            SetInitialState(Sample01StateTokens.B);
        }

        partial void PreHandlersRegistration();
        partial void PostHandlersRegistration();
    }
}
