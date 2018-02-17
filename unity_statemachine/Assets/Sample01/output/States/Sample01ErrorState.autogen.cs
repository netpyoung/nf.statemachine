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
    public partial class Sample01ErrorState : BaseSample01State
    {
        public Sample01ErrorState()
            : base(Sample01StateTokens.Error)
        {
        }

        protected override void OnInitialized()
        {
            PreInitialized();

            base.OnInitialized();

            PostInitialized();
        }

        partial void PreInitialized();
        partial void PostInitialized();
    }
}
