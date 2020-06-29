using UnityEngine;
using UnityEngine.InputSystem;

namespace IntegrationTests
{
    public class InputPressed
    {
        // taken from: https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Testing.html
        private readonly InputTestFixture _inputSystem = new InputTestFixture();
        private Keyboard _keyboard;

        public void PrepareInputSystem()
        {
            _inputSystem.Setup();
            _keyboard = InputSystem.AddDevice<Keyboard>();
        }

        public void CloseInputSystem()
        {
            _inputSystem.TearDown();
        }

        public YieldInstruction Right()
        {
            _inputSystem.Press(_keyboard.dKey);
            return new WaitForEndOfFrame();
        }
    }
}