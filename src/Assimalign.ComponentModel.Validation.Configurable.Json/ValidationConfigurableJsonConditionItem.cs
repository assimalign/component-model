using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Assimalign.ComponentModel.Validation.Configurable;

using Assimalign.ComponentModel.Validation.Configurable.Internal.Extensions;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class ValidationConfigurableJsonConditionItem<T> : IValidationCondition
    where T : class
{
    IEnumerable<IValidationItem> IValidationCondition.ValidationItems => this.ValidationItems;


    /// <summary>
    /// Represents a set of Conditions to run against <typeparamref name="T"/>.
    /// </summary>
    [JsonPropertyName("$condition")]
    public ValidationConfigurableJsonCondition<T> Condition { get; set; }

    /// <summary>
    /// The items to be validated if the condition is true.
    /// </summary>
    [JsonPropertyName("$validationItems")]
    public IEnumerable<ValidationConfigurableJsonItem<T>> ValidationItems { get; set; }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Expression<Func<T, bool>> GetCondition()
    {
        var parameter = Expression.Parameter(typeof(T));
        var body = GetLambdaExpressionBody(this, parameter);
        var lambda = Expression.Lambda<Func<T, bool>>(body, parameter);

        return lambda;
    }

    /// <summary>
    /// Loops through all children building an expression tree on the fly.
    /// </summary>
    /// <param name="conditionItem"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    private Expression GetLambdaExpressionBody(ValidationConfigurableJsonConditionItem<T> conditionItem, Expression parameter)
    {
        conditionItem.Condition.And ??= new List<ValidationConfigurableJsonConditionItem<T>>();
        conditionItem.Condition.Or ??= new List<ValidationConfigurableJsonConditionItem<T>>();

        // Represent a Parent expression to reference for any child expression
        Expression parent = null;

        if (conditionItem.Condition.And.Any())
        {
            foreach (var where in conditionItem.Condition.And)
            {
                var isParent = parent != null;
                // Check if only one filter was passed
                if (!isParent && !where.Condition.And.Any() && !where.Condition.Or.Any())
                {
                    parent = BuildLambdaExpressionBody(where, parameter);
                }
                else if (isParent)
                {
                    var child = GetLambdaExpressionBody(where, parameter);
                    if (where.Condition.Or.Any())
                    {
                        parent = Expression.AndAlso(parent, child);
                    }
                    else // We've reached the end of the And 'Concatenation'
                    {
                        parent = Expression.AndAlso(parent, child);
                    }
                }
            }

            return parent;
        }

        if (conditionItem.Condition.Or.Any())
        {
            foreach (var where in conditionItem.Condition.Or)
            {
                var isParent = parent != null;
                // Check if only one filter was passed
                if (!isParent && !where.Condition.And.Any() && !where.Condition.Or.Any())
                {
                    parent = BuildLambdaExpressionBody(where, parameter);
                }
                else if (isParent)
                {
                    var child = GetLambdaExpressionBody(where, parameter);
                    if (where.Condition.And.Any())
                    {
                        parent = Expression.OrElse(parent, child);
                    }
                    else // We've reached the end of the And 'Concatenation'
                    {
                        parent = Expression.OrElse(parent, child);
                    }
                }
            }

            return parent;
        }

        return BuildLambdaExpressionBody(conditionItem, parameter);
    }



    /// <summary>
    /// Builds a chained or member expression evaluation.
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="expression"></param>
    /// <returns></returns>
    private Expression BuildLambdaExpressionBody(ValidationConfigurableJsonConditionItem<T> condition, Expression expression)
    {
        // Check if Expression is ParameterExpression (Only Parameter Expressions should be passed at this point)
        if (expression is ParameterExpression parameter)
        {
            // Get the member expression to apply the evaluation to.
            if (condition.Condition.Member is not null)
            {
                var paths = condition.Condition.Member.Split('.');

                for (int i = 0; i < paths.Length; i++)
                {
                    expression = Expression.Property(expression, paths[i]);
                }
            }
            //// Check if any reserved functions were set
            //if (condition.Function is not ValueTypeFunctions.None)
            //{
            //    if (condition.Function == ValueTypeFunctions.ToLower)
            //        expression = CosmosUtility.GetMethodExpression(expression, CosmosUtility.GetToLowerMethod());

            //    else if (condition.Function == ValueTypeFunctions.ToUpper)
            //        expression = CosmosUtility.GetMethodExpression(expression, CosmosUtility.GetToUpperMethod());
            //}
            //// Check if there are any child functions to chain to the clause
            //if (condition.IsFunctionAvailable && condition.CurrentFunction.TryGetExpression(expression, out var methodchain))
            //{
            //    expression = methodchain;
            //}
            // Check if there is an evaluation to a property of the root of the queryable type
            if (condition.Condition.Value is string stringValue)
            {
                var paths = stringValue.Split('.');

                if (paths.First() == "$root")
                {
                    if (paths.Count() == 1)
                    {
                        throw new Exception();
                        //throw new CosmosInvalidPropertyException(
                        //    $"A property chain must be provided hen using '$root' as the value comparison");
                    }

                    for (int i = 1; i < paths.Length; i++)
                    {
                        expression = Expression.Property(expression, paths[i]);
                    }
                }
            }
        }
        // If we made it this far something unexpected has happened
        else
        {
            throw new Exception();
            //throw new CosmosInternalException(
            //    $"Unable to create '$where' clause parameter for property: {condition.Property}");
        }

        // Check if Value is of a $root property to compare to 
        if (condition.Condition.Value is MemberExpression memberValue)
        {
            Expression constant = memberValue;

            // Check if query is requesting to compare complex type or
            // Check if the member is a nullable and convert to a non nullable type
            if (constant.Type.IsNullableType(out var type))
            {
                constant = Expression.Convert(constant, type);
            }

            // The types need to be the same
            if (constant.Type != expression.Type)
            {
                throw new Exception();// expression.Type.Name, member.Member.Name);
            }

            switch (condition.Condition.Operator)
            {
                case OperatorType.EQ:
                    return Expression.Equal(expression, constant);
                case OperatorType.NE:
                    return Expression.NotEqual(expression, constant);
                case OperatorType.GT:
                    return Expression.GreaterThan(expression, constant);
                case OperatorType.GTE:
                    return Expression.GreaterThanOrEqual(expression, constant);
                case OperatorType.LT:
                    return Expression.LessThan(expression, constant);
                case OperatorType.LTE:
                    return Expression.LessThanOrEqual(expression, constant);
                default:
                    return null;
            }
        }
        //else
        //{
        //    throw new Exception();
        //}
        ////// Check if the evaluation is boolean or null, if so, this means our counterpart expression should be a predicate
        //if (condition.Value is null || condition.Value is bool)
        //{
        //    // If value is null then let's assume the evaluation is a predicate (true/false)
        //    var boolean = condition.Value == null || (bool)condition.Value;
        //    var constant = CosmosUtility.GetArgumentExpression(boolean);

        //    // Check if a OrElse or AndAlso Binary Expression was returned
        //    if (expression is BinaryExpression binary)
        //    {
        //        return boolean ? binary : CosmosUtility.GetOperatorExpression(
        //                 condition.Operator,
        //                 binary,
        //                 constant);
        //    }
        //    // Check if expression is a called method and the method returns boolean type
        //    // This will cut down on the extra predicate if continuing
        //    if (expression is MethodCallExpression method)
        //    {
        //        if (!method.Type.IsBooleanType())
        //        {
        //            // An Invalid Evaluation was requested. This means the type
        //            // comparison is invalid (Bad Evaluation Example: true == 1)
        //            throw new CosmosInvalidComparisonException(
        //                "Either '$value' was not set or the comparison " +
        //                "requested between '$property' & '$value' is inequitable");
        //        }
        //        // Return an operator expression
        //        return boolean ? method : CosmosUtility.GetOperatorExpression(
        //            condition.Operator,
        //            method,
        //            constant);
        //    }
        //    else
        //    {
        //        throw new CosmosInvalidComparisonException(
        //            $"The property or method does not match the evaluation " +
        //            $"type: '? - unknown member' {condition.Operator} '{condition.Value}'");
        //    }
        //}
        //else
        // {
        // If any other expression makes it past this point
        // and is not of member of method call expression then throw an exception

        // Lets pull out the nullable value type if any
        //if (expression is LambdaExpression lambda && lambda.Body.Type.IsSystemValueType(out var imp))
        //{
        //    expression = Expression.Convert(expression, imp);
        //}
        //else
        //{
        //    throw new Exception(); // CosmosInvalidPropertyException("");
        //}

        // Convert Type if possible to ensure evaluation is coursing the correct type
        //ConvertValueType(condition, ((LambdaExpression)expression).Body.Type);

        if (expression is MemberExpression member)
        {
            if (member.Type.IsValueType)
            {
                var type = member.Type.IsNullableType(out var nullType) ? nullType : member.Type;

                if (condition.Condition.Value.GetType() != type)
                {
                    condition.Condition.Value = Convert.ChangeType(condition.Condition.Value, type);
                }
            }
            else if (member.Type.IsEnum)
            {
                condition.Condition.Value = condition.Condition.Value is string value ?
                   Enum.Parse(member.Type, value, true) : 
                   throw new Exception();
            }

            var constant = Expression.Constant(condition.Condition.Value, member.Type);

            // Return an operator expression
            switch (condition.Condition.Operator)
            {
                case OperatorType.EQ:
                    return Expression.Equal(expression, constant);
                case OperatorType.NE:
                    return Expression.NotEqual(expression, constant);
                case OperatorType.GT:
                    return Expression.GreaterThan(expression, constant);
                case OperatorType.GTE:
                    return Expression.GreaterThanOrEqual(expression, constant);
                case OperatorType.LT:
                    return Expression.LessThan(expression, constant);
                case OperatorType.LTE:
                    return Expression.LessThanOrEqual(expression, constant);
                default:
                    return null;
            }
        }
        else
        {
            throw new Exception();
        }

        
        //}
    }
}

