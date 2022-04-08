# 1. EditorJsonUtility

Date: 2022-03-28

## Status

Accepted

## Context

Unity provides two json utility classes - one for editors and one for runtime. 
For some reason, `EditorJsonUtility.FromJsonOverwrite()` is not functioning properly.

## Decision

Despite its intention, `JsonUtility.FromJson<>()` is used instead of `EditorJsonUtility.FromJsonOverwrite()`.

## Consequences

Usage of Unity class againts its design. Might bite its tail at some point.

#### Example

Setup:
```C#
private void OnEnable()
        {
            Debug.Log(JsonUtility.FromJson<Vector2>(EditorPrefs.GetString(ClassKey + ObjectKey + ScrollVarKey,
                EditorJsonUtility.ToJson(new Vector2(0.0f, 0.0f)))));
            EditorJsonUtility.FromJsonOverwrite(
                EditorPrefs.GetString(ClassKey + ObjectKey + ScrollVarKey,
                    EditorJsonUtility.ToJson(new Vector2(0.0f, 0.0f))), _scrollPosition);
            Debug.Log("CompoundControls OnEnable() called with _scrollPosition: " + _scrollPosition);
            //code omitted
        }
```

Console Output:

`
GetStringFromEditorPrefs: {"x":32.0,"y":0.0}`

`(32.0, 0.0)`

`CompoundControls OnEnable() called with _scrollPosition: (0.0, 0.0)
`