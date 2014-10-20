using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TowerDefense.Controllers;
using UnityEngine;

namespace TowerDefense.GamePhases
{
    internal class BuildTowersPhase : GamePhase
    {
        private int m_OnGuiEventHandle;

        public override IEnumerator DoPhase()
        {
            m_OnGuiEventHandle = GameLoopController.AddEvent(GameLoopController.LoopControllers.OnGUI, OnGUI);
            yield break;
        }

        protected override void FinishPhase()
        {
            GameLoopController.RemoveEvent(GameLoopController.LoopControllers.OnGUI, ref m_OnGuiEventHandle);
            base.FinishPhase();
        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(20.0f, 20.0f, VariablesController.ScreenWidth - 20.0f, 40.0f), "Start game"))
            {
                FinishPhase();
            }
        }
    }
}
