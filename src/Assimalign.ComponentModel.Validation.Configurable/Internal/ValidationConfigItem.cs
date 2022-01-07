using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Configurable;


internal class ValidationConfigItem<T> : IValidationItem
{

    public ValidationConfigItem()
    {
        ItemRuleStack = new ValidationRuleStack();
    }


    public IValidationRuleStack ItemRuleStack { get; }

    public Expression<Func<T, object>> ItemExpression { get; set; }
    
    public void Evaluate(IValidationContext context)
    {
        throw new NotImplementedException();
    }
}
