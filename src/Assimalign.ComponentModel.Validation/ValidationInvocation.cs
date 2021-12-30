using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation;

/// <summary>
/// 
/// </summary>
public sealed class ValidationInvocation
{
    
    internal ValidationInvocation(string ruleName, bool invoked, long elapsedTicks = 0)
    {
        this.RuleName = ruleName;
        this.Invoked = invoked;
        this.ElapsedTicks = elapsedTicks;
    }

    /// <summary>
    /// A flag indicating whether the validation rule was invoked.
    /// </summary>
    public bool Invoked { get; }
    /// <summary>
    /// The name of the rule that was invoked.
    /// </summary>
    public string RuleName { get; }
    /// <summary>
    /// 
    /// </summary>
    public long? ElapsedTicks { get; }
    /// <summary>
    /// 
    /// </summary>
    public double? ElapsedMiliseconds => this.ElapsedTicks is null ? null : (double)this.ElapsedTicks / (double)TimeSpan.TicksPerMillisecond;
    /// <summary>
    /// 
    /// </summary>
    public double? ElapsedSeconds => this.ElapsedTicks is null ? null : (double)this.ElapsedTicks / (double)TimeSpan.TicksPerSecond;

}
