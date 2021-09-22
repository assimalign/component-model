﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Abstraction
{
    /// <summary>
    /// 
    /// </summary>
    public interface IValidationError
    {
        /// <summary>
        /// A unique error code to use when the validation rule fails.
        /// </summary>
        string Code { get; set; }

        /// <summary>
        /// A unique error message to use when the validation rule fails.
        /// </summary>
        string Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string Source { get; set; }
    }
}
