# 1. Use of LINQ

Date: 2022-04-07

## Status

Accepted

## Context

Unity's LINQ (functional programming) implementation is slower than a regular for loop. [Source](https://youtu.be/Xd4UhJufTx4?t=649) 

## Decision

Use LINQ nonetheless, as it is not called frequently and readability is more important than performance in the applied cases.

## Consequences

Refactor for loop to functional programming LINQ syntax.

#### Example

Regular for each loop:
```C#
             foreach (var asset in assets)
                assetsList.Add(AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(asset)));
```

LINQ syntax:
```C#
            assetsList.AddRange(assets.Select(asset => AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(asset))));
```

