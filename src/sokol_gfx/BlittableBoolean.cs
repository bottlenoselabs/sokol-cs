// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

/// <summary>
///     A boolean value type with the same memory layout as a <see cref="byte" /> in both managed and unmanaged
///     code.
/// </summary>
/// <remarks>
///     <para>
///         <see cref="BlittableBoolean" /> is blittable. Blittable types are data types in software applications
///         which have a unique characteristic. Data are often represented in memory differently in managed and
///         unmanaged code in the world of .NET. However, blittable types are defined as having an identical
///         presentation in memory for both environments, and can be directly shared. Understanding the difference
///         between blittable and non-blittable types can aid in using P/Invoke, a technique for interoperability
///         with unmanaged code in .NET applications.
///     </para>
/// </remarks>
public readonly struct BlittableBoolean
{
    private readonly byte _value;

    private BlittableBoolean(bool value)
    {
        _value = Convert.ToByte(value);
    }

    /// <summary>
    ///     Converts the specified <see cref="bool" /> to a <see cref="BlittableBoolean" />.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>A <see cref="BlittableBoolean" />.</returns>
    public static implicit operator BlittableBoolean(bool value)
    {
        return new BlittableBoolean(value);
    }

    /// <summary>
    ///     Converts the specified <see cref="BlittableBoolean" /> to a <see cref="bool" />.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>A <see cref="bool" />.</returns>
    public static implicit operator bool(BlittableBoolean value)
    {
        return Convert.ToBoolean(value._value);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return Convert.ToBoolean(_value).ToString();
    }
}
