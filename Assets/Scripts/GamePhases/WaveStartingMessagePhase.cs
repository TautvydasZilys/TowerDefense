using System.Collections;
using TowerDefense.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.GamePhases
{
    internal sealed class WaveStartingMessagePhase : GamePhase
    {
        private Text m_WaveStartingText;

        public override IEnumerator DoPhase()
        {
            m_WaveStartingText = UIController.WaveStartingText;
            m_WaveStartingText.text = string.Format("Level {0} is about to start", m_GameplayController.CurrentLevel);
            m_WaveStartingText.gameObject.SetActive(true);

            yield return new WaitForSeconds(3.0f);

            m_WaveStartingText.gameObject.SetActive(false);
            m_WaveStartingText = null;

            FinishPhase();
        }
    }
}
