using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation;


/// <summary>
/// 
/// </summary>
public interface IValidatorFactory
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="validatorName"></param>
    /// <returns></returns>
    IValidator Create(string validatorName);
}

