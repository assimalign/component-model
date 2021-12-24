using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Mapping;


internal static class MapperUtility
{


    public static void SetPropertyValue<T, TValue>(this T target, Expression<Func<T, TValue>> lambda, TValue value)
    {
        if (lambda.Body is MemberExpression member)
        {
            if (member.Member is PropertyInfo property)
            {
                property.SetValue(target, value);
            }
        }
    }
}

