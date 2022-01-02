
- [Assimalign Component Model](#assimalign-component-model)
- [Available Libraries](#available-libraries)
  - [Deep Cloning](#deep-cloning)
  - [Fluent Model Validation](#fluent-model-validation)
  - [Configurable Fluent Model Validation](#configurable-fluent-model-validation)
  - [Model Mapping](#model-mapping)
  - [Model Mapping Configurable](#model-mapping-configurable)



# Assimalign Component Model
A collection of libraries extending the functionality of System.ComponentModel within dotnet runtime.


# Available Libraries
Below is a list of available libraries.


|  Package ID                                           |  Latest Version  | Downloads | 
| ----------------------------------------------------- | ---------------- | --------- |
| `Assimalign.ComponentModel.Core`                      | [![NuGet](https://img.shields.io/nuget/v/Assimalign.ComponentModel.Core.svg)](https://nuget.org/packages/Assimalign.ComponentModel.Core) | [![Nuget](https://img.shields.io/nuget/dt/Assimalign.ComponentModel.Core.svg)](https://nuget.org/packages/Assimalign.ComponentModel.Core) |
| `Assimalign.ComponentModel.Cloning`                   | [![NuGet](https://img.shields.io/nuget/v/Assimalign.ComponentModel.Cloning.svg)](https://nuget.org/packages/Assimalign.ComponentModel.Cloning) | [![Nuget](https://img.shields.io/nuget/dt/Assimalign.ComponentModel.Cloning.svg)](https://nuget.org/packages/Assimalign.ComponentModel.Cloning) |
| `Assimalign.ComponentModel.Mapping`                   | [![NuGet](https://img.shields.io/nuget/v/Assimalign.ComponentModel.Mapping.svg)](https://nuget.org/packages/Assimalign.ComponentModel.Mapping) | [![Nuget](https://img.shields.io/nuget/dt/Assimalign.ComponentModel.Mapping.svg)](https://nuget.org/packages/Assimalign.ComponentModel.Mapping) |
| `Assimalign.ComponentModel.Mapping.Configurable`      | [![NuGet](https://img.shields.io/nuget/v/Assimalign.ComponentModel.Mapping.Configurable.svg)](https://nuget.org/packages/Assimalign.ComponentModel.Mapping.Configurable) | [![Nuget](https://img.shields.io/nuget/dt/Assimalign.ComponentModel.Mapping.Configurable.svg)](https://nuget.org/packages/Assimalign.ComponentModel.Mapping.Configurable) |
| `Assimalign.ComponentModel.Validation`                | [![NuGet](https://img.shields.io/nuget/v/Assimalign.ComponentModel.Validation.svg)](https://nuget.org/packages/Assimalign.ComponentModel.Validation) | [![Nuget](https://img.shields.io/nuget/dt/Assimalign.ComponentModel.Validation.svg)](https://nuget.org/packages/Assimalign.ComponentModel.Validation) |
| `Assimalign.ComponentModel.Validation.Configurable`   | [![NuGet](https://img.shields.io/nuget/v/Assimalign.ComponentModel.Validation.Configurable.svg)](https://nuget.org/packages/Assimalign.ComponentModel.Validation.Configurable) | [![Nuget](https://img.shields.io/nuget/dt/Assimalign.ComponentModel.Validation.Configurable.svg)](https://nuget.org/packages/Assimalign.ComponentModel.Validation.Configurable) |

<br/>
<br/>

---


## [Deep Cloning](./docs/cloning/overview.md)

|Build Status (By Branch) | Build Status |
|-------------------------|--------------|
|**Main**                 |![GitHub Workflow Status Main](https://img.shields.io/github/workflow/status/Assimalign-LLC/asal-component-model/assimalign.componentmodel.cloning.build.ci/main) |
|**Development**          |![GitHub Workflow Status Dev](https://img.shields.io/github/workflow/status/Assimalign-LLC/asal-component-model/assimalign.componentmodel.cloning.build.ci/development) |


**Summary:**
A library for Fluent object building using interfaces and func's

<br/>
<br/>

---


## [Fluent Model Validation](./docs/validation/overview.md)
|Build Status (By Branch) | Build Status |
|-------------------------|--------------|
|**Main**                 |![GitHub Workflow Status Main](https://img.shields.io/github/workflow/status/Assimalign-LLC/asal-component-model/assimalign.componentmodel.validation.build.ci/main) |
|**Development**          |![GitHub Workflow Status Dev](https://img.shields.io/github/workflow/status/Assimalign-LLC/asal-component-model/assimalign.componentmodel.validation.build.ci/development) |


**Summary:**

The following library offers an extensive set of fluent APIs for object validation which extend the [System.ComponentModel](https://github.com/dotnet/runtime/tree/main/src/libraries/System.ComponentModel) within [dotnet/runtime](https://github.com/dotnet/runtime). The implementation is very similar to [FluentValidation](https://github.com/FluentValidation) however the intent of remaking this library was to focus on three areas of improvement: 
- **Remove Constructor Configuration into Configurable Method**
- **Simplify the Abstraction for easier extensibility**
- **Allow for Configurable based Validation via Configuration Provider**


<br/>
<br/>

---

## [Configurable Fluent Model Validation](./docs/validation/configurable/overview.md)
|Build Status (By Branch) | Build Status |
|-------------------------|--------------|
|**Main**                 |![GitHub Workflow Status Main](https://img.shields.io/github/workflow/status/Assimalign-LLC/asal-component-model/assimalign.componentmodel.validation.configurable.build.ci/main) |
|**Development**          |![GitHub Workflow Status Dev](https://img.shields.io/github/workflow/status/Assimalign-LLC/asal-component-model/assimalign.componentmodel.validation.configurable.build.ci/development)|


**Summary:**
This library extends off of the [Fluent Model Validation](#fluent-model-validation) library adding configurable APIs.

<br/>
<br/>

---


## [Model Mapping](./docs/mapping/overview.md)
|Build Status (By Branch) | Build Status |
|-------------------------|--------------|
|**Main**                 |![GitHub Workflow Status Main](https://img.shields.io/github/workflow/status/Assimalign-LLC/asal-component-model/assimalign.componentmodel.mapping.build.ci/main) |
|**Development**          |![GitHub Workflow Status Dev](https://img.shields.io/github/workflow/status/Assimalign-LLC/asal-component-model/assimalign.componentmodel.mapping.build.ci/development)|


**Summary:**
A fluent object-to-object mapping.

<br/>
<br/>

---


##  [Model Mapping Configurable](./docs/mapping/configurable/overview.md)
|Build Status (By Branch) |Build Status|
|-------|------------|
|**Main**|![GitHub Workflow Status Main](https://img.shields.io/github/workflow/status/Assimalign-LLC/asal-component-model/assimalign.componentmodel.mapping.configurable.build.ci/main) |
|**Development**|![GitHub Workflow Status Dev](https://img.shields.io/github/workflow/status/Assimalign-LLC/asal-component-model/assimalign.componentmodel.mapping.configurable.build.ci/development)|


**Summary:**
A configuration based object-to-object mapping.