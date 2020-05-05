using System;
using System.Collections;
using NUnit.Framework;
using NUnit.Framework.Constraints;
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
            Time.timeScale = 10.0f;

            yield return Given.Scene(this, "MainScene");

            var tron = Find.SingleObjectById("Tron");
            var tronTransform = tron.transform;
            var originalPosition = tronTransform.position;

            // was tronTransform.GetComponent<Tron>().StartRace();
            ClickStartButton();

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

        private void ClickStartButton()
        {
            var buttonObject = Find.SingleObjectById("StartButton");
            var button = buttonObject.GetComponent<Button>();
            var buttonTransform = buttonObject.GetComponent<RectTransform>();
            //button.onClick.Invoke();

            var pointer = new PointerEventData(EventSystem.current);
            ExecuteEvents.Execute(buttonObject, pointer, ExecuteEvents.pointerClickHandler);
        }

        /*
         * TODO (refactor) - move Trail relevant tests to TrailTest?
         */

        [UnityTest]
        public IEnumerator CreatesTrailWhenStartRacing()
        {
            yield return Given.Scene(this, "MainScene");
            var tron = Find.SingleObjectById("Tron");

            var tronTransform = tron.transform;
            tronTransform.GetComponent<Tron>().StartRace();
            yield return new WaitForEndOfFrame();

            var trail = Find.SingleObjectById("Trail");
            AssertThat.IsVisible(trail, "Trail" + "visible");
        }

        [UnityTest]
        public IEnumerator TrailBackBorderIsCreatedAtTronBackBorder()
        {
            yield return Given.Scene(this, "MainScene");
            var tron = Find.SingleObjectById("Tron");
            var originalTronBackBorder = GeometryUtils.GetBackBorder(tron);

            var tronTransform = tron.transform;
            tronTransform.GetComponent<Tron>().StartRace();
            yield return new WaitForEndOfFrame();

            var trail = Find.SingleObjectById("Trail");
            var trailBackBorder = GeometryUtils.GetBackBorder(trail);
            Assert.That(trailBackBorder, Is.EqualTo(originalTronBackBorder), "trailBackBorder");
        }

        [UnityTest]
        public IEnumerator TrailGetsLongerDuringRace()
        {
            yield return Given.Scene(this, "MainScene");
            var tron = Find.SingleObjectById("Tron");
            var originalTronBackBorder = GeometryUtils.GetBackBorder(tron);

            var tronTransform = tron.transform;
            tronTransform.GetComponent<Tron>().StartRace();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

            var currentTronBackBorder = GeometryUtils.GetBackBorder(tron);
            var trail = Find.SingleObjectById("Trail");
            // still at same back position
            var trailBackBorder = GeometryUtils.GetBackBorder(trail);
            // TODO (testing) - assert with deldta, create Matcher for Vector3 with delta
            Assert.That(trailBackBorder, Is.EqualTo(originalTronBackBorder), "trailBackBorder");
            var trailFrontBorder = GeometryUtils.GetFrontBorder(trail);
            Assert.That(trailFrontBorder, Is.EqualTo(currentTronBackBorder), "trailFrontBorder");
        }

        // TODO 1. (continue tests) - open asserts Wall -> see the wall for fun
        // width == tron.width * 0.5
        // height == tron.height * 1
    }
}