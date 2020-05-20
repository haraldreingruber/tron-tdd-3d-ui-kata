using System;
using NUnit.Framework;
using UnityEngine;

namespace IntegrationTests
{
    public sealed class WaitUntilOrTimeout : CustomYieldInstruction
    {
        private readonly Func<bool> _isFinished;
        private readonly float _timeout;
        private bool _timeoutReached;

        public WaitUntilOrTimeout(Func<bool> isFinished, float timeoutSeconds)
        {
            _isFinished = isFinished;
            _timeout = Time.time + timeoutSeconds;
        }

        public override bool keepWaiting
        {
            get
            {
                _timeoutReached = Time.time >= _timeout;
                return !_timeoutReached && !_isFinished();
            }
        }

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Global
        public void AssertTimeoutWasNotReached(string condition)
        {
            Assert.False(_timeoutReached, $"timeout while waiting for {condition}");
        }
    }
}