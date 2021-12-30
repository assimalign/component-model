using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;


namespace Assimalign.ComponentModel.Validation.Internal;


internal sealed class ValidationCondition<T> : IValidationCondition<T>
{
    public ValidationMode ValidationMode { get; set; }

    public IList<IValidationItem> ValidationItems { get; set; }

    public Expression<Func<T, bool>> Condition { get; set; }

    public IValidationCondition<T> When(Expression<Func<T, bool>> condition, Action<IValidationRuleDescriptor<T>> configure)
    {
        var validationCondition = new ValidationCondition<T>()
        {
            Condition = condition,
            ValidationItems = this.ValidationItems
        };

        var descriptor = new ValidationRuleDescriptor<T>()
        {
            ValidationCondition = this.Condition.Compile(),
            ValidationMode = this.ValidationMode
        };

        configure.Invoke(descriptor);

        foreach (var item in descriptor.ValidationItems)
        {
            this.ValidationItems.Add(item);
        }

        return validationCondition;
    }
}

