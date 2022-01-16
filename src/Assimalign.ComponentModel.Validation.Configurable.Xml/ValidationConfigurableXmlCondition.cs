using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Assimalign.ComponentModel.Validation.Configurable;

using Assimalign.ComponentModel.Validation.Configurable.Serialization;

public sealed class ValidationConfigurableXmlCondition<T> : IValidationCondition
{
   
    [XmlAnyElement("$and")]
    public IEnumerable<ValidationConfigurableXmlCondition<T>> And { get; set; }

    [XmlAnyElement("$or")]
    public IEnumerable<ValidationConfigurableXmlCondition<T>> Or { get; set; }

    [XmlAnyElement("$member")]
    public string Member { get; set; }

    [XmlAnyElement("$operator")]
    //[XmlConverter(typeof(EnumConverter<OperatorType>))]
    public OperatorType Operator { get; set; }

    [XmlAnyElement("$value")]
    public object Value { get; set; }

    [XmlAnyElement("$validationItems")]
    public IList<ValidationConfigurableXmlItem<T>> ValidationItems { get; set; }

    [XmlIgnore]
    IEnumerable<IValidationItem> IValidationCondition.ValidationItems => this.ValidationItems;



    internal Expression<Func<T, bool>> GetCondition()
    {
        return default;
    }




    //public Expression<Func<T, bool>> Build<T>()
    //{
    //    var parameter = Expression.Parameter(typeof(T));
    //    var lambda = Expression.Lambda<Func<T, bool>>(GetLambdaExpressionBody(this, parameter), parameter);

    //    return lambda;
    //}


    ///// <summary>
    ///// Loops through all children building an expression tree on the fly.
    ///// </summary>
    ///// <param name="instance"></param>
    ///// <param name="parameter"></param>
    ///// <returns></returns>
    //private Expression GetLambdaExpressionBody(ValidationConfigXmlCondition instance, Expression parameter)
    //{
    //    // Represent a Parent expression to reference for any child expression
    //    Expression parent = null;

    //    if (instance.And.Any())
    //    {
    //        foreach (var where in instance.And)
    //        {
    //            var isParent = parent != null;
    //            // Check if only one filter was passed
    //            if (!isParent && !where.And.Any() && !where.Or.Any())
    //            {
    //                parent = BuildLambdaExpressionBody(where, parameter);
    //            }
    //            else if (isParent)
    //            {
    //                var child = GetLambdaExpressionBody(where, parameter);
    //                if (where.Or.Any())
    //                {
    //                    parent = Expression.AndAlso(parent, child);
    //                }
    //                else // We've reached the end of the And 'Concatenation'
    //                {
    //                    parent = Expression.AndAlso(parent, child);
    //                }
    //            }
    //        }

    //        return parent;
    //    }

    //    if (instance.Or.Any())
    //    {
    //        foreach (var where in instance.Or)
    //        {
    //            var isParent = parent != null;
    //            // Check if only one filter was passed
    //            if (!isParent && !where.And.Any() && !where.Or.Any())
    //            {
    //                parent = BuildLambdaExpressionBody(where, parameter);
    //            }
    //            else if (isParent)
    //            {
    //                var child = GetLambdaExpressionBody(where, parameter);
    //                if (where.And.Any())
    //                {
    //                    parent = Expression.OrElse(parent, child);
    //                }
    //                else // We've reached the end of the And 'Concatenation'
    //                {
    //                    parent = Expression.OrElse(parent, child);
    //                }
    //            }
    //        }

    //        return parent;
    //    }

    //    return BuildLambdaExpressionBody(instance, parameter);
    //}



    ///// <summary>
    ///// Builds a chained or member expression evaluation.
    ///// </summary>
    ///// <param name="where"></param>
    ///// <param name="expression"></param>
    ///// <returns></returns>
    //private Expression BuildLambdaExpressionBody(CosmosWhere where, Expression expression)
    //{
    //    // Check if Expression is ParameterExpression (Only Parameter Expressions should be passed at this point)
    //    if (expression is ParameterExpression parameter)
    //    {
    //        // Get the member expression to apply the evaluation to.
    //        if (where.Property is not null)
    //        {
    //            expression = CosmosUtility.GetMemberExpression(where.Property, parameter);
    //        }
    //        // Check if any reserved functions were set
    //        if (where.Function is not ValueTypeFunctions.None)
    //        {
    //            if (where.Function == ValueTypeFunctions.ToLower)
    //                expression = CosmosUtility.GetMethodExpression(expression, CosmosUtility.GetToLowerMethod());

    //            else if (where.Function == ValueTypeFunctions.ToUpper)
    //                expression = CosmosUtility.GetMethodExpression(expression, CosmosUtility.GetToUpperMethod());
    //        }
    //        // Check if there are any child functions to chain to the clause
    //        if (where.IsFunctionAvailable && where.CurrentFunction.TryGetExpression(expression, out var methodchain))
    //        {
    //            expression = methodchain;
    //        }
    //        // Check if there is an evaluation to a property of the root of the queryable type
    //        if (where.Value is string stringValue)
    //        {
    //            var properties = stringValue.Split('.');
    //            if (properties.First() == "$root")
    //            {
    //                if (properties.Count() == 1)
    //                {
    //                    throw new CosmosInvalidPropertyException(
    //                        $"A property chain must be provided hen using '$root' as the value comparison");
    //                }
    //                where.Value = CosmosUtility.GetMemberExpression(string.Join('.', properties.Skip(1)), parameter);
    //            }
    //        }
    //    }
    //    // If we made it this far something unexpected has happened
    //    else
    //    {
    //        throw new CosmosInternalException(
    //            $"Unable to create '$where' clause parameter for property: {where.Property}");
    //    }

