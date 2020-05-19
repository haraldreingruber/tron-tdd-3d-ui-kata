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
            Assert.That(newPosition.x, Is.EqualTo(_originalPosition.x));
            Assert.That(Distance(newPosition), IsNotCloseToStart());
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

        [Test]
        public void MovesWithGivenSpeed()
        {
            _racingInteraction.speedMeterPerSec = 5;
            _racingInteraction.StartRace();

            _racingInteraction.FixedUpdate(); // 0.02 seconds

            var newPosition = CurrentPosition();
            Assert.That(Distance(newPosition), Is.EqualTo(5 * Time.fixedDeltaTime).Within(0.0001f));
        }

        private float Distance(Vector3 newPosition)
        {
            return Math.Abs(newPosition.z - _originalPosition.z);
        }
    }
}