using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Behaviours
{
    public class GameController : MonoBehaviour
    {
        #region Public inspector fields

        public float kEnemyUpdateInterval = 0.2f;
        public float kSpawnInterval = 1.0f;
        public Vector3 kSpawnPoint;
        public GameObject kEnemyPrefab;

        #endregion

        private List<Enemy> m_Enemies = new List<Enemy>();
        
        private void Start()
        {
            StartCoroutine(SpawnEnemies());
            StartCoroutine(EnemyUpdate());
        }

        private IEnumerator SpawnEnemies()
        {
            for (;;)
            {
                yield return new WaitForSeconds(kSpawnInterval);

                var enemyObject = Instantiate(kEnemyPrefab, kSpawnPoint, Quaternion.identity) as GameObject;
                m_Enemies.Add(enemyObject.GetComponent<Enemy>());
            }
        }

        private IEnumerator EnemyUpdate()
        {
            for (;;)
            {
                yield return new WaitForSeconds(kEnemyUpdateInterval);

                for (int i = 0; i < m_Enemies.Count; i++)
                {
                    if (m_Enemies[i].SparseUpdate())
                    {
                        m_Enemies[i] = m_Enemies[m_Enemies.Count - 1];
                        m_Enemies.RemoveAt(m_Enemies.Count - 1);
                    }
                }
            }
        }
    }
}
