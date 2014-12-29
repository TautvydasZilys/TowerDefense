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
            
            foreach (var button in UIController.BuildButtons)
                button.Activate();

            yield break;
        }

        protected override void FinishPhase()
        {
            m_StartGameButton.gameObject.SetActive(false);
            m_StartGameButton.onClick.RemoveListener(FinishPhase);
            m_StartGameButton = null;

            foreach (var button in UIController.BuildButtons)
                button.Deactivate();

            base.FinishPhase();
        }
    }
}
