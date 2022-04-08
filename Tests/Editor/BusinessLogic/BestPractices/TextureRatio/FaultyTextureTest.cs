using BestPracticeChecker.Editor.BusinessLogic.BestPractices.TextureRatio;
using NUnit.Framework;
using UnityEngine;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.BestPractices.TextureRatio
{
    public class FaultyTextureTest
    {
        [Test]
        public void TestEqualsTrue()
        {
            var texture = new Texture2D(2, 2, TextureFormat.ARGB32, false);
            var faulty1 = new FaultyTexture(texture, true);
            var faulty2 = new FaultyTexture(texture, true);
            Assert.IsTrue(faulty1.Equals(faulty2));
        }

        [Test]
        public void TestEqualsFalseNonEqual()
        {
            var faulty1 = new FaultyTexture(new Texture2D(2, 2, TextureFormat.ARGB32, false), true);
            var faulty2 = new FaultyTexture(new Texture2D(2, 2, TextureFormat.ARGB32, false), true);
            Assert.IsFalse(faulty1.Equals(faulty2));
        }

        [Test]
        public void TestEqualsFalseNotType()
        {
            var faulty1 = new FaultyTexture(new Texture2D(2, 2, TextureFormat.ARGB32, false), true);
            Assert.IsFalse(faulty1.Equals(new Texture2D(2, 2, TextureFormat.ARGB32, false)));
        }

        [Test]
        public void TestTexture()
        {
            var texture = new Texture2D(2, 2, TextureFormat.ARGB32, false);
            var faulty = new FaultyTexture(texture, true);
            Assert.IsTrue(faulty.Texture().Equals(texture));
        }

        [Test]
        public void TestIsCompressed()
        {
            var isCompressed = false;
            var faulty = new FaultyTexture(new Texture2D(2, 2, TextureFormat.ARGB32, false), isCompressed);
            Assert.IsTrue(faulty.IsUnCompressed().Equals(isCompressed));
        }

        [Test]
        public void TestHashCode()
        {
            var texture = new Texture2D(2, 2, TextureFormat.ARGB32, false);
            var faulty1 = new FaultyTexture(texture, false);
            var faulty2 = new FaultyTexture(texture, false);
            Assert.IsTrue(faulty1.GetHashCode().Equals(faulty2.GetHashCode()));
        }
    }
}