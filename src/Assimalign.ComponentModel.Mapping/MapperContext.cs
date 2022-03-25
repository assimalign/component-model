namespace Assimalign.ComponentModel.Mapping;

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
