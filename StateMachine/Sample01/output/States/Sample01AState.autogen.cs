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
    public partial class Sample01AState : BaseSample01State
    {
        public Sample01AState()
            : base(Sample01StateTokens.A)
        {
        }

        protected override void OnInitialized()
        {
            PreInitialized();

            base.OnInitialized();
            RegisterActionHandler(Sample01ActionTokens.Initialized, OnSample01InitializedAction);

            PostInitialized();
        }

        partial void PreInitialized();
        partial void PostInitialized();
    }
}
