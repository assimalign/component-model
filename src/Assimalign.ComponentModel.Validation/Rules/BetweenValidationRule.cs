using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Rules
{
    using Assimalign.ComponentModel.Validation.Extensions;
    using Assimalign.ComponentModel.Validation.Abstraction;

    internal sealed class BetweenValidationRule<T, TValue, TLowerBound, TUpperBound> : IValidationRule
        where TLowerBound : struct, IComparable
        where TUpperBound : struct, IComparable
    {
        private readonly TLowerBound lower;
        private readonly TUpperBound upper;
        private readonly Expression<Func<T, TValue>> expression;

        public BetweenValidationRule(Expression<Func<T, TValue>> expression, TLowerBound lower, TUpperBound upper)
        {
            this.lower = lower;
            this.upper = upper;
            this.expression = expression;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; } = "";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="Exception"></exception>
        public void Evaluate(IValidationContext context)
        {
            if (context.ValidationInstance is T instance)
            {
                var value = this.expression.Compile().Invoke(instance);
               
                if (value is IEnumerable<DateTime> dateTimes)
                {
                    if (lower is DateTime lowerDateTime && upper is DateTime upperDateTime)
                    {
                        foreach (var dt in dateTimes)
                        {
                            if (dt < lowerDateTime || dt > upperDateTime)
                            {
                                context.AddFailure(new ValidationError());
                                break;
                            }
                        }
                        return;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                if (value is DateTime dateTime)
                {
                    if (lower is DateTime lowerDateTime && upper is DateTime upperDateTime)
                    {
                        if (dateTime < lowerDateTime || dateTime > upperDateTime)
                        {
                            context.AddFailure(new ValidationError());
                        }
                        return;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                if (value is IEnumerable<TimeSpan> timeSpans)
                {
                    if (lower is TimeSpan lowerTimeSpan && upper is TimeSpan upperTimeSpan)
                    {
                        foreach (var ts in timeSpans)
                        {
                            if (ts < lowerTimeSpan || ts > upperTimeSpan)
                            {
                                context.AddFailure(new ValidationError());
                                break;
                            }
                        }
                        return;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                if (value is TimeSpan timeSpan)
                {
                    if (lower is TimeSpan lowerTimeSpan && upper is TimeSpan upperTimeSpan)
                    {
                        if (timeSpan < lowerTimeSpan || timeSpan > upperTimeSpan)
                        {
                            context.AddFailure(new ValidationError());
                        }
                        return;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                if (value is IEnumerable<short> int16s)
                {
                    if (lower is short lowerInt16 && upper is short upperInt16)
                    {
                        foreach (var number in int16s)
                        {
                            if (number < lowerInt16 || number > upperInt16)
                            {
                                context.AddFailure(new ValidationError());
                                break;
                            }
                        }
                        return;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                if (value is short int16)
                {
                    if (lower is short lowerInt16 && upper is short upperInt16)
                    {
                        if (int16 < lowerInt16 || int16 > upperInt16)
                        {
                            context.AddFailure(new ValidationError());
                        }
                        return;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                if (value is IEnumerable<int> int32s)
                {
                    if (lower is int lowerInt32 && upper is int upperInt32)
                    {
                        foreach (var number in int32s)
                        {
                            if (number < lowerInt32 || number > upperInt32)
                            {
                                context.AddFailure(new ValidationError());
                                break;
                            }
                        }
                        return;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                if (value is int int32 || value is Nullable<int>)
                {
                    if (lower is int lowerInt32 && upper is int upperInt32)
                    {
                        if (int32 < lowerInt32 || int32 > upperInt32)
                        {
                            context.AddFailure(new ValidationError());
                        }
                        return;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                if (value is IEnumerable<long> int64s)
                {
                    if (lower is long lowerInt64 && upper is long upperInt64)
                    {
                        foreach(var number in int64s)
                        {
                            if (number < lowerInt64 || number > upperInt64)
                            {
                                context.AddFailure(new ValidationError());
                                break;
                            }
                        }
                        return;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                if (value is long int64)
                {
                    if (lower is long lowerInt64 && upper is long upperInt64)
                    {
                        if (int64 < lowerInt64 || int64 > upperInt64)
                        {
                            context.AddFailure(new ValidationError());
                        }
                        return;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                if (value is IEnumerable<double> dbls)
                {
                    if (lower is double lowerDouble && upper is double upperDouble)
                    {
                        foreach (var number in dbls)
                        {
                            if (number < lowerDouble || number > upperDouble)
                            {
                                context.AddFailure(new ValidationError());
                                break;
                            }
                        }
                        return;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                if (value is double dble)
                {
                    if (lower is double lowerDouble && upper is double upperDouble)
                    {
                        if (dble < lowerDouble || dble > upperDouble)
                        {
                            context.AddFailure(new ValidationError());
                        }
                        return;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                if (value is IEnumerable<decimal> decis)
                {
                    if (lower is decimal lowerDecimal && upper is decimal upperDecimal)
                    {
                        foreach(var number in decis)
                        {
                            if (number < lowerDecimal || number > upperDecimal)
                            {
                                context.AddFailure(new ValidationError());
                                break;
                            }
                        }
                        return;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                if (value is decimal deci)
                {
                    if (lower is decimal lowerDecimal && upper is decimal upperDecimal)
                    {
                        if (deci < lowerDecimal || deci > upperDecimal)
                        {
                            context.AddFailure(new ValidationError());
                        }
                        return;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                if (value is IEnumerable<float> flts)
                {
                    if (lower is float lowerFloat && upper is float upperFloat)
                    {
                        foreach(var number in flts)
                        {
                            if (number < lowerFloat || number > upperFloat)
                            {
                                context.AddFailure(new ValidationError());
                                break;
                            }
                        }
                        return;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                if (value is float flt)
                {
                    if (lower is float lowerFloat && upper is float upperFloat)
                    {
                        if (flt < lowerFloat || flt > upperFloat)
                        {
                            context.AddFailure(new ValidationError());
                        }
                        return;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }
        }
    }
}
