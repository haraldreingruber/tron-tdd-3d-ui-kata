using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TestTools;
using UnityEngine.UI;
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
        public IEnumerator StartsRacingOnButtonClick()
        {
            yield return Given.Scene(this, "MainScene");

            var tron = Find.SingleObjectById("Tron");
            var tronTransform = tron.transform;
            var originalPosition = tronTransform.position;

            // was tronTransform.GetComponent<Tron>().StartRace();
            clickStartButton();

            var distance = 0.0f;
            var newPosition = originalPosition;
            var someDistance = 0.5f;
            yield return new WaitUntilOrTimeout(() =>
            {
                // ReSharper disable once Unity.InefficientPropertyAccess - position has changed
                newPosition = tronTransform.position;
                distance = Math.Abs(newPosition.z - originalPosition.z);
                return distance >= someDistance;
            }, 2.0f);

            Assert.That(newPosition.y, Is.EqualTo(originalPosition.y));
            Assert.That(distance, Is.GreaterThan(someDistance));
        }

        private void clickStartButton()
        {
            var buttonObject = Find.SingleObjectById("StartButton");
            var button = buttonObject.GetComponent<Button>();
            var buttonTransform = buttonObject.GetComponent<RectTransform>();
            //button.onClick.Invoke();

            var buttonCorners = new Vector3[4];
            buttonTransform.GetWorldCorners(buttonCorners);
            var buttonCenter = Vector3.Lerp(buttonCorners[0], buttonCorners[2], 0.5f);

            var pointer = new PointerEventData(EventSystem.current);
            ExecuteEvents.Execute(buttonObject, pointer, ExecuteEvents.pointerClickHandler);
        }

        /*
         * TODO Test list
         * - button or click starts game/racing
         * - leaves walls behind
         * TODO review all tests if we could get away without UI?
         */
    }
}