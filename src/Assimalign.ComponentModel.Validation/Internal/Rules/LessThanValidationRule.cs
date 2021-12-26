using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Rules
{
   
    using Assimalign.ComponentModel.Validation.Extensions;
    using Assimalign.ComponentModel.Validation.Exceptions;

    internal sealed class LessThanValidationRule<T, TValue> : IValidationRule
        where TValue : IComparable
    {
        private readonly Type type = typeof(TValue);
        private readonly Expression<Func<T, TValue>> expression;
        
        public LessThanValidationRule(Expression<Func<T, TValue>> expression)
        {
            this.expression = expression;
        }





        public string Name { get; }

        public void Evaluate(IValidationContext context)
        {
            if (context.Instance is T instance)
            {
                var value = expression.Compile().Invoke(instance);
                
                // Let's check that the TValue type is numeric and pull 
                // out the underlying numeric type if Nullable
                if (type.IsNumericType(out var numericType))
                {

                }
                else
                {
                    throw new ValidationInvalidEvaluationException(
                        message: $"",
                        source: $"");
                }

                switch (expression.Body)
                {
                    case MemberExpression member: 
                    {
                            
                        
                        break;
                    }
                    case MethodCallExpression method:
                    {
                        break;
                    
                    }
                }
            }
        }
    }
}
