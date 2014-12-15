using System.Collections;
using TowerDefense.Controllers;
using UnityEngine.UI;

namespace TowerDefense.GamePhases
{
    internal sealed class BuildTowersPhase : GamePhase
    {
        private Button m_StartGameButton;

        public override IEnumerator DoPhase()
        {
            m_StartGameButton = UIController.StartGameButton;
            m_StartGameButton.onClick.AddListener(FinishPhase);
            m_StartGameButton.gameObject.SetActive(true);

            yield break;
        }

        protected override void FinishPhase()
        {
            m_StartGameButton.gameObject.SetActive(false);
            m_StartGameButton.onClick.RemoveListener(FinishPhase);
            m_StartGameButton = null;

            base.FinishPhase();
        }
    }
}
