namespace Assimalign.ComponentModel.Mapping;

/// <summary>
/// 
/// </summary>
public interface IMapperAction 
{
	/// <summary>
	/// An arbitrary number to used for uniqueness. This helps to 
	/// prevent duplicate mappings of the same member within a <see cref="IMapperProfile"/>.
	/// </summary>
	int Id { get; }
	/// <summary>
	/// 
	/// </summary>
	/// <param name="context"></param>
	void Invoke(MapperContext context);
}