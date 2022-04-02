namespace Assimalign.ComponentModel.Mapping;

/// <summary>
/// 
/// </summary>
public sealed class MapperContext : IMapperContext
{
	private readonly object target;
	private readonly object source;

	public MapperContext(object target, object source)
	{
		this.target = target;
		this.source = source;
	}
	/// <summary>
	/// 
	/// </summary>
	public object Target => this.target;
	/// <summary>
	/// 
	/// </summary>
	public object Source => this.source;
	/// <summary>
	/// 
	/// </summary>
    public MapperOptions MapOptions { get; init; }
}
