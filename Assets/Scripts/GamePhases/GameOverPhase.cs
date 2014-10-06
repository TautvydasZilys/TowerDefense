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
    class GameOverPhase : GamePhase
    {
        private GUIStyle m_GUIStyle;
        private string m_Message;

        private int m_OnGUIEventHandle;
        private int m_UpdateEventHandle;

        public GameOverPhase()
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
            var currentLevel = m_GameplayController.CurrentLevel;

            m_Message = string.Format("Game over!\r\nYou have survived {0} level{1}.\r\nPress any key to play again",
                currentLevel - 1, currentLevel == 1 ? "s" : "");

            m_OnGUIEventHandle = GameLoopController.AddEvent(GameLoopController.LoopControllers.OnGUI, DrawLabel);
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

        private void DrawLabel()
        {
            var screenRect = new Rect(0.0f, 0.0f, VariablesController.ScreenWidth, VariablesController.ScreenHeight);
            GUI.Label(screenRect, m_Message, m_GUIStyle);
        }

        private void Finish()
        {
            GameLoopController.RemoveEvent(GameLoopController.LoopControllers.Update, m_UpdateEventHandle);
            GameLoopController.RemoveEvent(GameLoopController.LoopControllers.OnGUI, m_OnGUIEventHandle);

            m_GameplayController.ResetLevel();
            Target.Instance.ResetLives();

            FinishPhase();
        }
    }
}
