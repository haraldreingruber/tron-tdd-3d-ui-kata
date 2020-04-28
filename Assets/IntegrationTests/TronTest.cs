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
         * TODO Test /Feature list
         * - button disappears after click
         * - camera follows the racer
         * - leaves walls behind
         * - change direction (racer and walls)
         * - boundary walls
         * - collision/death/game over
         * - score
         * - AI enemies
         * TODO review all tests if we could get away without UI?
         */

        /*
         * Idea:
         * we start with a TrailInteraction
         * - knows current trail wall behind, creates it and such knows it
         * - creates new wall behind us length 0 from prefab, different game object
         *   - wall is half thickness, full height, different colour (styling, no test)
         * - when move the wall gets longer
         *   - holt distanz von seinem game object seit letztem update und update its current trail
         *
         * Tests
         * - startRace = new instance -> there is trail instance length 0 at fixed coordinates
         * - update -> length is increased by x (unit)
         */

        [UnityTest]
        public IEnumerator LeavesTrailWhenStartRacing()
        {
            yield return Given.Scene(this, "MainScene");

            var tron = Find.SingleObjectById("Tron");
            var tronTransform = tron.transform;
            var originalPosition = tronTransform.position;

            tronTransform.GetComponent<Tron>().StartRace();
            yield return new WaitForEndOfFrame();

            // TODO assert there is a wall behind us:
            // z == originalPosition.z + 0.5
            // y == originalPosition.y + 0
            // x == originalPosition.x + 0
            // width == tron.width * 0.5
            // height == tron.height * 1
            // length == <= 0.02
        }

    }
}