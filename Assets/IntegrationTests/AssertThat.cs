using NUnit.Framework;
using UnityEngine;

namespace IntegrationTests
{
    public static class AssertThat
    {
        public static void IsVisible(GameObject gameObject, string objectId)
        {
            Assert.NotNull(gameObject, objectId);
            Assert.That(gameObject.activeInHierarchy, objectId + ".activeInHierarchy");
        }
    }
}