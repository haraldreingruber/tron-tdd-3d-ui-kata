using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace IntegrationTests
{
    public class TronTest : SceneTestFixture
    {
        [UnityTest]
        public IEnumerator IsOnGridOnStartup()
        {
            yield return Given.Scene(this, "MainScene");

            const string objectId = "Tron";
            var tron = Find.SingleObjectById(objectId);
            AssertThat.IsVisible(tron, objectId + "visible");
            AssertIsTripleAsHigh(tron, objectId);
            // not assertThatSize(tron, ?);
            // not assertThatPosition(tron, ?);
        }

        private static void AssertIsTripleAsHigh(GameObject gameObject, string objectId)
        {
            var visualization = gameObject.transform.GetComponent<Tron>().visualization;
            var scale = visualization.transform.localScale;
            var width = scale.x;
            var depth = scale.z;
            var height = scale.y;

            Assert.That(width, Is.EqualTo(depth), objectId + " width");
            Assert.That(height, Is.EqualTo(width * 3), objectId + " height");
        }

        [UnityTest]
        public IEnumerator StartsRacing()
        {
            yield return Given.Scene(this, "MainScene");
            const string objectId = "Tron";
            var tron = Find.SingleObjectById(objectId);
            var tronTransform = tron.transform;
            var originalPosition = tronTransform.position;

            tronTransform.GetComponent<Tron>().StartRace();

            yield return new WaitForSeconds(2.0f);
            // ReSharper disable once Unity.InefficientPropertyAccess - position has changed
            var newPosition = tronTransform.position;

            Assert.That(newPosition.y, Is.EqualTo(originalPosition.y));
            Assert.That(Math.Abs(newPosition.z - originalPosition.z), Is.GreaterThan(1.0f));
        }

        // TODO maybe refactor to use Tron component instead of GameObject in all places

        /*
         * Test list
         * - do not move unless start racing
         * - speed or relative movement
         * - leaves walls behind
         */
    }
}