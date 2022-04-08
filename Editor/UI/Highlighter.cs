using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace BestPracticeChecker.Editor.UI
{
    public class Highlighter : MonoBehaviour
    {
        private Highlighter()
        {
            throw new AssertionException("No instantiation intended");
        }
        
        public static void HighlightObject(Object obj)
        {
            EditorGUIUtility.PingObject(obj);
            Selection.activeObject = obj;
        }
    }
}