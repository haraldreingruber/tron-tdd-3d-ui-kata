using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace IntegrationTests
{
    public class KeyboardInputTest : SceneTestFixture
    {
        private readonly InputSimulation _inputSimulation = new InputSimulation();

        [SetUp]
        public void Prepare()
        {
            _inputSimulation.Prepare();
        }

        [TearDown]
        public void Close()
        {
            _inputSimulation.Close();
        }

        [UnityTest]
        public IEnumerator ShouldTriggerTurnRight()
        {
            yield return Given.Scene(this, "MainScene");
            var tron = Find.SingleObjectById("Tron");

            // TODO duplication: extract mock creation to helper method
            var tronTransform = tron.transform; // TODO: do we need transform? direct?
            var racingInteraction = tron.AddComponent<RacingInteractionSpy>();
            tronTransform.GetComponent<Tron>().racingInteraction = racingInteraction;

            // TODO duplication: We have lots of calls like this. Maybe have Given.RaceStarted()
            // need also var tronTransform = tron.transform;
            tronTransform.GetComponent<Tron>().StartRace();
            yield return new WaitForEndOfFrame();

            yield return _inputSimulation.PressRight();

            // TODO duplication: extract mock verification to helper methods
            Assert.That(racingInteraction.TurnRightHasBeenCalled());
        }

        [UnityTest]
        public IEnumerator DoesntTurnRightWhenGameHasNotStarted()
        {
            yield return Given.Scene(this, "MainScene");

            var tron = Find.SingleObjectById("Tron");
            var tronTransform = tron.transform;
            var racingInteraction = tron.AddComponent<RacingInteractionSpy>();
            tronTransform.GetComponent<Tron>().racingInteraction = racingInteraction;
            // NO tronTransform.GetComponent<Tron>().StartRace();
            yield return new WaitForEndOfFrame();

            yield return _inputSimulation.PressRight();

            Assert.That(racingInteraction.TurnRightHasBeenCalled(), Is.False);
        }

        // TODO next test: ignore 0/0 move events
    }

    class RacingInteractionSpy : RacingInteraction
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