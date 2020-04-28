using System;
using UnityEngine;

namespace IntegrationTests
{
    public sealed class WaitUntilOrTimeout : CustomYieldInstruction
    {
        private readonly Func<bool> m_Predicate;
        private readonly float m_Timeout;

        public override bool keepWaiting => Time.time < m_Timeout && !m_Predicate(); // TODO Do we need to fail if timeout reached?

        public WaitUntilOrTimeout(Func<bool> predicate, float timeoutSeconds)
        {
            m_Predicate = predicate;
            m_Timeout = Time.time + timeoutSeconds;
        }
    }
}