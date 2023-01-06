using System;
using System.Collections.Generic;
using System.Linq;

public static class ListHelpers
{
    public static T GetValue<T>(this List<T> list, T value) where T : class
    {
        return list.First(x => x.Equals(value)) ?? null;
    }

    public static T GetValue<T>(this List<T> list, Func<T, bool> operation) where T : class
    {
        return list.First( x =>
        {
            return operation(x);
        }) ?? null;
    }
}
