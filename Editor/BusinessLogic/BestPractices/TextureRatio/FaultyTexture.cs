using System;
using UnityEngine;

namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices.TextureRatio
{
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
}