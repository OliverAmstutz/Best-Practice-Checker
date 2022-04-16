using System.Collections.Generic;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices;
using BestPracticeChecker.Editor.BusinessLogic.Persistor;
using BestPracticeChecker.Editor.BusinessLogic.WebAccess;
using NUnit.Framework;
using UnityEngine;

namespace BestPracticeChecker.Editor.UI.BestPractices
{
    public sealed class BestPracticeEntryFactory : ScriptableObject
    {
        private BestPracticeEntryFactory()
        {
            throw new AssertionException("No instantiation intended");
        }

        public static IEnumerable<BestPracticeEntry> CreateAll()
        {
            return new List<BestPracticeEntry>
            {
                Create<BusinessLogic.BestPractices.TestFramework.TestFramework>(BestPracticeName.Test_Framework),
                Create<BusinessLogic.BestPractices.TextureRatio.TextureRatio>(BestPracticeName.Texture_Ratio),
                Create<BusinessLogic.BestPractices.SourceControl.SourceControl>(BestPracticeName.Source_Control),
                Create<BusinessLogic.BestPractices.AudioFormat.AudioFormat>(BestPracticeName.Audio_Format),
                Create<BusinessLogic.BestPractices.NoAssetsInRoot.NoAssetsInRoot>(BestPracticeName.No_Assets_In_Root),
                Create<BusinessLogic.BestPractices.CodeCoverage.CodeCoverage>(BestPracticeName.Code_Coverage)
            };
        }

        private static BestPracticeEntry Create<T>(BestPracticeName bpName) where T : BestPractice
        {
            return Create<T>(bpName, new Persistor(), new WebAccess(), null, new ResultEditorFactory());
        }

        public static BestPracticeEntry Create<T>(BestPracticeName bpName,
            IPersistor persistor, IWebAccess webAccess, IBusinessLogic<IResult> businessLogic,
            IResultEditorFactory resultEditorFactory) where T : BestPractice
        {
            var bp = BestPracticeFactory.Create<T>(bpName, persistor, webAccess, businessLogic, resultEditorFactory);
            var bpe = CreateInstance<BestPracticeEntry>();
            bpe.Init(bp, bp.GetName().ToString(), persistor);
            return bpe;
        }
    }
}