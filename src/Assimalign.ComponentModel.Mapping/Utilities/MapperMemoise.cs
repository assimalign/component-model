using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Mapping;


internal static class MapperMemoise<TIn, TOut>
{
    private static IDictionary<TIn, TOut> cache;

    static MapperMemoise()
    {
        cache ??= new Dictionary<TIn, TOut>();
    }

    /// <summary>
    /// This invocation the invocation of delegates
    /// </summary>
    /// <param name="method"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Func<TIn, TOut> Invoke(Func<TIn, TOut> method)
    {
        return input => cache.TryGetValue(input, out var results) ?
            results :
            cache[input] = method(input);
    }
}

