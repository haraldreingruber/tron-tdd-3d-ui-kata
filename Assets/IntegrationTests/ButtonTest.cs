using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using Zenject;

namespace IntegrationTests
{
    public class ButtonTest : SceneTestFixture
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
        [Ignore("We cannot make it work, skip it after 5 hours trial and error")]
        public IEnumerator StartsGameOnButtonClick()
        {
            yield return Given.Scene(this, "MainScene");
            yield return new WaitForEndOfFrame();

            var tron = Find.SingleObjectById("Tron");
            var tronSpy = tron.AddComponent<TronSpy>();

            var buttonGameObject = Find.SingleObjectById("StartButton");
            var button = buttonGameObject.GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(tronSpy.StartRace);

            // Trying to  get the simulation of the button click to work. No success.
            // It feels like Fixture is not connected to UI somehow. Experiments below...
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(10);

            // first try: button.onClick.Invoke(); - works of course
            button.Select();

            var buttonRectTransform = button.GetComponent<RectTransform>();
            // var buttonRect = buttonRectTransform.rect;
            // var buttonCenter = buttonRect.position;
            yield return _inputPressed.MouseLeftClick(buttonRectTransform.position);

            yield return new WaitForSeconds(15);
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

            Assert.That(tronSpy.StartRacingHasBeenCalled(), Is.True,
                "tronSpy.StartRacingHasBeenCalled() has been called");
        }
    }

    class TronSpy : Tron
    {
        private bool _startRacingHasBeenCalled;

        public bool StartRacingHasBeenCalled()
        {
            return _startRacingHasBeenCalled;
        }

        public override void StartRace()
        {
            _startRacingHasBeenCalled = true;
        }
    }
}