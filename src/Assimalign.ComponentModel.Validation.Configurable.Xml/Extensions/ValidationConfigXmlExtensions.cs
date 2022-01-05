using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Configurable;

using Assimalign.ComponentModel.Validation;


public static class ValidationConfigXmlExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="builder"></param>
    /// <param name="xml"></param>
    /// <returns></returns>
    public static IValidationConfigBuilder ConfigureXml<T>(this IValidationConfigBuilder builder, string xml)
        where T : class
    {


        return builder;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="builder"></param>
    /// <param name="xml"></param>
    /// <returns></returns>
    public static IValidationConfigBuilder ConfigureXml<T>(this IValidationConfigBuilder builder, Stream xml)
        where T : class
    {


        return builder;
    }
}
