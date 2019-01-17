using System;
using System.Collections.Generic;
using UnityEngine;
using ZombiesRun.General;

namespace ZombiesRun.AI
{
    public class AgentHub : MonoBehaviour
    {
        public static AgentHub Instance = null;

        [Serializable]
        public struct AgentData
        {
            public string m_agentKey;
            public int m_spawnCount;
        }

        [SerializeField] private List<Transform> m_waypoints = new List<Transform>();

        [SerializeField] private Transform m_agentHolder = null;
        [SerializeField] private List<AgentData> m_agentData = new List<AgentData>();
        private Dictionary<string, List<AgentController>> m_agentPool = new Dictionary<string, List<AgentController>>();

        private void Awake()
        {
            CreateInstance();
        }

        private void CreateInstance()
        {
            if (Instance)
            {
                DestroyImmediate(this);
                return;
            }
            else
            {
                Instance = this;
                return;
            }
        }

        private void Start()
        {
            PopulateAgents();
        }

        private void PopulateAgents()
        {
            foreach (AgentData data in m_agentData)
            {
                m_agentPool.Add(data.m_agentKey, new List<AgentController>());

                for (int spawnIter = 0; spawnIter < data.m_spawnCount; spawnIter++)
                {
                    GameObject agentObject = ObjectPool.Instance.GetObject(data.m_agentKey);
                    agentObject.transform.SetParent(m_agentHolder);
                    agentObject.SetActive(true);
                    m_agentPool[data.m_agentKey].Add(agentObject.GetComponent<AgentController>());
                }
            }
        }

        public Vector3 GetPosition()
        {
            return m_waypoints[UnityEngine.Random.Range(0, m_waypoints.Count)].position;
        }
    }
}
