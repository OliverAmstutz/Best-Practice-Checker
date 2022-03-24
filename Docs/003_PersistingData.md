# 1. Persisting Data
Date: 2022-03-17

## Status

Open

## Context

In order to save editor results, two possibilities are available:
- [EditorPrefs](https://docs.unity3d.com/ScriptReference/EditorPrefs.html)
- [Json to Textfile to Json](https://docs.unity3d.com/ScriptReference/EditorJsonUtility.html)

Both options are feasible, prototyping both variants to see which is easier readable and extendable.
It is even possible to combine the two by converting more complicated objects to strings by JSON Utility and back when reading it.

Difficulty when writing in EditorPrefs:
- Per best practice at least one string

Difficulty when writing in file:
- concurrent result generation requires buffer for file to write to.

[Source](https://github.com/edwardrowe/unity-custom-tool-example) of save system example.

## Decision

KISS principle applied - the implementation effort is lower using the EditorPrefs.
In addition, concurrency is handled naturally by editor prefs.

## Consequences

Quite a lot of Registry entries in:
- Windows: `HKCU\Software\Unity Technologies\UnityEditor 5.x`
- MacOS: `~/Library/Preferences/com.unity3d.UnityEditor5.x.plist`

