# BP0006: Ordinal String Comparison

## Cause

The method `String.Compare` or `String.Equals` was called without specifying an ordinal comparison.

## Rule description

Certain string APIs are considered extremely inefficient except for ordinal comparisons[[1]](#1).
The `String.Equals` method should not be used without method overloading (specifying an ordinal comparison)[[3]](#3).

## How to fix violations

To ensure better performance when comparing strings, the method should be called overloaded using a parameter of the type StringComparison (starting with Ordinal).

An alternative option is to use the 'String.CompareOrdinal' method[[2]](#2).

## When to suppress warnings

If an ordinal comparison must be dispensed with.

## Example of a violation

### Description

Regular comparison of strings.

### Code

```csharp
String.Compare("Hallo", "H@llo");

String.Equals("Velo", "Velo");
```

## Example of how to fix

### Description

Use of a comparison with ordinal comparison.

### Code

```csharp
string a = "Hallo";
string b = "H@llo";

String.Compare(a, b, StringComparison.Ordinal);

String.Equals(a, b, StringComparison.Ordinal);
```

## Related rules


[BP0007: Inefficient String Methods](https://github.com/OliverAmstutz/Best-Practice-Checker/tree/main/Docs/References/BP0007_InefficientStringMethods.md)

## References
<a id="1">[1]</a>
Unity Technologies, 27. Oktober 2020, Strings and text. <br /> 
Accessed 08. November 2020 from https://docs.unity3d.com/Manual/BestPracticeUnderstandingPerformanceInUnity5.html

<a id="2">[2]</a>
Microsoft Documentation, 11. November 2020,String.CompareOrdinal Method. <br />
Accessed 11. November 2020 from https://docs.microsoft.com/en-us/dotnet/api/system.string.compareordinal?view=netcore-3.1

<a id="3">[3]</a>
Unity Technologies, 11. November 2020. September 2020, Best practices for comparing strings in .NET. <br />
Accessed 11. November 2020 from https://docs.microsoft.com/en-us/dotnet/standard/base-types/best-practices-strings?redirectedfrom=MSDN
