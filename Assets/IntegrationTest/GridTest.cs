using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Zenject;

namespace IntegrationTest {
    public class GridTest : SceneTestFixture {
        // When game starts, there is a tron on the grid.
        // (done) 1. load the main game scene
        // (done) 2. Assert.that("there is a grid") Is it relevant if it is static?
        // 3. Assert.that("there is a tron")
        // 4. Assert.that("tron is at the 'correct' location)
        [UnityTest]
        public IEnumerator TestGridExists() {
            var sceneName = "MainScene";
            yield return LoadScene(sceneName);

            // Wait a few seconds to ensure the scene starts correctly
            yield return new WaitForSeconds(2.0f);

            var currentSceneName = SceneManager.GetActiveScene().name;
            Assert.That(currentSceneName, Is.EqualTo(sceneName));
            // TODO extract as givenScene

            var objectId = "Grid";
            var grid = findSingleObjectById(objectId);

            AssertIsVisible(grid, objectId);
            // open question: Do we need to test the size of the grid?
            // Definitely not test the colour of the grid.
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

        /*
        [UnityTest]
        public IEnumerator TestTronIsOnGridAtStartingPosition()
        {
            yield assertSceneLoaded("MainScene");

            var tron = Object.FindObjectsOfType<Identifier>()
                .Where(identifier => identifier.id == "Tron")
                .Select(identifier => identifier.gameObject).
                First();

            Assert.NotNull(tron, "tron");
            Assert.That(tron.activeInHierarchy, "tron.activeInHierarchy");
            assertThatSize(tron, ?);
            assertThatPosition(tron, ?);
        }
        */
    }
}