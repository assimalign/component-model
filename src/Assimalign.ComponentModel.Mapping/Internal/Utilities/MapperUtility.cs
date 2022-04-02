using System;
using System.Linq;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Mapping;

using Assimalign.ComponentModel.Mapping.Internal;

internal static class MapperUtility
{
    public static MemberExpression GetMemberExpression(this ParameterExpression parameter, string memberName)
    {
        String[] paths = memberName.Split('.');
        Expression expression = parameter;

        for (int i = 0; i < paths.Length; i++)
        {
            expression = Expression.Property(expression, paths[i]);
        }

        return expression as MemberExpression;
    }
}

