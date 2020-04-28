using System;
using UnityEngine;

namespace IntegrationTests
{
    public sealed class WaitUntilOrTimeout : CustomYieldInstruction
    {
        private readonly Func<bool> m_Predicate;
        private readonly float m_Timeout;

        public override bool keepWaiting
        {
            get
            {
                return Time.time < this.m_Timeout && !this.m_Predicate();
            }
        }

        public WaitUntilOrTimeout(Func<bool> predicate, float timeoutSeconds)
        {
            this.m_Predicate = predicate;
            this.m_Timeout = Time.time + timeoutSeconds;
        }
    }
}