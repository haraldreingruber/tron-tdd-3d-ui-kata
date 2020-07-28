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
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

            // first try: button.onClick.Invoke(); - works of course
            button.Select();
            yield return new WaitForSeconds(15);
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

            Assert.That(tronSpy.StartRacingHasBeenCalled(), Is.True);
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