using System;
using System.Diagnostics.CodeAnalysis;

namespace LegacyApp.Services;

[ExcludeFromCodeCoverage]
public class DateTimeProvider : IDateTimeProvider
{
    public DateTime GetToday()
    {
        return DateTime.Now;
    }
}

public interface IDateTimeProvider
{
    DateTime GetToday();
}