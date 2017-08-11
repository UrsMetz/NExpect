using System;
using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal class ThrowContinuation : ExpectationContext<Exception>, IThrowContinuation
    {
        public Exception Exception { get; set; }

        public IWithAfterThrowContinuation With => 
            Factory.Create<Exception, WithAfterThrowContinuation>(Exception, this);
    }
}