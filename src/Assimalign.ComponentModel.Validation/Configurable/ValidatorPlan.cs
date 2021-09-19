﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Configurable
{
    internal class ValidatorPlan
    {



        [JsonPropertyName("$ruleFor")]
        public ValidatorPlanRule RuleFor { get; set; }



        [JsonPropertyName("$ruleForEach")]
        public ValidatorPlanRule RuleForEach { get; set; }






        public IValidator<T> Compile<T>()
        {
            throw new NotImplementedException();
        }
    }
}
