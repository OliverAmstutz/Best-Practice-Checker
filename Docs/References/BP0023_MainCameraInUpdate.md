# BP0023: Main Camera in Update

## Cause

The property `main` found on the class `Camera` was called in an `Update` method.

## Rule description

The `main` property found on the `Camera` class uses `Object.FindObjectWithTag` internally.
Therefore, a call on this property is no more efficient than a call on `Object.FindObjectWithTag`.
Such calls should never be made in the `Update` method because they are computationally intensive.

## How to fix violations

Instead of making such a call in the `Update`, the call should be made in the `Start` or `OnEnable` method.
From there, the reference can be cached and accessed in the `Update` method.

Alternatively, a `Camera` manager class can be used, which offers or injects the reference.

## When to suppress warnings

Never

## Example of a violation

### Description

The `main` property found on the `Camera` class is called in the `Update` method.

### Code

```csharp
class Something : MonoBehaviour
{
    Camera _MainCamera;

    void Update()
    {
        _MainCamera = Camera.main;

        // Do something
    }
}
```

## Example of how to fix

### Description

The `main` property found on the `Camera` class is cached in the `Start` method, for use in the `Update` method.

### Code

```csharp
class Something : MonoBehaviour
{
    Camera _MainCamera;

    void Start()
    {
        _MainCamera = Camera.main;

        // Do something
    }

    void Update() 
    {
        // Do something with _MainCamera
    }
}
```

## Related rules

[BP0001: Methods to Avoid in Update](https://github.com/OliverAmstutz/Best-Practice-Checker/tree/main/Docs/References/BP0001_MethodsToAvoidInUpdate.md)

## References
<a id="1">[1]</a>
Unity Technologies, 06. Mai 2020, General Optimizations. <br /> 
Accessed 3. November 2020 from https://docs.unity3d.com/2019.3/Documentation/Manual/BestPracticeUnderstandingPerformanceInUnity7.html
