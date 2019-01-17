using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using Unity.Entities;

namespace ZombiesRun.AI
{
    public class MoveAgent : ComponentSystem
    {
        private struct Components
        {
            public AgentData data;
            public NavMeshAgent agent;
        }

        protected override void OnUpdate()
        {
            ComponentGroupArray<Components> entities = GetEntities<Components>();
            AgentHub hub = AgentHub.Instance;

            foreach(Components entity in entities)
            {
                if (AtDestination(entity.agent))
                {
                    entity.agent.SetDestination(AgentHub.Instance.GetPosition());
                }
            }
        }

        public bool AtDestination(NavMeshAgent _agent)
        {
            return _agent.remainingDistance <= _agent.stoppingDistance;
        }
    }
}
