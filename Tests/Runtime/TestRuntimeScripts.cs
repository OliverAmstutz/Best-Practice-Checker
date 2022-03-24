using System.Collections;
using BestPracticeChecker.Runtime;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestRuntimeScripts
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestPlayModeTest()
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(0, 0.5f, 0);
        RuntimeTestBehaviour.MoveGameObjectInX(cube, 1f);
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
        Assert.AreEqual(cube.transform.position, new Vector3(1f, 0.5f, 0f));
    }
}