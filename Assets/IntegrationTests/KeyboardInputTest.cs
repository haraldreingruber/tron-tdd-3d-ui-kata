﻿using System.Collections;
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

        [Ignore("next test")]
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
            _input.Press(keyboard.rightArrowKey);
            yield return new WaitForEndOfFrame();

            Assert.That(racingInteraction.TurnRightHasBeenCalled());
        }
    }
}