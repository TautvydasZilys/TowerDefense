using UnityEngine;
using System.Collections;

namespace TowerDefense.Behaviours
{
    public class Target : MonoBehaviour
    {
        public static Target Instance { get { return s_Instance; } }
        public Vector3 Position { get { return CachedTransform.position; } }

        private static Target s_Instance;
        private Transform m_Transform;

        private Transform CachedTransform
        {
            get
            {
                if (Object.ReferenceEquals(m_Transform, null))
                {
                    m_Transform = transform;
                }

                return transform;
            }
        }

        public Target()
        {
            s_Instance = this;
        }
    }
}