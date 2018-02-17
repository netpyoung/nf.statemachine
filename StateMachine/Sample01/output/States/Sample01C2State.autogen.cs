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
    public partial class Sample01C2State : BaseSample01State
    {
        public Sample01C2State()
            : base(Sample01StateTokens.C2)
        {
        }

        protected override void OnInitialized()
        {
            PreInitialized();

            base.OnInitialized();
            RegisterActionHandler(Sample01ActionTokens.Continue, OnSample01ContinueAction);

            PostInitialized();
        }

        partial void PreInitialized();
        partial void PostInitialized();
    }
}
