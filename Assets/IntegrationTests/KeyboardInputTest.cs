using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace IntegrationTests
{
    public class KeyboardInputTest : SceneTestFixture
    {
        private readonly InputPressed _inputPressed = new InputPressed();

        [SetUp]
        public void Prepare()
        {
            _inputPressed.PrepareInputSystem();
        }

        [TearDown]
        public void Close()
        {
            _inputPressed.CloseInputSystem();
        }

        [UnityTest]
        public IEnumerator ShouldTriggerTurnRight()
        {
            yield return Given.Scene(this, "MainScene");
            var tron = Find.SingleObjectById("Tron");
            var tronTransform = tron.transform;
            var racingInteraction = tron.AddComponent<RacingInteractionMock>();

            tronTransform.GetComponent<Tron>().racingInteraction = racingInteraction;

            // TODO duplication: We have lots of calls like this. Maybe have Given.RaceStarted()
            tronTransform.GetComponent<Tron>().StartRace();
            yield return new WaitForEndOfFrame();

            yield return _inputPressed.Right();

            Assert.That(racingInteraction.TurnRightHasBeenCalled());
        }

        [UnityTest]
        public IEnumerator DoesntTurnRightWhenGameHasNotStarted()
        {
            yield return Given.Scene(this, "MainScene");

            var tron = Find.SingleObjectById("Tron");
            var tronTransform = tron.transform;
            var racingInteraction = tron.AddComponent<RacingInteractionMock>();
            tronTransform.GetComponent<Tron>().racingInteraction = racingInteraction;
            // NO tronTransform.GetComponent<Tron>().StartRace();
            yield return new WaitForEndOfFrame();

            yield return _inputPressed.Right();

            Assert.That(racingInteraction.TurnRightHasBeenCalled(), Is.False);
        }

        // TODO: ignore 0/0 move events
    }

    class RacingInteractionMock : RacingInteraction
    {
        private bool _turnRightHasBeenCalled;

        public bool TurnRightHasBeenCalled()
        {
            return _turnRightHasBeenCalled;
        }

        public override void TurnRight()
        {
            _turnRightHasBeenCalled = true;
        }

        public override void FixedUpdate()
        {
        }
    }
}