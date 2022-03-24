# 1. Result window

Date: 2022-03-17

## Status

Accepted

## Context

In order to display the versatile best practice results, a new editor window is spawned. Several types of editor windows are available:
- [Regular editor window](https://docs.unity3d.com/ScriptReference/EditorWindow.html) Create your own custom editor window that can float free or be docked as a tab, just like the native windows in the Unity interface.
- [Auxiliary editor window:](https://docs.unity3d.com/ScriptReference/EditorWindow.ShowAuxWindow.html) The single auxiliary window can be re-used by different editor windows at different times
- [Modal editor window:](https://docs.unity3d.com/ScriptReference/EditorWindow.ShowModal.html) Other windows will not be accessible and any script recompilation will not happen until this window is closed.
- [Notification editor window:](https://docs.unity3d.com/ScriptReference/EditorWindow.ShowNotification.html) Unlike message boxes or log messages notification will fade out automatically after some time.
- [Popup editor window:](https://docs.unity3d.com/ScriptReference/EditorWindow.ShowPopup.html) This means the window has no frame, and is not draggable. It is intended for showing something like a popup menu within an existing window.
- [Utility editor window:](https://docs.unity3d.com/ScriptReference/EditorWindow.ShowUtility.html) When the utility window loses focus it remains on top of the new active window. This means the EditorWindow.ShowUtility window is never hidden by the Unity editor. It is, however, not dockable to the editor.

## Decision

Selecting Auxiliary editor window. The user has only use for one result at a time, and the re-use of existing windows declutter the Unity editor window. The other features are either not particularly useful or even harmful for the intended use.

## Consequences

Only one result window can be open at a time. The default editor window implementation allows for reusing the existing window.


## Example

```c#
        private void OnGUI()
        {
            if (GUILayout.Button("Close"))
                Close();
        }

        [MenuItem("Tools/Test")]
        private static void OpenBestPracticeCheckerEditor()
        {
            GetWindow<MyEditor>("Best Practice Checker");
        }
```
