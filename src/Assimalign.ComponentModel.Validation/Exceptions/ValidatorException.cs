using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ValidatorException : Exception
    {
        private const string messageDefault = "";

        /// <summary>
        /// 
        /// </summary>
        public ValidatorException() : base(messageDefault)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public ValidatorException(string message) : base(message)
        {

        }
    }
}
