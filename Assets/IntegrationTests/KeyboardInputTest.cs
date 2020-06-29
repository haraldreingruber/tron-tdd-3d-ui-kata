using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TestTools;
using Zenject;

namespace IntegrationTests
{
    public class KeyboardInputTest : SceneTestFixture
    {
        // taken from: https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Testing.html
        private readonly InputTestFixture _input = new InputTestFixture();
        private Keyboard _keyboard;

        [SetUp]
        public void PrepareInputSystem()
        {
            _input.Setup();
            _keyboard = InputSystem.AddDevice<Keyboard>();
        }

        [TearDown]
        public void CloseInputSystem()
        {
            _input.TearDown();
        }

        [UnityTest]
        public IEnumerator ShouldTriggerTurnRight()
        {
            yield return Given.Scene(this, "MainScene");
            var tron = Find.SingleObjectById("Tron");
            var tronTransform = tron.transform;
            var racingInteraction = tron.AddComponent<RacingInteractionMock>();

            tronTransform.GetComponent<Tron>().racingInteraction = racingInteraction;
            
            tronTransform.GetComponent<Tron>().StartRace();
            yield return new WaitForEndOfFrame();

            _input.Press(_keyboard.dKey);
            yield return new WaitForEndOfFrame();
            // yield return new WaitForInputPress(_input).Right();

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

            _input.Press(_keyboard.dKey);
            yield return new WaitForEndOfFrame();

            Assert.That(racingInteraction.TurnRightHasBeenCalled(), Is.False);
        }

        // TODO: ignore 0/0 move events
        
    }

    public class WaitForInputPress
    {
        private readonly InputTestFixture _input;
        private readonly Keyboard _keyboard;

        public WaitForInputPress(InputTestFixture input)
        {
            _input = input;
            _keyboard = InputSystem.AddDevice<Keyboard>();
        }

        public YieldInstruction Right()
        {
            _input.Press(_keyboard.dKey);
            return new WaitForEndOfFrame();
        }
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