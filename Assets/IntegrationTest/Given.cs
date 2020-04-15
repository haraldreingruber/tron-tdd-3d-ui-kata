﻿using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace IntegrationTest
{
    public static class Given
    {
        public static IEnumerator Scene(SceneTestFixture self, string sceneName)
        {
            yield return self.LoadScene(sceneName);

            // Wait a few seconds to ensure the scene starts correctly
            yield return new WaitForSeconds(2.0f);

            var currentSceneName = SceneManager.GetActiveScene().name;
            Assert.That(currentSceneName, Is.EqualTo(sceneName));
        }
    }
}