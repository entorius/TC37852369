using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC37852369.Helpers
{
    public static class DictionaryHelper
    {
       
        internal static bool TryGetTypedValue<TKey, TValue, TActual>(
       this IDictionary<TKey, TValue> data,
       TKey key,
       out TActual value) where TActual : TValue
        {
            TValue tmp;
            if (data.TryGetValue(key, out tmp))
            {
                value = (TActual)tmp;
                return true;
            }
            value = default(TActual);
            return false;
        }
    }
}
