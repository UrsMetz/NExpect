﻿using System;
using System.Collections.Generic;

namespace NExpect.EqualityComparers
{
    /// <summary>
    /// Compares two decimals to a specified number of decimal places,
    /// truncated
    /// </summary>
    public class DecimalsEqualToDecimalPlacesTruncated
        : IEqualityComparer<decimal>
    {
        private readonly int _decimalPlaces;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="decimalPlaces"></param>
        public DecimalsEqualToDecimalPlacesTruncated(int decimalPlaces)
        {
            _decimalPlaces = decimalPlaces;
        }

        private decimal Truncate(decimal value)
        {
            var mul = (decimal) Math.Pow(10, _decimalPlaces);
            return Math.Truncate(value * mul) / mul;
        }

        /// <summary>
        /// Tests equality up to the provided number of decimal places
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Equals(decimal x,
            decimal y)
        {
            return Truncate(x) == Truncate(y);
        }

        /// <summary>
        /// Gets the hashcode of the decimal value to two decimal places,
        /// truncated
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int GetHashCode(decimal value)
        {
            return Truncate(value)
                .GetHashCode();
        }
    }
}