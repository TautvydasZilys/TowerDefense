using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TowerDefense.Behaviours
{
    public class Enemy : MonoBehaviour
    {
        private NavMeshAgent m_NavMeshAgent;

        void Start()
        {
            m_NavMeshAgent = GetComponent<NavMeshAgent>();
            var targetPosition = Target.Instance.Position;
            Debug.Log("Going to " + targetPosition);

            m_NavMeshAgent.SetDestination(targetPosition);
        }

        void Update()
        {
            Debug.Log("Remains " + m_NavMeshAgent.remainingDistance);
        }
    }
}