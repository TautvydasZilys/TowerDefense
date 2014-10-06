using UnityEngine;
using System.Collections;

namespace TowerDefense.Behaviours
{
    public class Target : MonoBehaviour
    {
        #region Public inspector fields

        public int kLives;

        #endregion

        public static Target Instance { get { return s_Instance; } }
        public Vector3 Position { get { return m_Transform.position; } }
        public int LivesRemaining { get { return m_LivesRemaining; } }

        private static Target s_Instance;
        private Transform m_Transform;
        private int m_LivesRemaining;

        private void Awake()
        {
            s_Instance = this;
            m_Transform = transform;
            m_LivesRemaining = kLives;
        }

        public void DecrementLives()
        {
            m_LivesRemaining--;
        }

        public void ResetLives()
        {
            m_LivesRemaining = kLives;
        }
    }
}