using System.Collections;
using NUnit.Framework;
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
            yield return new WaitUntilOrTimeout(() =>
            {
                currentSceneName = SceneManager.GetActiveScene().name;
                return currentSceneName == sceneName;
            }, 2.0f);

            Assert.That(currentSceneName, Is.EqualTo(sceneName));
        }
    }
}