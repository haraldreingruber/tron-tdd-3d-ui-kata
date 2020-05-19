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
            this._transform = transform;
            this._originalPosition = transform.position;
            this._requiredDistance = requiredDistance;
        }

        public float CurrentDistance { get; private set; } = 0.0f;

        public bool HasMoved()
        {
            var newPosition = _transform.position;
            CurrentDistance = Math.Abs(newPosition.z - _originalPosition.z);
            return CurrentDistance >= _requiredDistance;
        }
    }
}