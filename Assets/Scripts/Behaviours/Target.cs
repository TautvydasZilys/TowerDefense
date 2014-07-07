using UnityEngine;
using System.Collections;
using TowerDefense.DataStructures;

namespace TowerDefense.Behaviours
{
    public class Target : MonoBehaviour
    {
        public static Target Instance;
        public Vector2Int Position { get { return m_Position; } }

        private Vector2Int m_Position;

        public Target()
        {
            Instance = this;
        }

        void Start()
        {
            var worldPos = transform.position;
            m_Position = new Vector2Int(worldPos.x, worldPos.z);
        }
    }
}