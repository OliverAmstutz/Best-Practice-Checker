# BP0020: Non Allocating Physics

## Cause

One of the following methods from the Physics Module was called.

[Physics](https://docs.unity3d.com/2019.4/Documentation/ScriptReference/Physics.html):
  - `BoxCast`
  - `CapsuleCast`
  - `OverlapBox`
  - `OverlapCapsule`
  - `OverlapSphere`
  - `Raycast`
  - `SphereCast`

[Physics2D](https://docs.unity3d.com/ScriptReference/Physics2D.html):
  - `BoxCast`
  - `CapsuleCast`
  - `CircleCast`
  - `GetRayIntersection`
  - `Linecast`
  - `OverlapArea`
  - `OverlapBox`
  - `OverlapCapsule`
  - `OverlapCircle`
  - `OverlapPoint`
  - `Raycast`
  
## Rule description

With the introduction of Unity 5.3, these methods should no longer be used as they allocate memory.
Memory allocation should be done by the user.

## How to fix violations

The methods used should be used with their `NonAlloc` alternative. [[1]](#1).

[Physics](https://docs.unity3d.com/2019.4/Documentation/ScriptReference/Physics.html):
  - `BoxCastNonAlloc`
  - `CapsuleCastNonAlloc`
  - `OverlapBoxNonAlloc`
  - `OverlapCapsuleNonAlloc`
  - `OverlapSphereNonAlloc`
  - `RaycastNonAlloc`
  - `SphereCastNonAlloc`

[Physics2D](https://docs.unity3d.com/ScriptReference/Physics2D.html):
  - `BoxCastNonAlloc`
  - `CapsuleCastNonAlloc`
  - `CircleCastNonAlloc`
  - `GetRayIntersectionNonAlloc`
  - `LinecastNonAlloc`
  - `OverlapAreaNonAlloc`
  - `OverlapBoxNonAlloc`
  - `OverlapCapsuleNonAlloc`
  - `OverlapCircleNonAlloc`
  - `OverlapPointNonAlloc`
  - `RaycastNonAlloc`

## When to suppress warnings

Never

## Example of a violation

### Description

An allocating method was called

### Code

```csharp
BoxCast(...);
```

## Example of how to fix

### Description

The call has been replaced with the `NonAlloc` alternative.
Be sure to check the required parameters of the NonAlloc methods to be used!

### Code

```csharp
BoxCastNonAlloc(...);
```

## Related rules

None

## References

<a id="1">[1]</a>
Unity Technologies, 16. September 2020, General Optimizations. <br /> 
Accessed 20. September 2020 from https://docs.unity3d.com/2020.2/Documentation/Manual/BestPracticeUnderstandingPerformanceInUnity7.html