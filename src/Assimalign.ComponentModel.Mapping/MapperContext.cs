using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Mapping;

using Assimalign.ComponentModel.Mapping;

/// <summary>
/// 
/// </summary>
public sealed class MapperContext 
{
	private readonly object source;
	private readonly object target;

	public MapperContext(object source, object target)
	{
		this.source = source;
		this.target = target;
	}

	/// <summary>
	/// 
	/// </summary>
	public object Source => this.source;
	/// <summary>
	/// 
	/// </summary>
	public object Target => this.target;
}
