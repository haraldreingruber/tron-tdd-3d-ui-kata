using System.Linq;
using NUnit.Framework;
using UnityEngine;

namespace IntegrationTests
{
    public static class Find
    {
        public static GameObject SingleObjectById(string objectId)
        {
            var gameObjects = Object.FindObjectsOfType<Identifier>()
                .Where(identifier => identifier.id == objectId)
                .Select(identifier => identifier.gameObject)
                .ToList();
            Assert.That(gameObjects.Count, Is.EqualTo(1), $"Object {objectId} not found");
            return gameObjects[0];
        }
    }
}