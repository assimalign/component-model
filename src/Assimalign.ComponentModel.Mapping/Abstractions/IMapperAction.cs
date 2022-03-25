using System;
using System.Reflection;
using System.Collections.Generic;

namespace Assimalign.ComponentModel.Mapping;

/// <summary>
/// 
/// </summary>
public interface IMapperAction 
{
	/// <summary>
	/// 
	/// </summary>
	/// <param name="context"></param>
	void Invoke(MapperContext context);
}

///// <summary>
///// Mapper action represents a
///// </summary>
//public interface IMapperAction<TSource, TTarget> : IMapperAction
//{
//	/// <summary>
//	/// 
//	/// </summary>
//	Type TargetType { get; }
//	/// <summary>
//	/// 
//	/// </summary>
//	MemberInfo TargetMember { get; }
//	/// <summary>
//	/// 
//	/// </summary>
//	/// <param name="context"></param>
//	void Invoke(MapperContext context);
//}
