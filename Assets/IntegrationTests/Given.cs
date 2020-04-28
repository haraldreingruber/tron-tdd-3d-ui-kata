using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace IntegrationTests
{
    public static class Given
    {
        public static IEnumerator Scene(SceneTestFixture self, string sceneName)
        {
            yield return self.LoadScene(sceneName);

            var currentSceneName = "";
            // Wait a few seconds to ensure the scene starts correctly
            for (int i = 0; i < 20; i++)
            {
                yield return new WaitForSeconds(0.1f);
                currentSceneName = SceneManager.GetActiveScene().name;
                if (currentSceneName == sceneName)
                {
                    break;
                }
            }
            //yield return new WaitForSeconds(2.0f);  // TODO: remove time or replace with condition

            Assert.That(currentSceneName, Is.EqualTo(sceneName));
        }
    }
}