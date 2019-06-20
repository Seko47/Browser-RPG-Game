using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public static class StringExtensionsClass
{
    public static string ToUpperFirst(this string text)
    {
        switch (text)
        {
            case null:
                {
                    throw new ArgumentNullException(nameof(text));
                }
            case "":
                {
                    throw new ArgumentException($"{nameof(text)} cannot be empty", nameof(text));
                }
            default:
                {
                    return text.First().ToString().ToUpper() + text.Substring(1);
                }
        }
    }
}
