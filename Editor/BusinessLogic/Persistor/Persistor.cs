using System;
using UnityEditor;

namespace BestPracticeChecker.Editor.BusinessLogic.Persistor
{
    public class Persistor : IPersistor
    {
        public void Save<T>(string key, T value)
        {
            switch (value)
            {
                case bool boolValue:
                    EditorPrefs.SetBool(key, boolValue);
                    break;
                case float floatValue:
                    EditorPrefs.SetFloat(key, floatValue);
                    break;
                case int intValue:
                    EditorPrefs.SetInt(key, intValue);
                    break;
                case string stringValue:
                    EditorPrefs.SetString(key, stringValue);
                    break;
                default:
                    throw new ArgumentException("Type " + value.GetType() + " not valid");
            }
        }

        public T Load<T>(string key, T defaultValue)
        {
            switch (defaultValue)
            {
                case bool boolValue:
                {
                    var value = EditorPrefs.GetBool(key, boolValue);
                    return (T) (object) value;
                }
                case float floatValue:
                {
                    var value = EditorPrefs.GetFloat(key, floatValue);
                    return (T) (object) value;
                }
                case int intValue:
                {
                    var value = EditorPrefs.GetInt(key, intValue);
                    return (T) (object) value;
                }
                case string stringValue:
                {
                    var value = EditorPrefs.GetString(key, stringValue);
                    return (T) (object) value;
                }
                default:
                {
                    throw new ArgumentException("Persistor load type not supported");
                }
            }
        }
    }
}