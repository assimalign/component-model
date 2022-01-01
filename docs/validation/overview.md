



# Getting Started


### 1. Create a Validation Profile
```csharp 

public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthday { get; set; }
}


public class PersonValidationProfile : ValidationProfile<Person>
{

    public PersonValidationProfile()
    {
        // Specify that the validation stop of first failure
        base.ValidationMode = ValidationMode.Stop;
    }


    public override void Configure(IValidationRuleDescriptor<Person> descriptor)
    {
        descriptor.RuleFor(p => p.FirstName)
            .NotEmpty()
            .MaxLength(255);

        descriptor.RuleFor(p => p.LastName)
            .NotEmpty()
            .MaxLength(255);

        descriptor.RuleFor(p => p.Birthday)
            .LessThan(DateTime.Now)
            .NotEqual(default(DateTime));
    }
}
```

### 2. Build Validator
```csharp 

var validator = Validator.Create(options => 
{
    // Optional configurations
    options.ThrowExceptionOnFailure = true; // 

    // Required Configurations
    options.AddProfile(new PersonValidationProfile());
});

```


### 3. Run Validation 
```csharp

var person = new Person() 
{
    FirstName = "Chase",
    LastName = "Crawford",
    Birthday = default(DateTime)
};

var validationResults = validator.Validate(person);


```


