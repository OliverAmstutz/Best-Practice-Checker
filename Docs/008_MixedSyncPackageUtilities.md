# 1. InstanceID

Date: 2022-05-19

## Status

Accepted

## Context

Unity's package operations are asynchronous. The PackageUtility component utilises said components.
The use of package utility and the user interaction does not require asynchronous operation.

## Decision

To keep the software simple and readable, the package utilities are synchronously implemented. 
The initialisation of the package utility class, the only measurable operation benefiting from the provided asynchronous approach, is still performed asynchronously.

## Consequences

This hybrid approach lets package utilities be performed in serial, but initialises the packages asynchronously.
This has the advantage of no identifiable loading times, while the program workflow is easy to understand and simple to follow.
