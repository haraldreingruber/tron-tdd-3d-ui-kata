using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace IntegrationTest
{
    public class TronTest : SceneTestFixture
    {
        [UnityTest]
        public IEnumerator TestTronIsOnGridOnStartup()
        {
            yield return Given.Scene(this, "MainScene");

            const string objectId = "Tron";
            var tron = Find.SingleObjectById(objectId);
            AssertThat.IsVisible(tron, objectId);
            AssertIsTripleAsHigh(tron, objectId);
//          assertThatSize(tron, ?);
//          assertThatPosition(tron, ?);
        }

        private static void AssertIsTripleAsHigh(GameObject gameObject, string objectId)
        {
            var visualization = gameObject.transform.GetComponent<Tron>().visualization;
            var scale = visualization.transform.localScale;
            var width = scale.x;
            var depth = scale.z;
            var height = scale.y;

            Assert.That(width, Is.EqualTo(depth));
            Assert.That(height, Is.EqualTo(width*3));
        }

        /*
        [UnityTest]
        public IEnumerator TestTronStartsRacing()
        {
            yield return GivenScene(this, "MainScene");

            StartRace();
            // yield return new WaitForEndOfFrame();

            const string objectId = "Tron";
            var tron = findSingleObjectById(objectId);
//            assertThat(tron, isDrving);
        }
        */
    }
}