using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Assimalign.ComponentModel.Validation.Internal.Extensions;


internal static partial class ValidationInternalExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetExpressionBody<T, TValue>(this IValidationItem<T, TValue> item)
    {
        if (item.ItemExpression.Body is MemberExpression member)
        {
            return string.Join('.', member.ToString().Split('.').Skip(1));
        }

        return null;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetExpressionBody<T, TValue>(this IValidationItem item)
    {
        if (item is IValidationItem<T, TValue> validationItem)
        {
            return validationItem.GetExpressionBody();
        }

        return null;

    }
}

