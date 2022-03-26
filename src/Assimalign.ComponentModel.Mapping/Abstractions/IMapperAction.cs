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