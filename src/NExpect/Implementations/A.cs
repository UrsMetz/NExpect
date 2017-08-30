﻿using NExpect.Interfaces;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations
{
    internal class A<T> : ExpectationContext<T>, IA<T>
    {
        public object Actual { get; }

        public A(object actual)
        {
            Actual = actual;
        }
    }
}