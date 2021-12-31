using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ValidatorTestProfile : ValidationProfile<ValidatorTestObject>
{
    public override void Configure(IValidationRuleDescriptor<ValidatorTestObject> descriptor)
    {
        descriptor.RuleFor(p => p.EqualToFields.ShortEqualToSuccessField)
            .EqualTo(ValidatorTestValues.ShortEqualToValue);

        descriptor.RuleFor(p => p.EqualToFields.ShortEqualToFailureField)
            .EqualTo(ValidatorTestValues.ShortEqualToValue);


        descriptor.RuleFor(p => p.EqualToFields.IntEqualToSuccessField)
            .EqualTo(ValidatorTestValues.IntEqualToValue);

        descriptor.RuleFor(p => p.EqualToFields.IntEqualToFailureField)
            .EqualTo(ValidatorTestValues.IntEqualToValue);


        descriptor.RuleFor(p => p.EqualToFields.LongEqualToFailureField)
            .EqualTo(ValidatorTestValues.LongEqualToValue);

        descriptor.RuleFor(p => p.EqualToFields.LongEqualToFailureField)
            .EqualTo(ValidatorTestValues.LongEqualToValue);
    }

}
