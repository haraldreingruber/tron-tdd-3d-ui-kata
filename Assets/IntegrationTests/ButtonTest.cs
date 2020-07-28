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
        [UnityTest]
        public IEnumerator StartsGameOnButtonClick() 
        {
            yield return Given.Scene(this, "MainScene");

            var tron = Find.SingleObjectById("Tron");
            var tronSpy = tron.AddComponent<TronSpy>();
            
            var buttonGameObject = Find.SingleObjectById("StartButton");
            var button = buttonGameObject.GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(tronSpy.StartRace);

            // first try: button.onClick.Invoke(); - works of course
            button.Select();
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