using BestPracticeChecker.Editor.BusinessLogic.Persistor;
using BestPracticeChecker.Editor.BusinessLogic.WebAccess;
using BestPracticeChecker.Editor.UI.BestPractices;
using NUnit.Framework;
using UnityEngine;

namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices
{
    public sealed class BestPracticeFactory : ScriptableObject
    {
        private BestPracticeFactory()
        {
            throw new AssertionException("No instantiation intended");
        }

        public static IBestPractice Create<T>(BestPracticeName bpName) where T : BestPractice
        {
            return Create<T>(bpName, new Persistor.Persistor(), new WebAccess.WebAccess(), null,
                new ResultEditorFactory());
        }


        public static IBestPractice Create<T>(BestPracticeName bpName, IPersistor persistor,
            IWebAccess webAccess, IBusinessLogic<IResult> businessLogic, IResultEditorFactory resultEditorFactory)
            where T : BestPractice
        {
            var bp = CreateInstance<T>();
            bp.Name(bpName);
            bp.Init(persistor, webAccess, businessLogic, resultEditorFactory);
            return bp;
        }
    }
}