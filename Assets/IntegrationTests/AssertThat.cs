using NUnit.Framework;
using UnityEngine;

namespace IntegrationTests
{
    public static class AssertThat
    {
        // ReSharper disable ParameterOnlyUsedForPreconditionCheck.Global
        public static void IsVisible(GameObject gameObject, string objectId)
        // ReSharper restore ParameterOnlyUsedForPreconditionCheck.Global
        {
            Assert.NotNull(gameObject, objectId);
            Assert.That(gameObject.activeInHierarchy, objectId + ".activeInHierarchy");
        }
    }
}