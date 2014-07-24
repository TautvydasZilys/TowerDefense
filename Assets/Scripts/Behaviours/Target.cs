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

        private static Target s_Instance;
        private Transform m_Transform;
        private int m_LivesRemaining;
        
        public Target()
        {
            s_Instance = this;
        }

        private void Awake()
        {
            m_Transform = transform;
            m_LivesRemaining = kLives;
        }

        public void DecrementLives()
        {
            m_LivesRemaining--;
        }
    }
}