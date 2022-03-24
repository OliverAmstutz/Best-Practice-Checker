using BestPracticeChecker.Editor;
using NUnit.Framework;

public class UnitTestPlaceholder
{
    [Test]
    public void AddHappyCaseTest()
    {
        Assert.That(HelperFunctions.AddTwoValues(1, 3).Equals(4L));
    }


/*    // A Test behaves as an ordinary method
    [Test]
    public void UnitTestPlaceholderSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator UnitTestPlaceholderWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }*/
}