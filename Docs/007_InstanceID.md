# 1. InstanceID

Date: 2021-04-15

## Status

Accepted

## Context

Persisting Result objects with Unity Serializer utilises Unity's Unique Instance ID. When more than one list is persisted though, the Instance ID (which Unity does not guarantee to be the same after Unity restart) can differ.
This leads either to a Null reference or a Mixup of the objects.

## Decision

The persistor class needs to provide an extra method for saving and loading Lists. 
This method does save the asset Path instead of its InstanceID.
During Load, the asset is Loaded through the `AssetDatabase.LoadAssetAtPath`.

## Consequences

Less efficient implementation as a workaround through Unity's limitations.
