/*
 ###########################################################
 ### Warning: this file has been generated automatically ###
 ###                    DO NOT MODIFY                    ###
 ###########################################################
*/

using System;
using NF.StateMachine;

namespace Sample01
{
    public static class Sample01ActionTokens
    {
        public static readonly ActionToken Initialized = new ActionToken(nameof(Initialized));
        public static readonly ActionToken Action1 = new ActionToken(nameof(Action1));
        public static readonly ActionToken Action2 = new ActionToken(nameof(Action2));
        public static readonly ActionToken Continue = new ActionToken(nameof(Continue));
        public static readonly ActionToken Error = new ActionToken(nameof(Error));
        public static readonly ActionToken Restart = new ActionToken(nameof(Restart));

        public static readonly ActionToken[] Items = 
        {
            Initialized,
            Action1,
            Action2,
            Continue,
            Error,
            Restart,
        };
    }
}
