using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZombiesRun.General
{
    public class ObjectPool : MonoBehaviour
    {
        public static ObjectPool Instance = null;

        [Serializable]
        public struct ObjectData
        {
            public string m_objectKey;
            public GameObject m_objectPrefab;
            public int m_spawnCount;
        }

        [SerializeField] private List<ObjectData> m_objectData = new List<ObjectData>();
        private Dictionary<string, Queue<GameObject>> m_objectPools = new Dictionary<string, Queue<GameObject>>();
        [SerializeField] private Transform m_objectHolder = null;

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
                StockPools();
                return;
            }
        }

        private void StockPools()
        {
            foreach (ObjectData data in m_objectData)
            {
                m_objectPools.Add(data.m_objectKey, new Queue<GameObject>());

                for (int spawnIter = 0; spawnIter < data.m_spawnCount; spawnIter++)
                {
                    GameObject spawnedObject = Instantiate(data.m_objectPrefab, m_objectHolder);
                    spawnedObject.SetActive(false);

                    m_objectPools[data.m_objectKey].Enqueue(spawnedObject);
                }
            }
        }

        public GameObject GetObject(string _agentKey)
        {
            if (!m_objectPools.ContainsKey(_agentKey))
            {
                return null;
            }

            GameObject createdObject = m_objectPools[_agentKey].Dequeue();
            m_objectPools[_agentKey].Enqueue(createdObject);
            return createdObject;
        }
    }
}
