using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Assert = UnityEngine.Assertions.Assert;

namespace Tests
{
    public class ExampleTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void FailingTest()
        {
            Assert.AreEqual(true, false, "True should equal false?");
        }

        /* Keeping the UnityTest example, but usually it is not required for simple test scenarios.
         See description here:  https://docs.unity3d.com/Packages/com.unity.test-framework@1.1/manual/reference-attribute-unitytest.html

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator ExampleTestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }

        */
    }
}
