using System;
using System.Collections;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;
using UnityEngine.UI;
using Zenject;

namespace IntegrationTests
{
    public class TronTest : SceneTestFixture
    {
        [UnityTest]
        public IEnumerator IsInSceneOnStartup()
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
            var scale = gameObject.transform.localScale;
            var width = scale.x;
            var depth = scale.z;
            var height = scale.y;

            Assert.That(width, Is.EqualTo(depth), objectId + " width");
            Assert.That(height, Is.EqualTo(width * 3), objectId + " height");
        }

        [UnityTest]
        public IEnumerator IsOnGridOnStartup()
        {
            yield return Given.Scene(this, "MainScene");

            const string objectId = "Tron";
            var tron = Find.SingleObjectById(objectId);

            // Tron is based on plane
            Assert.That(tron.transform.position.y, Is.EqualTo(0), objectId + ".y");

            // Tron internally is aligned that its base is the bottom
            // Do we write a test for that or edit in the editor? Seems like Gui fiddeling?
            // Assert trail is internally aligned that its base is the bottom - manual

            // Grid plane is also at 0
            var grid = Find.SingleObjectById("Grid");
            Assert.That(grid.transform.position.y, Is.EqualTo(0), "grid.y");
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

            const float someDistance = 0.5f;
            var tronMoved = new ObjectMovedPredicate(tronTransform, someDistance);
            var waiter = new WaitUntilOrTimeout(tronMoved.HasMoved, 2.0f);
            yield return waiter;
            waiter.AssertTimeoutWasNotReached("move of Tron");

            var newPosition = tronTransform.position;
            Assert.That(newPosition.y, Is.EqualTo(originalPosition.y));
            Assert.That(tronMoved.CurrentDistance, Is.GreaterThan(someDistance));
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
        public IEnumerator TrailHeightIsTronHeightAndHalfWidth()
        {
            yield return Given.Scene(this, "MainScene");
            var tron = Find.SingleObjectById("Tron");

            var tronTransform = tron.transform;
            tronTransform.GetComponent<Tron>().StartRace();
            yield return new WaitForEndOfFrame();

            var trail = Find.SingleObjectById("Trail");
            Assert.That(trail.transform.localScale.y, Is.EqualTo(tronTransform.localScale.y), "trail height");
            Assert.That(trail.transform.localScale.x, Is.EqualTo(tronTransform.localScale.x/2), "trail width");
        }

        [UnityTest]
        public IEnumerator TrailBottomIsCreatedAtTronBottom()
        {
            yield return Given.Scene(this, "MainScene");
            var tron = Find.SingleObjectById("Tron");

            var tronTransform = tron.transform;
            tronTransform.GetComponent<Tron>().StartRace();
            yield return new WaitForEndOfFrame();

            var trail = Find.SingleObjectById("Trail");
            Assert.That(trail.transform.position.y, Is.EqualTo(tronTransform.position.y), "trail.y");
            // Assert trail is internally aligned that its base is the bottom - manual
        }

        [UnityTest]
        public IEnumerator TrailGetsLongerDuringRace()
        {
            yield return Given.Scene(this, "MainScene");
            var tron = Find.SingleObjectById("Tron");
            var originalTronBackBorder = GeometryUtils.GetBackBorder(tron);

            var tronTransform = tron.transform;
            tronTransform.GetComponent<Tron>().StartRace();
            var tronMoved = new ObjectMovedPredicate(tronTransform, 0.5f);
            var waiter = new WaitUntilOrTimeout(tronMoved.HasMoved, 2.0f);
            yield return waiter;
            waiter.AssertTimeoutWasNotReached("move of Tron");

            var currentTronBackBorder = GeometryUtils.GetBackBorder(tron);
            var trail = Find.SingleObjectById("Trail");
            // still at same back position
            var trailBackBorder = GeometryUtils.GetBackBorder(trail);

            var comparer = new Vector3EqualityComparer(0.0001f);
            Assert.That(trailBackBorder, Is.EqualTo(originalTronBackBorder).Using(comparer), "trailBackBorder");
            var trailFrontBorder = GeometryUtils.GetFrontBorder(trail);
            Assert.That(trailFrontBorder, Is.EqualTo(currentTronBackBorder).Using(comparer), "trailFrontBorder");
        }

        [Ignore("next next test")]
        [UnityTest]
        public IEnumerator FacesRightAfterTurnRight()
        {
            yield return Given.Scene(this, "MainScene");
            var tron = Find.SingleObjectById("Tron");
            var tronTransform = tron.transform;
            tronTransform.GetComponent<Tron>().StartRace();
            yield return new WaitForEndOfFrame();

            tronTransform.GetComponent<RacingInteraction>().TurnRight();
            yield return new WaitForEndOfFrame();

            // assert tronTransformation.rotation, bla
        }
    }
}