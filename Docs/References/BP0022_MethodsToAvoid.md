# BP0022: Methods to avoid

## Cause

One of the following methods was called:
  - `UnityEngine.GameObject.Find`
  - `UnityEngine.Object.FindObjectOfType`

## Rule description

Calls to the `Find` and `FindObjectsOfType` methods should generally be avoided in the code.
Since these APIs iterate over all GameObjects used in Unity,
the increasing size of projects is accompanied by performance problems [[1]](#1).
An exception to the above rules can be made when referencing manager classes (singleton) using the `FindObjectOfTyype` - API.

## How to fix violations

It is not necessary to use the methods mentioned. 
Exceptions are the referencing of manager classes in `Awake` or `Start`. Never use in `Update`!

## When to suppress warnings

Never

## Example of a violation

### Description

The method `GameObject.Find` is called in a method to find an object called `Hand`.
In the above example, only active `GameObjects` are returned. If no `GameObject` with name can be found, `null` is returned.
This also only works in the same `GameObject` or hierarchy. If the name contains a '/' character, it will traverse the hierarchy like a pathname.
The method call is very slow and should therefore never be used in the `Update` method.
Furthermore, only the first `GameObject` found is output in each case. If a scene contains several `GameObjects` with the same name, there is no guarantee that a specific `GameObject` will be returned.

Calling `GameObject.Find.Find` does not perform a recursive descent in a hierarchy.

Translated with www.DeepL.com/Translator (free version)

### Code

```csharp
using UnityEngine;

public class ExampleClass : MonoBehaviour
{
    public GameObject hand;

    void Example()
    {
        hand = GameObject.Find("Hand");
    }

}
```

## Example of how to fix

### Description
Whenever possible, the call to the methods
- `UnityEngine.GameObject.Find`
- `UnityEngine.Object.FindObjectOfType` methods.

should be avoided.

## Related rules

[BP0001: Methods to avoid in Update](https://github.com/emanuelbuholzer/unity-best-practices/blob/master/docs/reference/BP0001_MethodsToAvoidInUpdate.md)

## References

<a id="1">[1]</a>
Unity Technologies, 16. September 2020, General Optimizations. <br /> 
Accessed 20. September 2020 from https://docs.unity3d.com/2020.2/Documentation/Manual/BestPracticeUnderstandingPerformanceInUnity7.html
