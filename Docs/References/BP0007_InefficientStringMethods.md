# BP0007: Inefficient String Methods

## Cause

You have called one of the following inefficient string methods:
  - `String.StartsWith`
  - `String.EndsWith`

## Rule description

With the exception of switching to ordinal comparisons, certain C# string APIs are extremely inefficient and can lead to performance problems [[1]]. Among them are the following methods:
- `String.StartsWith`
- String.EndsWith

## How to fix violations

According to the problems described, 
the methods should be implemented themselves whenever possible or at least executed using ordinals.
The methods 'String.StartsWith' and 'String.EndsWith' can be replaced and optimised relatively easily.

## When to suppress warnings

Never

## Example of a violation

### Description

The inefficient string method `String.StartsWith` is called.

### Code

```csharp
"RespawnEnemy".StartsWith("Respawn");
```

## Example of how to fix

### Description

Provide own implementation or use ordinal method [[1]].

### Code

```
// Example of an implementation for .StartsWith and .EndsWith 
public static bool CustomEndsWith(string a, string b) {
    int ap = a.Length - 1;
    int bp = b.Length - 1;

    while (ap >= 0 && bp >= 0 && a [ap] == b [bp]) {
        ap--;
        bp--;
    }
    return (bp < 0 && a.Length >= b.Length) || (ap < 0 && b.Length >= a.Length);
    }

public static bool CustomStartsWith(string a, string b) {
    int aLen = a.Length;
    int bLen = b.Length;
    int ap = 0; int bp = 0;

    while (ap < aLen && bp < bLen && a [ap] == b [bp]) {
        ap++;
        bp++;
    }

    return (bp == bLen && aLen >= bLen) || (ap == aLen && bLen >= aLen);
}

//Example with use of an ordinal
public static bool StartsWithOrdinal(string b, StringComparsion){
    a.Startswith(b, StringComparsion.Ordinal);  
}
```

## Related rules

[BP0006: Ordinal String Comparison](https://github.com/OliverAmstutz/Best-Practice-Checker/tree/main/Docs/References/BP0006_OrdinalStringComparison.md)

## References
<a id="1">[1]</a>
Unity, 27. Oktober 2020 Strings und Text<br/>
Accessed 29. Oktober from https://docs.unity3d.com/Manual/BestPracticeUnderstandingPerformanceInUnity5.html
