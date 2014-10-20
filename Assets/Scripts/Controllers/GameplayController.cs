using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TowerDefense.Behaviours;
using TowerDefense.GamePhases;
using UnityEngine;

namespace TowerDefense.Controllers
{
    [Serializable]
    public class GameplayController
    {
        #region Helper data structures

        [Serializable]
        public struct LevelInfo
        {
            public GameObject SpawnPoint;
            public GameObject EnemyPrefab;
            public int EnemyCount;
            public float EnemySpawnInterval;
        }

        #endregion

        #region Public fields

        public LevelInfo[] Levels;

        #endregion

        private int m_CurrentLevel = 1;
        public int CurrentLevel { get { return m_CurrentLevel; } }

        private GamePhase[] m_Phases;

        public void Start()
        {
            m_Phases = new GamePhase[]
            {
                new BuildTowersPhase(),
                new WaveStartingMessagePhase(),
                new EnemyWaveGamePhase()
            };

            for (int i = 0; i < m_Phases.Length; i++)
            {
                m_Phases[i].Initialize(this, m_Phases[(i + 1) % m_Phases.Length]);
            }

            CentralController.BeginCoroutine(m_Phases[0].DoPhase());
        }

        public void CompleteLevel()
        {
            m_CurrentLevel++;
        }

        public void ResetLevel()
        {
            m_CurrentLevel = 1;
        }
    }
}
