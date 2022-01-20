using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Configurable.Internal.Extensions;


internal static class ValidationConfigurableJsonExtensions
{

	public static Type UnwrapNullableType(this Type type)
    {
		var arguments = type.GenericTypeArguments;

		// Since Nullable only takes one type parameter the length should be equal
		// to exactly one
		if (arguments.Any() && arguments.Length == 1)
		{
			if ((arguments[0].IsValueType || arguments[0].IsEnum) && type == typeof(Nullable<>).MakeGenericType(arguments[0]))
			{
				return arguments[0];
			}
		}

		return type;
	}

	public static Type GetEnumerableType(this Type type)
	{
		if (type.IsGenericType)
		{
			var arguments = type.GetGenericArguments();
			if (type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
			{
				return arguments[0];
			}
			else
			{
				var interfaces = type.GetInterfaces();

				foreach (var argument in arguments)
				{
					var enumerableType = typeof(IEnumerable<>).MakeGenericType(argument);

					if (interfaces.Contains(enumerableType))
					{
						return argument;
					}
				}
			}
		}

		throw new Exception("");
		//else
		//{
		//	var hasIntefaces = type.GetInterfaces().Length > 0;

		//	if (hasIntefaces)
		//	{
		//		var other = type.FindInterfaces((filter, criteria) => IsEnumerableType(filter), null).First();

		//		if (null != other)
		//		{
		//			implementation = other.GetGenericArguments().First();
		//			return true;
		//		}
		//	}
		//}

		//return false;
	}

}
