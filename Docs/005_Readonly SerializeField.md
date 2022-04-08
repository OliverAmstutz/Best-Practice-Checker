# 1. Readonly SerializeField

Date: 2022-04-01

## Status

Accepted

## Context

Unity's JsonUtility and JsonUtilityEditor class does not serialize `readonly` marked fields. 

## Decision

In order to serialize FaultyTexture class, the `isUnCompressed` and `texture` fields need to be non final.
The hashCode with mutable fields is acceptable, as no Dictionary or HashTable is used with this object.

## Consequences

"GetHashCode" should not reference mutable fields. 
GetHashCode is used to file an object in a Dictionary or Hashtable. 
If GetHashCode uses non-readonly fields and those fields change after the object is stored, the object immediately becomes mis-filed in the Hashtable.
Any subsequent test to see if the object is in the Hashtable will return a false negative.

#### Example

```C#
    [Serializable]
    public class FaultyTexture
    {
        [SerializeField] private bool isUnCompressed;
        [SerializeField] private Texture texture;

        public FaultyTexture(Texture texture, bool isUnCompressed)
        {
            this.texture = texture;
            this.isUnCompressed = isUnCompressed;
        }

        public Texture Texture()
        {
            return texture;
        }

        public bool IsUnCompressed()
        {
            return isUnCompressed;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is FaultyTexture faultyTexture)) return false;

            return texture.Equals(faultyTexture.texture) && isUnCompressed.Equals(faultyTexture.isUnCompressed);
        }

        public override int GetHashCode()
        {
            var hash = 17;
            hash = hash * 31 + texture.GetHashCode();
            hash = hash * 31 + isUnCompressed.GetHashCode();
            return hash;
        }
    }
```
