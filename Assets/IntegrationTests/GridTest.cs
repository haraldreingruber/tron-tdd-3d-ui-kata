using System.Collections;
using UnityEngine.TestTools;
using Zenject;

namespace IntegrationTests
{
    public class GridTest : SceneTestFixture
    {
        // Grid test list:
        // When game starts, there is a tron on the grid.
        // (done) 1. load the main game scene
        // (done) 2. Assert.that("there is a grid") Is it relevant if it is static?
        // (done) 3. Assert.that("there is a tron")
        // (skip) 4. Assert.that("tron is at the 'correct' location)

        [UnityTest]
        public IEnumerator Exists()
        {
            yield return Given.Scene(this, "MainScene");

            const string objectId = "Grid";
            var grid = Find.SingleObjectById(objectId);
            AssertThat.IsVisible(grid, objectId);
            // TODO Do we need to test the size of the grid?
            // Definitely not test the colour of the grid.
        }
    }
}