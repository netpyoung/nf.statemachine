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
    public partial class Sample01BState : BaseSample01State
    {
        public Sample01BState()
            : base(Sample01StateTokens.B)
        {
        }

        protected override void OnInitialized()
        {
            PreInitialized();

            base.OnInitialized();
            RegisterActionHandler(Sample01ActionTokens.Action1, OnSample01Action1Action);
            RegisterActionHandler(Sample01ActionTokens.Action2, OnSample01Action2Action);

            PostInitialized();
        }

        partial void PreInitialized();
        partial void PostInitialized();
    }
}
