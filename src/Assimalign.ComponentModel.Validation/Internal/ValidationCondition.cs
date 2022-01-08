using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace Assimalign.ComponentModel.Validation.Internal;

internal sealed class ValidationCondition<T> : IValidationCondition<T>
{
    private IList<IValidationItem> validationItems;

    public ValidationCondition()
    {
        this.validationItems = new List<IValidationItem>();
    }

    public ValidationMode ValidationMode { get; set; }

    public IEnumerable<IValidationItem> ValidationItems
    {
        get => this.validationItems;
        set
        {
            if (value is IList<IValidationItem> list)
            {
                this.validationItems = list;
            }
            else
            {
                this.validationItems = value.ToList();
            }
        }
    }

    public Func<object, bool> CanEvaluate => this.Condition as Func<object, bool>;

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
            ValidationItems = new List<IValidationItem>(),
            ValidationCondition = condition.Compile(),
            ValidationMode = this.ValidationMode
        };

        configure.Invoke(descriptor);

        foreach (var item in descriptor.ValidationItems)
        {
            this.validationItems.Add(item);
        }

        return validationCondition;
    }
}

