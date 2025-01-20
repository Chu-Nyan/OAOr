using System.Collections.Generic;

public static class Utilities
{
    public static Dictionary<StatType, Stat> GenerateStats()
    {
        var dic = new Dictionary<StatType, Stat>();

        for (int i = 0; i <= 4; i++)
        {
            var type = (StatType)i;
            dic.Add(type, new(type));
        }

        return dic;
    }
}