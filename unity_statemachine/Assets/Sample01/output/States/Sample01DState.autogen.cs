/*
 ###########################################################
 ### Warning: this file has been generated automatically ###
 ###                    DO NOT MODIFY                    ###
 ###########################################################
*/

using System;
using NF.StateMachine;

namespace Sample01.States
{
    public partial class Sample01DState : BaseSample01State
    {
        public Sample01DState()
            : base(Sample01StateTokens.D)
        {
        }

        protected override void OnInitialized()
        {
            PreInitialized();

            base.OnInitialized();
            RegisterActionHandler(Sample01ActionTokens.Error, OnSample01ErrorAction);
            RegisterActionHandler(Sample01ActionTokens.Restart, OnSample01RestartAction);

            PostInitialized();
        }

        partial void PreInitialized();
        partial void PostInitialized();
    }
}
