using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Configurable;

/// <summary>
/// Compiles a profile from a given configuration.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IValidationConfigProfileCompiler<T>
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IValidationProfile<T> Compile();
}