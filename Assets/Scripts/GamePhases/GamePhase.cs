using System.Collections;
using TowerDefense.Behaviours;
using TowerDefense.Controllers;

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

        protected virtual void FinishPhase()
        {
            CentralController.BeginCoroutine(m_NextPhase.DoPhase());
        }
    }
}
