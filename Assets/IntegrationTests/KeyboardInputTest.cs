using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TestTools;
using Zenject;

namespace IntegrationTests
{
    //[TestFixture]
    public class KeyboardInputTest : SceneTestFixture
    {
        // taken from: https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Testing.html
        private readonly InputTestFixture _input = new InputTestFixture();

        [SetUp]
        public void foo()
        {
            Debug.Log("setup-foo");
            _input.Setup();
        }

        [TearDown]
        public void foo2()
        {
            Debug.Log("teardown-foo");
            _input.TearDown();
        }

        // TODO test: ignore keys before start race

        private class RacingInteractionMock : RacingInteraction
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

        [UnityTest]
        public IEnumerator FacesRightAfterTurnRight()
        {
            yield return Given.Scene(this, "MainScene");
            var tron = Find.SingleObjectById("Tron");
            var tronTransform = tron.transform;
            var racingInteraction = tron.AddComponent<RacingInteractionMock>();
            tronTransform.GetComponent<Tron>().racingInteraction = racingInteraction;
            tronTransform.GetComponent<Tron>().StartRace();
            yield return new WaitForEndOfFrame();

  
            var keyboard = InputSystem.AddDevice<Keyboard>();
            Debug.Log("setup");
            _input.Press(keyboard.dKey);
            Debug.Log("yield");
            yield return new WaitForEndOfFrame();

            Debug.Log("assert");
            Assert.That(racingInteraction.TurnRightHasBeenCalled());
            Debug.Log("success");
        }
        
        // TODO: test that nothing happens before gmae starts
        // TODO: ignore 0/0 move events
    }
}