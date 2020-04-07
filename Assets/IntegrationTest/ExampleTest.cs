using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;
using Zenject;

namespace Tests
{
    public class LoadGameOfLifeScene : SceneTestFixture
    {
        [UnityTest]
        public IEnumerator TestEmptySceneIsBlue() {
            yield return LoadScene("CellTestScene");

            // Wait a few seconds to ensure the scene starts correctly
            yield return new WaitForSeconds(2.0f);

            // var cell = Object.FindObjectOfType<CellVis2>();
            var texture = ScreenCapture.CaptureScreenshotAsTexture();
            var pixel = texture.GetPixel(100, 100);
            // cleanup
            Object.Destroy(texture);

            Assert.That(pixel, Is.EqualTo(Color.blue));
        }

        [UnityTest]
        public IEnumerator TestAliveCellIsRed() {
            yield return LoadScene("CellTestScene");

            // Wait a few seconds to ensure the scene starts correctly
            yield return new WaitForSeconds(2.0f);

            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Cell.prefab");
            var cell = Object.Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
            cell.transform.localScale = Vector3.one * 14;
            var cellVisualization = cell.GetComponent<CellVisualization>();

            yield return new WaitForEndOfFrame();

            // var cell = Object.FindObjectOfType<CellVis2>();
            var texture = ScreenCapture.CaptureScreenshotAsTexture();
            var pixel = texture.GetPixel(texture.width/2, texture.height/2);

            // cleanup
            Object.Destroy(texture);

            var comparer = new ColorEqualityComparer(10e-4f);
            Assert.That(pixel, Is.EqualTo(new Color(0.420f, 0.055f, 0.047f, 1.000f)).Using(comparer));
        }

        /*
         [UnityTest]
         public IEnumerator TestGameOfLifeScene()
         {
             yield return LoadScene("GameOfLife");

             // Wait a few seconds to ensure the scene starts correctly
             yield return new WaitForSeconds(2.0f);

             // var cell = Object.FindObjectOfType<CellVisualization>();
             // Assert.That(cell, Is.Not.Null);
             // Assert.That(cell.gameObject.activeInHierarchy, Is.True);
         }
         */
    }

}
