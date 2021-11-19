using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CashConverter
{
    private static string[] suffix = new string[] 
    {"", "K", "M", "B", "T", 
    "aa", "ab", "ac", "ae", "af", "ag", "ah", "ai", "aj", "ak", "al", "am", "ao", "ap", "aq", "ar", "as", "at", "au", "av", "aw", "ax", "ay", "az",
    "ba", "bb", "bc", "be", "bf", "bg", "bh", "bi", "bj", "bk", "bl", "bm", "bo", "bp", "bq", "br"};
    //br = 10^144

    public static string doubleToString(double currentValue)
    {
        int scale = 0;
        double valueTemp = currentValue;

        while (valueTemp >= 1000d)
        {
            valueTemp /= 1000d;
            scale++;
            if (scale >= suffix.Length)
                return currentValue.ToString("Over limit");
        }
        return valueTemp.ToString("0.##") + suffix[scale];
    }
}
