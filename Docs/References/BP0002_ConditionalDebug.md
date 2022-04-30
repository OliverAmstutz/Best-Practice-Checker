# BP0002: Conditional Debug

## Cause

A method for outputting logs of the class `UnityEngine.Debug` was called.

## Rule description

The `UnityEngine.Debug` [Debugging API](https://docs.unity3d.com/ScriptReference/Debug.html) calls are not removed from release builds and write to log files [[1]](#1).
Debug information is not normally output in release builds.

## How to fix violations

To prevent debugging API calls from being output in release builds, they can be provided with a [conditional attribute](https://docs.microsoft.com/de-de/dotnet/api/system.diagnostics.conditionalattribute?redirectedfrom=MSDN&view=netcore-3.1).

For this, a wrapper method must be created in a class declaration or a structure declaration, with the return value `void` [[2]](#2).

With the conditional attribute, if the corresponding preprocessing identifier is not set, the method will be ignored or omitted in the release build.

To set the preprocessing identifier in debug builds, it can be set globally under `<project path>/Assets/mcs.rsp` with `-define:<preprocessing identifier>`.
One of the already existing preprocessing identifiers can also be used, such as `UNITY_EDITOR` or `DEVELOPMENT_BUILD` [[3]](#3).
Furthermore, preprocessing identifiers can also be set directly in the code.

**Beispiel**:
```csharp
#if (UNITY_EDITOR || DEVELOPMENT_BUILD)
#define ENABLE_LOGS
#endif 
```

An introduction to preprocessing commands and preprocessing identifiers can be found on [Unity Learn](https://learn.unity.com/tutorial/introduction-to-preprocessing-commands#).

## When to suppress warnings

This should only be suppressed if debugging logs are also to be output in release builds.

## Example of a violation

### Description

In the `Start` method, the following is logged in any case:
> Just started

### Code

```csharp
public class Something : MonoBehaviour
{
    void Start() 
    {
        UnityEngine.Debug.Log("Just started");
    }
}
```

## Example of how to fix

### Description

For the solution, a utility class can be used, whereby the methods of these are provided with a `Conditional` attribute.

### Code
```csharp
public static class DebugLogger 
{
    
    [Conditional("ENABLE_LOGS")]
    public static void Log(string message) 
    {
        UnityEngine.Debug.Log(message); 
    }
}
```

## Related rules

None

## References
<a id="1">[1]</a>
Unity Technologies, 16. September 2020, General Optimizations. <br /> 
Accessed 20. September 2020 from https://docs.unity3d.com/2020.2/Documentation/Manual/BestPracticeUnderstandingPerformanceInUnity7.html

<a id="2">[2]</a>
Microsoft, 3. September 2020, Conditional-Attribute (C#-Programmer handbook). <br />
Accessed 20. September 2020 from https://docs.microsoft.com/de-ch/previous-versions/visualstudio/visual-studio-2008/4xssyw96(v=vs.90)?redirectedfrom=MSDN#hinweise

<a id="3">[3]</a>
Unity Technologies, 17. September 2020, Platform dependent compilation. <br />
Accessed 20. September 2020 from https://docs.unity3d.com/2020.2/Documentation/Manual/PlatformDependentCompilation.html?_ga=2.192344162.995033292.1600604413-1679067612.1600330815
