using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Zenject;

namespace IntegrationTest {
    public class GridTest : SceneTestFixture {
        // When game starts, there is a tron on the grid.
        // 1. load the main game scene
        // 2. Assert.that("there is a grid") Is it relevant if it is static?
        // 3. Assert.that("there is a tron")
        // 4. Assert.that("tron is at the 'correct' location)
        [UnityTest]
        public IEnumerator TestGridExists() {
            var sceneName = "MainScene";
            yield return LoadScene(sceneName);

            var currentSceneName = SceneManager.GetActiveScene().name;
            Assert.That(currentSceneName, Is.EqualTo(sceneName));

            var grid = Object.FindObjectOfType<Grid>();

            Assert.NotNull(grid, "grid");
            Assert.That(grid.isActiveAndEnabled, "grid.isActiveAndEnabled");
        }
    }
}