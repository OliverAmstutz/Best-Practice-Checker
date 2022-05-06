# 1. Coroutines

Date: 2022-03-11

## Status

Accepted

## Context

Concurrency within Unity is possible with Coroutines or C# Threads. Which one is most suitable?

## Decision

Coroutines are used instead of Threads. EditorCoroutines are easier to use then Threads and the code is more readable.
It is assumed that the EditorCoroutines dependency is very stable and long term maintained. 

## Consequences

Unity's EditorCoroutines dependency is necessary.
In case of loss of dependency, the editor needs to be rewritten or the EditorCoroutine component needs to be redone.

- StartCoroutine(IEnumerator, Object) = Starts an EditorCoroutine with the specified owner object. If the garbage collector collects the owner object, while the resulting coroutine is still executing, the coroutine will stop running.

- StartCoroutineOwnerless = This method starts an EditorCoroutine without an owning object. The EditorCoroutine runs until it completes or is canceled using StopCoroutine(EditorCoroutine).
