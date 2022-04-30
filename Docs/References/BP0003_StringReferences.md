# BP0003: Strings References

## Cause

Methods have been called that use strings as referral parameters.

## Rule description

In general, strings should be used exclusively for displayed text[[2]](*2).
In particular, refrain from referencing with strings method calls, 
gameobjects, prefabs, etc. Animations and manager classes are an exception.
Furthermore, these methods, if already used, should not be used in update methods, 
e.g. 'Update', as this can increase the performance loss[[1]](*1).

## How to fix violations

A targeted design of the classes in compliance with the design patterns as well as 
direct method calls help to avoid the "lazy" way of referencing via strings.

## When to suppress warnings

Never

## Example of a violation

### Description

The referencing with string is to be avoided for reasons of refactoring (hardcoded) 
as well as the use of inefficient APIs (e.g. `UnityEngine.GameObject.Find`) which can lead to performance problems.

### Code

```csharp

//Avoid StartCoroutine with method name
    this.StartCoroutine("SampleCoroutine");
    
```

## Example of how to fix

### Description

Use method calls directly

### Code

```csharp

//Instead use the method directly
    this.StartCoroutine(this.SampleCoroutine());

```

## Related rules

[BP0001: Methods to avoid in Update](https://github.com/emanuelbuholzer/unity-best-practices/blob/master/docs/reference/BP0001_MethodsToAvoidInUpdate.md) <br/>
[BP0022: Methods to avoid generaly](https://github.com/emanuelbuholzer/unity-best-practices/blob/master/docs/reference/BP0022_MethodsToAvoid.md)

## References

<a id="1">[1]</a>
Rip Tutorial, 16. September 2020, Unity3d <br /> 
Accessed 20. September 2020 from https://riptutorial.com/de/unity3d/example/25290/vermeiden-sie-den-aufruf-von-methoden--die-strings-verwenden

<a id="2">[2]</a>
Dev.Mag 50 Tips for working with Unity, 16. September 2020, Tip. 34 <br />
Accessed 20. September 2020 from http://devmag.org.za/2012/07/12/50-tips-for-working-with-unity-best-practices/

