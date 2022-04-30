# BP0004: Constant Strings

## Cause

You have called a method that requires one or more tags as parameters.
The tags were not passed as constants.

## Rule description

If tags are not passed as constants,
there is more often the possibility of mistyping,
there is no auto completion. 
Furthermore, it is more difficult to change them again [[1]]. 
This can lead to unwanted `NullPointerExceptions` at runtime.

## How to fix violations

By using constants with the `const` keyword, strings can no longer be changed and stored centrally, for example. 
This alleviates the problem to a certain extent, but not completely.

## When to suppress warnings

If the tags are passed variably and it can be ensured that the tags definitely exist.

## Example of a violation

### Description

In this example, the tag `Respawn` was stored in a non-constant variable and used to call `FindWithTag`.

### Code

```csharp
public class Something : MonoBehaviour
{
    void Start()
    {
        var tag = "Respawn";
        var g = gameObject.FindWithTag(tag)
    }
}
```

## Example of how to fix

### Description

The tags are stored as constants in the static class 'Tags'. They can be managed centrally here.

### Code

```csharp
public static class Tags
{
    public const string Respawn = "Respawn";
}

public class Something : MonoBehaviour
{
    void Start()
    {
        var g = gameObject.FindWithTag(Tags.Respawn)
    }
}
```

## Related rules

None

## References
Devin Reimer, 11. März 2020, Tags, Layers and Scene Constants Generator in Unity. <br />
Accessed 10. Oktober 2020 from http://blog.almostlogical.com/2014/03/11/tags-layers-and-scene-constants-generator-in-unity/
