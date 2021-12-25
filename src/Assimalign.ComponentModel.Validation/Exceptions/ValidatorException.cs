using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ValidatorException : Exception
    {
        private const string messageDefault = "An unknown error was caught.";

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




        /// <summary>
        /// 
        /// </summary>
        public int ErrorCode { get; set; } = ValidatorErrors.UnknownError;

        /// <summary>
        /// 
        /// </summary>
        public override string Source { get; set; }

    }
}
