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
    public partial class Sample01C1State : BaseSample01State
    {
        public Sample01C1State()
            : base(Sample01StateTokens.C1)
        {
        }

        protected override void OnInitialized()
        {
            PreInitialized();

            base.OnInitialized();
            RegisterActionHandler(Sample01ActionTokens.Continue, OnSample01ContinueAction);
            RegisterActionHandler(Sample01ActionTokens.Error, OnSample01ErrorAction);

            PostInitialized();
        }

        partial void PreInitialized();
        partial void PostInitialized();
    }
}
