using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TowerDefense.Behaviours;
using TowerDefense.Controllers;
using UnityEngine;

namespace TowerDefense.GamePhases
{
    internal class EnemyWaveGamePhase : GamePhase
    {
        private GameplayController.LevelInfo m_LevelInfo;
        private bool m_Finished;

        private List<Enemy> m_Enemies = new List<Enemy>();

        public override IEnumerator DoPhase()
        {
            m_LevelInfo = m_GameplayController.Levels[m_GameplayController.CurrentLevel];
            m_Finished = false;

            CentralController.BeginCoroutine(SpawnEnemies());
            CentralController.BeginCoroutine(EnemyUpdate());

            while (!m_Finished)
            {
                yield return new WaitForSeconds(2.0f);
            }
            
            FinishPhase();
        }

        private IEnumerator SpawnEnemies()
        {
            for (int i = 0; i < m_LevelInfo.EnemyCount; i++)
            {
                var enemyObject = UnityEngine.Object.Instantiate(m_LevelInfo.EnemyPrefab, m_LevelInfo.SpawnPoint.transform.position, Quaternion.identity) as GameObject;
                m_Enemies.Add(enemyObject.GetComponent<Enemy>());

                yield return new WaitForSeconds(m_LevelInfo.EnemySpawnInterval);
            }
        }

        private IEnumerator EnemyUpdate()
        {
            for (;;)
            {
                yield return new WaitForSeconds(0.2f);

                for (int i = 0; i < m_Enemies.Count; i++)
                {
                    if (m_Enemies[i].SparseUpdate())
                    {
                        m_Enemies[i] = m_Enemies[m_Enemies.Count - 1];
                        m_Enemies.RemoveAt(m_Enemies.Count - 1);
                    }
                }

                if (m_Enemies.Count == 0)
                {
                    m_Finished = true;
                    yield break;
                }
            }
        }
    }
}
