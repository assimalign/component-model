﻿using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;

internal sealed class EmailValidationRule<T, TValue> : IValidationRule
{
    private readonly string pattern = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-||_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+([a-z]+|\d|-|\.{0,1}|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])?([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$";
    private readonly Expression<Func<T, TValue>> expression;
    private readonly string expressionBody;

    public EmailValidationRule(Expression<Func<T, TValue>> expression)
    {
        if (expression is null)
        {
            throw new ArgumentNullException(
                paramName: nameof(expression),
                message: $"The following expression where the 'EmailAddress()' rule is defined cannot be null.");
        }
        if (expression.Body is MemberExpression member)
        {
            this.expressionBody = string.Join('.', member.ToString().Split('.').Skip(1));
        }
        this.expression = expression;
    }

    public string Name => $"EmailValidationRule<{typeof(T).Name}, {expressionBody ?? typeof(TValue).Name}>";

    public IValidationError Error { get; set; }

    public ValidationRuleType RuleType { get; set; }

    public void Evaluate(IValidationContext context)
    {
        if (context.Instance is T instance)
        {
            var value = this.expression.Compile().Invoke(instance);

            if (value is IEnumerable emails)
            {
                foreach (var email in emails)
                {
                    if (email is string emailValue && !Regex.IsMatch(emailValue, pattern))
                    {
                        context.AddFailure(this.Error);
                        break;
                    }
                }
            }
            else if (value is string email)
            {
                if (!Regex.IsMatch(email, pattern))
                {
                    context.AddFailure(this.Error);
                }
            }
            else
            {
                context.AddSuccess(this);
            }
        }
    }
}