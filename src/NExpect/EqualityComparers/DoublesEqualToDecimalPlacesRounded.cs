using System;
using System.Collections.Generic;

namespace NExpect.EqualityComparers;

/// <summary>
/// Compares two decimals to a specified number of decimal places,
/// truncated
/// </summary>
public class DoublesEqualToDecimalPlacesRounded : IEqualityComparer<double>
{
    private readonly int _decimalPlaces;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="decimalPlaces"></param>
    public DoublesEqualToDecimalPlacesRounded(int decimalPlaces)
    {
        _decimalPlaces = decimalPlaces;
    }

    private double Round(double value)
    {
        return Math.Round(value, _decimalPlaces);
    }

    /// <summary>
    /// Tests equality up to the provided number of decimal places
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public bool Equals(double x,
        double y)
    {
        return Math.Abs(Round(x) - Round(y)) < Double.Epsilon;
    }

    /// <summary>
    /// Gets the hashcode of the decimal value to two decimal places,
    /// truncated
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public int GetHashCode(double value)
    {
        return Round(value).GetHashCode();
    }
}