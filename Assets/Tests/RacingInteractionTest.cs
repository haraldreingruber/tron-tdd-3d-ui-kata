using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using UnityEngine;

namespace Tests
{
    public class RacingInteractionTest
    {
        private RacingInteraction _racingInteraction;
        private Transform _racerTransform;
        private Vector3 _originalPosition;

        [SetUp]
        public void CreateRacer()
        {
            var racer = new GameObject();
            _racingInteraction = racer.AddComponent<RacingInteraction>();
            _racerTransform = racer.transform;
            _originalPosition = CurrentPosition();
        }

        private Vector3 CurrentPosition()
        {
            return _racerTransform.position;
        }

        [Test]
        public void StartsRacing()
        {
            // same test, no UI?

            _racingInteraction.StartRace();
            _racingInteraction.FixedUpdate();

            var newPosition = CurrentPosition();
            Assert.That(newPosition.y, Is.EqualTo(_originalPosition.y));
            Assert.That(Math.Abs(newPosition.z - _originalPosition.z), IsNotCloseToStart());
        }

        private static GreaterThanConstraint IsNotCloseToStart()
        {
            return Is.GreaterThan(0.001f);
        }

        [Test]
        public void StandsStillOnStartup()
        {
            _racingInteraction.FixedUpdate();

            Assert.That(CurrentPosition(), Is.EqualTo(_originalPosition));
        }

        /*
 * Test list
 * - speed or relative movement - UI?
 */
    }
}