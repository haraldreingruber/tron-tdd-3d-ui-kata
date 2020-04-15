using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Zenject;

namespace IntegrationTest
{
    public class GridTest : SceneTestFixture
    {
        // When game starts, there is a tron on the grid.
        // (done) 1. load the main game scene
        // (done) 2. Assert.that("there is a grid") Is it relevant if it is static?
        // 3. Assert.that("there is a tron")
        // 4. Assert.that("tron is at the 'correct' location)
        [UnityTest]
        public IEnumerator TestGridExists()
        {
            yield return GivenScene("MainScene");

            const string objectId = "Grid";
            var grid = findSingleObjectById(objectId);
            AssertIsVisible(grid, objectId);
            // open question: Do we need to test the size of the grid?
            // Definitely not test the colour of the grid.
        }

        private IEnumerator GivenScene(string sceneName)
        {
            yield return LoadScene(sceneName);

            // Wait a few seconds to ensure the scene starts correctly
            yield return new WaitForSeconds(2.0f);

            var currentSceneName = SceneManager.GetActiveScene().name;
            Assert.That(currentSceneName, Is.EqualTo(sceneName));
        }

        private static GameObject findSingleObjectById(string objectId)
        {
            var gameObjects = Object.FindObjectsOfType<Identifier>()
                .Where(identifier => identifier.id == objectId)
                .Select(identifier => identifier.gameObject)
                .ToList();
            Assert.That(gameObjects.Count, Is.EqualTo(1));
            return gameObjects[0];
        }

        private static void AssertIsVisible(GameObject gameObject, string objectId)
        {
            Assert.NotNull(gameObject, objectId);
            Assert.That(gameObject.activeInHierarchy, objectId + ".activeInHierarchy");
        }

        [UnityTest]
        public IEnumerator TestTronIsOnGridOnStartup()
        {
            yield return GivenScene("MainScene");

            const string objectId = "Tron";
            var tron = findSingleObjectById(objectId);
            AssertIsVisible(tron, objectId);
            AssertIsTwiceAsHigh(tron, objectId);
//          assertThatSize(tron, ?);
//          assertThatPosition(tron, ?);
        }

        private void AssertIsTwiceAsHigh(GameObject tron, string objectId)
        {
            //tron.transform.GetComponent<Tron>()

            var scale = tron.transform.localScale;
            var width = scale.x;
            var depth = scale.z;
            var height = scale.y;

            Assert.That(width, Is.EqualTo(depth));
            Assert.That(height, Is.EqualTo(width));
        }

        /*
        [UnityTest]
        public IEnumerator TestTronStartsRacing()
        {
            yield return GivenScene("MainScene");

            StartRace();
            // yield return new WaitForEndOfFrame();

            const string objectId = "Tron";
            var tron = findSingleObjectById(objectId);
//            assertThat(tron, isDrving);
        }
        */
    }
}