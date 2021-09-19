using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation
{
    public sealed class ValidationError
    {


        public ValidationError()
        {

        }

        /// <summary>
        /// A unique error code for the validation error.
        /// </summary>
        public string Code { get; set; } = "400";

        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The source of the validation error such as a member of method.
        /// </summary>
        public string Source { get; set; }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Error {Code}: {Message}. {Environment.NewLine} -> Source: {Source}";
        }
    }
}
