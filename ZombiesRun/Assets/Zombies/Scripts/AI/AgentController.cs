using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ZombiesRun.AI
{
    public class AgentController : MonoBehaviour
    {
        [SerializeField] private bool m_sleeping = false;
        [SerializeField] private NavMeshAgent m_agent = null;

        private void Update()
        {
            if (!m_sleeping)
            {
                if (AtDestination())
                {
                    m_agent.SetDestination(AgentHub.Instance.GetPosition());
                }
            }
        }

        public bool AtDestination()
        {
            return m_agent.remainingDistance <= m_agent.stoppingDistance;
        }

        public bool IsSleeping()
        {
            return m_sleeping;
        }

        public void SetSleeping(bool _state)
        {
            m_sleeping = _state;
        }
    }
}
