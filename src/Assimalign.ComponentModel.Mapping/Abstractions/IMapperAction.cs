using System;
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
	string TargetItem { get; }
	/// <summary>
	/// 
	/// </summary>
	/// <param name="context"></param>
	void Invoke(MapperContext context);
}