    //    // Check if Value is of a $root property to compare to 
    //    if (where.Value is MemberExpression member)
    //    {
    //        Expression constant = member;

    //        // Check if query is requesting to compare complex type or
    //        // Check if the member is a nullable and convert to a non nullable type
    //        if (constant.Type.IsSystemValueType(out var type))
    //        {
    //            constant = Expression.Convert(constant, type);
    //        }
    //        else
    //        {
    //            throw new CosmosInvalidComparisonException(member.Member.Name);
    //        }
    //        // The types need to be the same
    //        if (member.Type != expression.Type)
    //        {
    //            throw new CosmosInvalidComparisonException(expression.Type.Name, member.Member.Name);
    //        }

    //        // Return an operator expression
    //        return CosmosUtility.GetOperatorExpression(
    //            where.Operator,
    //            expression,
    //            constant);
    //    }
    //    // Check if the evaluation is boolean or null, if so, this means our counterpart expression should be a predicate
    //    if (where.Value is null || where.Value is bool)
    //    {
    //        // If value is null then let's assume the evaluation is a predicate (true/false)
    //        var boolean = where.Value == null || (bool)where.Value;
    //        var constant = CosmosUtility.GetArgumentExpression(boolean);

    //        // Check if a OrElse or AndAlso Binary Expression was returned
    //        if (expression is BinaryExpression binary)
    //        {
    //            return boolean ? binary : CosmosUtility.GetOperatorExpression(
    //                     where.Operator,
    //                     binary,
    //                     constant);
    //        }
    //        // Check if expression is a called method and the method returns boolean type
    //        // This will cut down on the extra predicate if continuing
    //        if (expression is MethodCallExpression method)
    //        {
    //            if (!method.Type.IsBooleanType())
    //            {
    //                // An Invalid Evaluation was requested. This means the type
    //                // comparison is invalid (Bad Evaluation Example: true == 1)
    //                throw new CosmosInvalidComparisonException(
    //                    "Either '$value' was not set or the comparison " +
    //                    "requested between '$property' & '$value' is inequitable");
    //            }
    //            // Return an operator expression
    //            return boolean ? method : CosmosUtility.GetOperatorExpression(
    //                where.Operator,
    //                method,
    //                constant);
    //        }
    //        else
    //        {
    //            throw new CosmosInvalidComparisonException(
    //                $"The property or method does not match the evaluation " +
    //                $"type: '? - unknown member' {where.Operator} '{where.Value}'");
    //        }
    //    }
    //    else
    //    {
    //        // If any other expression makes it past this point
    //        // and is not of member of method call expression then throw an exception

    //        // Lets pull out the nullable value type if any
    //        if (expression.Type.IsSystemValueType(out var imp))
    //        {
    //            expression = Expression.Convert(expression, imp);
    //        }
    //        else
    //        {
    //            throw new CosmosInvalidPropertyException("");
    //        }

    //        // Convert Type if possible to ensure evaluation is coursing the correct type
    //        ConvertValueType(ref where, expression.Type);

    //        var constant = CosmosUtility.GetArgumentExpression(where.Value);

    //        // Return an operator expression
    //        return CosmosUtility.GetOperatorExpression(
    //            where.Operator,
    //            expression,
    //            constant);
    //    }
    //}


    ///// <summary>
    ///// Depending on the Expression Method or Member return type you may need 
    ///// to coarse the evaluation value to the correct type. Example: int32 -> decimal
    ///// </summary>
    ///// <param name="where"></param>
    ///// <param name="type"></param>
    //private void ConvertValueType(ref CosmosWhere where, Type type)
    //{
    //    try
    //    {
    //        if (type.IsEnum)
    //        {
    //            where.Value = where.Value is string stringEnum ?
    //                Enum.Parse(type, stringEnum, true) :
    //                // Only except Enum query values for as string name of the Enum values
    //                throw new CosmosInvalidValueException($"Value '{where.Value}' is not supported for Enum evaluation.");

    //            if (where.Operator is not OperatorType.Equal)
    //            {
    //                throw new CosmosInvalidComparisonException(
    //                    $"'Enum' values can only be compared using the equality operator. " +
    //                    $"'{where.Operator}' is not supported for property '{where.Property}'.");
    //            }
    //        }
    //        else if (type.IsValueType)
    //        {
    //            where.Value = Convert.ChangeType(where.Value, type);
    //        }
    //        else
    //        {
    //            throw new CosmosInternalException(
    //                message: "Unable to evaluate comparison type.",
    //                source: $"Evaluation for: '{where.Property}' {where.Operator} '{where.Value}'");
    //        }
    //    }
    //    catch (InvalidCastException exception)
    //    {
    //        throw new CosmosInvalidComparisonException(exception);
    //    }
    //    catch (Exception exception)
    //    {
    //        throw new CosmosInvalidComparisonException(exception, type.Name);
    //    }
    //}
}

