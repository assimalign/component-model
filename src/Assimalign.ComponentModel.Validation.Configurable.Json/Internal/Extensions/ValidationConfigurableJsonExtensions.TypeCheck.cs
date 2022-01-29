using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

	/// <summary>
	/// Certain types in the .NET such as strings aren't considered value types 
	/// since they are mutable. Need to considered types such as these to be value types
	/// as if will cut down on the type checking for the expression building.
	/// </summary>
	/// <param name="type"></param>
	/// <param name="checkNullable"></param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool IsSystemValueType(this Type type, bool checkNullable = true)
	{
		// Will use this array of types to check for nullable value and enum types
		var valueTypes = new Type[]
		{
				typeof(short),
				typeof(int),
				typeof(long),
				typeof(double),
				typeof(decimal),
				typeof(float),
				typeof(ushort),
				typeof(uint),
				typeof(ulong),
				typeof(char),
				typeof(byte),
				typeof(sbyte),
				typeof(bool),
				typeof(Guid),
				typeof(DateTime),
				typeof(DateTimeOffset),
				typeof(TimeSpan),
				typeof(nint),
				typeof(nuint),
				typeof(Half),
#if NET6_0_OR_GREATER
				typeof(DateOnly),
				typeof(TimeOnly)
#endif
		};

		if (type == typeof(string))
		{
			return true;
		}
		// Let's ensure that the type is not wrapped in the Nullable<> type class
		if (checkNullable)
		{
			foreach (var valueType in valueTypes)
			{
				if (type == valueType)
				{
					return true;
				}
				if (type == typeof(Nullable<>).MakeGenericType(valueType))
				{
					return true;
				}
			}
		}
		else
		{
			foreach (var valueType in valueTypes)
			{
				if (type == valueType)
				{
					return true;
				}
			}
		}

		return false;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool IsSystemValueType(this Type type, out Type implementationType, bool checkNullable = true)
	{
		// Will use this array of types to check for nullable value and enum types
		var valueTypes = new Type[]
		{
				typeof(short),
				typeof(int),
				typeof(long),
				typeof(double),
				typeof(decimal),
				typeof(float),
				typeof(ushort),
				typeof(uint),
				typeof(ulong),
				typeof(char),
				typeof(byte),
				typeof(sbyte),
				typeof(bool),
				typeof(Guid),
				typeof(DateTime),
				typeof(DateTimeOffset),
				typeof(TimeSpan),
				typeof(nint),
				typeof(nuint),
				typeof(Half),
#if NET6_0_OR_GREATER
				typeof(DateOnly),
				typeof(TimeOnly)
#endif
		};

		if (type == typeof(string))
		{
			implementationType = typeof(string);
			return true;
		}
		// Let's ensure that the type is not wrapped in the Nullable<> type class
		if (checkNullable)
		{
			foreach (var valueType in valueTypes)
			{
				if (type == valueType)
				{
					implementationType = valueType;
					return true;
				}
				if (type == typeof(Nullable<>).MakeGenericType(valueType))
				{
					implementationType = valueType;
					return true;
				}
			}
		}
		else
		{
			foreach (var valueType in valueTypes)
			{
				if (type == valueType)
				{
					implementationType = valueType;
					return true;
				}
			}
		}
		implementationType = null;

		return false;
	}


	/// <summary>
	/// 
	/// </summary>
	/// <param name="type"></param>
	/// <param name="implementation"></param>
	/// <returns></returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool IsNullableType(this Type type, out Type implementation)
	{
		implementation = null;
		var arguments = type.GetGenericArguments();

		// Since Nullable only takes one type parameter the length should be equal
		// to exactly one
		if (arguments.Any() && arguments.Length == 1)
		{
			if (type == typeof(Nullable<>).MakeGenericType(arguments[0]))
			{
				implementation = arguments[0];
				return true;
			}
		}

		return false;
	}

}
