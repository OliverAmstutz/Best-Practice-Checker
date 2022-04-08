using BestPracticeChecker.Editor.BusinessLogic.Persistor;
using NUnit.Framework;
using UnityEngine;

namespace BestPracticeChecker.Editor.UI
{
    public sealed class CompoundControlsFactory : ScriptableObject
    {
        private CompoundControlsFactory()
        {
            throw new AssertionException("No instantiation intended");
        }

        public static CompoundControls Create()
        {
            var cc = CreateInstance<CompoundControls>();
            cc.Init();
            return cc;
        }

        public static CompoundControls Create(IPersistor persistor)
        {
            var cc = CreateInstance<CompoundControls>();
            cc.Init(persistor);
            return cc;
        }
    }
}