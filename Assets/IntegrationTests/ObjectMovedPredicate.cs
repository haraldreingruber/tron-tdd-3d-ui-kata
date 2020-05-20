using System;
using UnityEngine;

namespace IntegrationTests
{
    public class ObjectMovedPredicate
    {
        private readonly Transform _transform;
        private readonly Vector3 _originalPosition;
        private readonly float _requiredDistance;

        public ObjectMovedPredicate(Transform transform, float requiredDistance)
        {
            _transform = transform;
            _originalPosition = transform.position;
            _requiredDistance = requiredDistance;
        }

        public float CurrentDistance { get; private set; }

        public bool HasMoved()
        {
            var newPosition = _transform.position;
            CurrentDistance = Math.Abs(newPosition.z - _originalPosition.z);
            return CurrentDistance >= _requiredDistance;
        }
    }
}