﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TowerDefense.Behaviours;
using TowerDefense.Controllers;
using UnityEngine;

namespace TowerDefense.GamePhases
{
    internal abstract class GamePhase
    {
        protected GamePhase m_NextPhase;
        protected GameplayController m_GameplayController;

        public void Initialize(GameplayController gameplayController, GamePhase nextPhase)
        {
            m_GameplayController = gameplayController;
            m_NextPhase = nextPhase;
        }

        public abstract IEnumerator DoPhase();

        protected void FinishPhase()
        {
            CentralController.BeginCoroutine(m_NextPhase.DoPhase());
        }
    }
}
