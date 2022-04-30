# BP0001: Methods to avoid in Update

## Cause

One of the following methods was called in the `Update` method:
- `GetComponent`
- GetComponents
- FindObjectOfType

## Rule description

Since calls to the `GetComponent`, `GetComponents`,
`FindObjectsOfType` and `FindObjectOfType` methods are very computationally intensive,
accessing other GameObjects should never be done via these methods in the `Update` method.
This is because the referencing is called again with every frame, which uses up resources unnecessarily.

## How to fix violations

Instead of making such a call in the `Update`, such calls should be made in the `Start` or `OnEnable` method.
From there, the reference can be cached and accessed in the `Update` method.

Alternatively, a manager class can be used that offers or injects the reference.

## When to suppress warnings

Never

## Example of a violation

### Description

The `GetComponent` method is called in the `Update` method to get a `HingeJoint`.

### Code

```csharp
class Something : MonoBehaviour
{
    HingeJoint _HingeJoint;

    void Update()
    {
        HingeJoint hinge = gameObject.GetComponent(typeof(HingeJoint)) as HingeJoint;

        // Do something
    }
} 
```

## Example of how to fix

### Description

The `GetComponent` method is called in the `Start` method and the return is cached for use in the `Update` method.
### Code

```csharp
class Something : MonoBehaviour
{
    HingeJoint _HingeJoint;

    void Start()
    {
        _HingeJoint = gameObject.GetComponent(typeof(HingeJoint)) as HingeJoint;

        // Do something
    }

    void Update()
    {
        // Do somethig with _HineJoint
    }
} 
```

## Related rules

[BP0023: Main Camera In Update][1]

## References
<a id="1">[1]</a>
Richard Wetzel, Fall semester 2019, Game Architecture<br/>
Accessed 22. September 2020