using System;
using UnityEngine;

namespace TowerDefense.Behaviours
{
    public class Enemy : MonoBehaviour
    {
        private NavMeshAgent m_NavMeshAgent;
        private Transform m_Transform;

        private void Start()
        {
            m_NavMeshAgent = GetComponent<NavMeshAgent>();
            m_Transform = GetComponent<Transform>();

            var targetPosition = Target.Instance.Position;
            m_NavMeshAgent.SetDestination(targetPosition);
        }

        // Returns true if it gets destroyed
        public bool SparseUpdate()
        {
            var navigationDistance = m_NavMeshAgent.remainingDistance;
            var realDistanceSqr = (Target.Instance.Position - m_Transform.position).sqrMagnitude;

            if (realDistanceSqr < 1.0f)
            {
                Target.Instance.DecrementLives();
                Destroy(gameObject);

                return true;
            }
            else if (navigationDistance < 0.5f && realDistanceSqr > 1.0f)
            {
                Explode();

                return true;
            }

            return false;
        }

        private void Explode()
        {
            throw new NotImplementedException();
        }
    }
}