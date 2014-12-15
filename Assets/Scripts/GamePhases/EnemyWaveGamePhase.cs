using System.Collections;
using System.Collections.Generic;
using TowerDefense.Behaviours;
using TowerDefense.Controllers;
using UnityEngine;

namespace TowerDefense.GamePhases
{
    internal sealed class EnemyWaveGamePhase : GamePhase
    {
        private GameplayController.LevelInfo m_LevelInfo;
        private List<Enemy> m_Enemies = new List<Enemy>();
        private bool m_GameOver;

        public override IEnumerator DoPhase()
        {
            m_GameOver = false;
            m_LevelInfo = m_GameplayController.Levels[m_GameplayController.CurrentLevel - 1];

            CentralController.BeginCoroutine(SpawnEnemies());
            CentralController.BeginCoroutine(EnemyUpdate());

            yield break;
        }

        private IEnumerator SpawnEnemies()
        {
            for (int i = 0; i < m_LevelInfo.EnemyCount && !m_GameOver; i++)
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

                        if (Target.Instance.LivesRemaining == 0)
                        {                            
                            GameOver();
                            yield break;
                        }
                    }
                }

                if (m_Enemies.Count == 0)
                {
                    m_GameplayController.CompleteLevel();
                    FinishPhase();
                    yield break;
                }
            }
        }

        private void GameOver()
        {
            m_GameOver = true;

            for (int i = 0; i < m_Enemies.Count; i++)
            {
                m_Enemies[i].Explode();
            }

            m_Enemies.Clear();

            var nextPhase = m_NextPhase;
            m_NextPhase = new GameOverPhase();
            m_NextPhase.Initialize(m_GameplayController, nextPhase);

            FinishPhase();
        }
    }
}
