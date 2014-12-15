using System.Collections;
using TowerDefense.Behaviours;
using TowerDefense.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.GamePhases
{
    internal sealed class GameOverPhase : GamePhase
    {
        private int m_UpdateEventHandle;
        private Text m_GameOverText;

        public override IEnumerator DoPhase()
        {
            var currentLevel = m_GameplayController.CurrentLevel;

            m_GameOverText = UIController.GameOverText;
            m_GameOverText.text = string.Format("Game over!\r\nYou have survived {0} level{1}.\r\nPress any key to play again",
                currentLevel - 1, currentLevel == 1 ? "s" : "");
            m_GameOverText.gameObject.SetActive(true);

            m_UpdateEventHandle = GameLoopController.AddEvent(GameLoopController.LoopControllers.Update, Update);

            yield break;
        }

        private void Update()
        {
            if (Input.anyKeyDown)
            {
                Finish();
            }
        }

        private void Finish()
        {
            GameLoopController.RemoveEvent(GameLoopController.LoopControllers.Update, ref m_UpdateEventHandle);

            m_GameOverText.gameObject.SetActive(false);
            m_GameOverText = null;

            m_GameplayController.ResetLevel();
            Target.Instance.ResetLives();

            FinishPhase();
        }
    }
}
