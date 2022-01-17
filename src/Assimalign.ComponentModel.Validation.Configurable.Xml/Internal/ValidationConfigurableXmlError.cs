using System;
using System.Xml;
using System.Xml.Serialization;

namespace Assimalign.ComponentModel.Validation.Configurable;

public sealed class ValidationConfigurableXmlError : IValidationError
{
    /// <summary>
    /// 
    /// </summary>
    [XmlElement("$code")]
    public string Code { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [XmlElement("$message")]
    public string Message { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [XmlElement("$source")]
    public string Source { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"Error {Code}: {Message} {Environment.NewLine} └─> Source: {Source}";
    }
}
