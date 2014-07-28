using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TowerDefense.Controllers;
using UnityEngine;

namespace TowerDefense.GamePhases
{
    internal class WaveStartingMessagePhase : GamePhase
    {
        private GUIStyle m_GUIStyle;
        private string m_Message;

        public WaveStartingMessagePhase()
        {
            m_GUIStyle = new GUIStyle()
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 96
            };

            m_GUIStyle.normal.textColor = Color.white;
        }

        public override IEnumerator DoPhase()
        {
            m_Message = string.Format("Level {0} is about to start", m_GameplayController.CurrentLevel);

            var eventHandle = GameLoopController.AddEvent(GameLoopController.LoopControllers.OnGUI, DrawLabel);
            yield return new WaitForSeconds(5.0f);
            GameLoopController.RemoveEvent(GameLoopController.LoopControllers.OnGUI, eventHandle);

            FinishPhase();
        }

        private void DrawLabel()
        {
            var screenRect = new Rect(0.0f, 0.0f, VariablesController.ScreenWidth, VariablesController.ScreenHeight);
            GUI.Label(screenRect, m_Message, m_GUIStyle);
        }
    }
}
