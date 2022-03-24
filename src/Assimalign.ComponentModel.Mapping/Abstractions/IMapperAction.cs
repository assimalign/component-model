using System;
using System.Reflection;
using System.Collections.Generic;

namespace Assimalign.ComponentModel.Mapping;

public interface IMapperAction : 
	IEquatable<IMapperAction>,
	IEqualityComparer<IMapperAction>
{
	/// <summary>
	/// 
	/// </summary>
	Type TargetType { get; }
	/// <summary>
	/// 
	/// </summary>
	MemberInfo TargetMember { get; }
	/// <summary>
	/// 
	/// </summary>
	/// <param name="context"></param>
	void Invoke(MapperContext context);
}
