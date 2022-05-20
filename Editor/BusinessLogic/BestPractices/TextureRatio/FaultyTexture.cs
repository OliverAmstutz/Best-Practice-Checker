using System;
using UnityEngine;

namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices.TextureRatio
{
    [Serializable]
    public class FaultyTexture
    {
        [SerializeField] private bool isUnCompressed;
        [SerializeField] private string texturePath;
        private bool _isDirty;

        public FaultyTexture(string texturePath, bool isUnCompressed)
        {
            this.texturePath = texturePath;
            this.isUnCompressed = isUnCompressed;
        }

        public string TexturePath()
        {
            return texturePath;
        }

        public bool IsUnCompressed()
        {
            return isUnCompressed;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is FaultyTexture faultyTexture)) return false;

            return texturePath.Equals(faultyTexture.texturePath) && isUnCompressed.Equals(faultyTexture.isUnCompressed);
        }

        public override int GetHashCode()
        {
            var hash = 17;
            hash = hash * 31 + texturePath.GetHashCode();
            hash = hash * 31 + isUnCompressed.GetHashCode();
            return hash;
        }

        public void SetDirty()
        {
            _isDirty = true;
        }

        public bool IsDirty()
        {
            return _isDirty;
        }
    }
}