using System;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class RacingInteractionTest
    {
        [Test]
        public void StartsRacing()
        {
            // same test, no UI?
            var tron = new GameObject();
            var racingInteraction = tron.AddComponent<RacingInteraction>();

            var tronTransform = tron.transform;
            var originalPosition = tronTransform.position;

            racingInteraction.StartRace();

            racingInteraction.FixedUpdate();
            // ReSharper disable once Unity.InefficientPropertyAccess - position has changed
            var newPosition = tronTransform.position;

            Assert.That(newPosition.y, Is.EqualTo(originalPosition.y));
            Assert.That(Math.Abs(newPosition.z - originalPosition.z), Is.GreaterThan(0.001f));
        }

        [Test]
        public void StandsStillOnStartup()
        {
            var tron = new GameObject();
            var racingInteraction = tron.AddComponent<RacingInteraction>();

            var tronTransform = tron.transform;
            var originalPosition = tronTransform.position;

            racingInteraction.FixedUpdate();
            // ReSharper disable once Unity.InefficientPropertyAccess - position has changed
            var newPosition = tronTransform.position;

            Assert.That(newPosition, Is.EqualTo(originalPosition));
        }

        /*
 * Test list
 * - do not move unless start racing - UI?
 * - speed or relative movement - UI?
 * - leaves walls behind
 */
    }
}