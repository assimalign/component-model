﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Configurable;

/// <summary>
/// Configuration 
/// </summary>
public interface IValidationConfigProvider
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IValidationProfile Compile();
}