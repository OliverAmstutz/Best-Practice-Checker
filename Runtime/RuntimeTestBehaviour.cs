using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestPracticeChecker.Runtime
{
    public class RuntimeTestBehaviour : MonoBehaviour
    {
        public static void MoveGameObjectInX(GameObject go, float direction)
        {
            go.transform.position += new Vector3(direction, 0, 0);
        }
    }
}

