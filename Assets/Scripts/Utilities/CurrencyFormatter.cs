using System;
using System.Globalization;

public static class CurrencyFormatter
{
    private static readonly string[] Suffixes = { "", "k", "M", "B", "T" };

    /// <summary>
    /// Converts large numbers into compact tycoon strings (e.g., 10000 -> "10,0k").
    /// </summary>
    public static string FormatValue(double value)
    {
        bool isNegative = value < 0;
        double absoluteValue = Math.Abs(value);

        if (absoluteValue < 10000)
        {
            string rawNumber = Math.Floor(absoluteValue).ToString("0");
            return isNegative ? "-" + rawNumber : rawNumber;
        }

        int suffixIndex = 0;

        while (absoluteValue >= 1000 && suffixIndex < Suffixes.Length - 1)
        {
            suffixIndex++;
            absoluteValue /= 1000;
        }

        string formattedNumber = absoluteValue.ToString("0.0", CultureInfo.InvariantCulture);
        formattedNumber = formattedNumber.Replace('.', ',');

        string finalString = formattedNumber + Suffixes[suffixIndex];
        return isNegative ? "-" + finalString : finalString;
    }

    /// <summary>
    /// Overload to accept standard long integers seamlessly.
    /// </summary>
    public static string FormatValue(long value)
    {
        return FormatValue((double)value);
    }
}