using System;

namespace TuneLab_Extensions.GUI;

internal interface IScrollAxis
{
    event Action AxisChanged;
    double ViewLength { get; set; }
    double ViewOffset { get; set; }
    double ContentLength { get; }
}

internal static class IScrollAxisExtension
{
    public static TuneLab.Base.Structures.RangeF ViewRange(this IScrollAxis axis)
    {
        return new TuneLab.Base.Structures.RangeF(axis.ViewOffset, Math.Min(axis.ViewOffset + axis.ViewLength, axis.ContentLength));
    }
}
