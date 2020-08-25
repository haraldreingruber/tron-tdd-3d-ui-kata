using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.UI;

namespace IntegrationTests
{
    public class InputPressed // TODO: rename?
    {
        // taken from: https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Testing.html
        private readonly InputTestFixture _inputSystem = new InputTestFixture();
        private Keyboard _keyboard;
        private Mouse _mouse;

        public void PrepareInputSystem()
        {
            _inputSystem.Setup();
            _keyboard = InputSystem.AddDevice<Keyboard>();
            _mouse = InputSystem.AddDevice<Mouse>();
        }

        public void CloseInputSystem()
        {
            _inputSystem.TearDown();
        }

        public IEnumerator Right()
        {
            _inputSystem.Press(_keyboard.dKey);
            yield return new WaitForEndOfFrame();
        }

        public IEnumerator MouseLeftClick(Vector2 position)
        {
            _inputSystem.Move(_mouse.position, position);
            yield return new WaitForEndOfFrame();
            _inputSystem.Press(_mouse.leftButton);
            yield return new WaitForEndOfFrame();
        }
    }
}