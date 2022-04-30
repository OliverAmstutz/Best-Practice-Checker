# BP0008: Use small classes

## Cause

A class exceeds 100 lines of code.

## Rule description

Classes with more than 100 lines of code often argue for refactoring the class.
Following the Single Responsibilty Principle (SRP)[[2]](*2) and the statement of Robert Martin (Uncle Bob):
"There should never be more than one reason to change a class" [[1]](*1),
it can be assumed that the class has optimisation potential.

## How to fix violations

Use of small, but many classes that adhere to the following principles [[3]](*3):

* Each module or class should have the responsibility of a single part of a software
* The responsibility of the class should be completely encapsulated within the class
* The responsibilities of all services should be closely aligned to

The resulting advantages are in particular[[3]](#3):

* Readability
* Extensibility
* Reusability / Increases modularity

## When to suppress warnings

Never

## Example of how to fix

### Description

Make a design revision of the class based on the SRP pattern.

## Related rules

None

## References

<a id="1">[1]</a>
Wikipedia, 21. September 2020, Principles of object-oriented design.<br />
Accessed 23. September 2020 from https://de.wikipedia.org/wiki/Prinzipien_objektorientierten_Designs

<a id="2">[2]</a>
Unity Connect, 16. September 2020, SOLID Principles in Unity.<br />
Accessed 23. September 2020 from https://connect.unity.com/p/solid-principles-in-unity-part-2-single-responsibility-principle

<a id="3">[3]</a>
HSLU GAMEDEV, Fall semester 2019, Game Architecture S.19<br />
https://drive.google.com/file/d/14pu-zxf7NAG8-nKgfBrdSwbPRTU-MPrx/view?usp=sharing