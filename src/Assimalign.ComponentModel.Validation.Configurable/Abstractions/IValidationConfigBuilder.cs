using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Configurable;

/// <summary>
/// 
/// </summary>
public interface IValidationConfigBuilder
{

    /// <summary>
    /// 
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    IValidationConfigBuilder Register(IValidationConfigProvider provider);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IValidationConfigProfileCompiler<T> Build<T>();
}

