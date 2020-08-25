using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace IntegrationTests
{
    public class InputSimulation
    {
        // taken from: https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Testing.html
        private readonly InputTestFixture _inputSystem = new InputTestFixture();
        private Keyboard _keyboard;
        private Mouse _mouse;

        public void Prepare()
        {
            _inputSystem.Setup();
            _keyboard = InputSystem.AddDevice<Keyboard>();
            _mouse = InputSystem.AddDevice<Mouse>();
        }

        public void Close()
        {
            _inputSystem.TearDown();
        }

        public IEnumerator PressRight()
        {
            var rightKey = _keyboard.dKey;
            _inputSystem.Press(rightKey);
            yield return new WaitForEndOfFrame();
        }
    }
}