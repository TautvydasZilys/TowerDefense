using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefense.Controllers
{
    internal class GameLoopController
    {
        private List<Action> m_ActionList;
        private static GameLoopController[] s_Controllers;

        static GameLoopController()
        {
            s_Controllers = new GameLoopController[(int)LoopControllers.ControllerCount];

            for (int i = 0; i < (int)LoopControllers.ControllerCount; i++)
            {
                s_Controllers[i] = new GameLoopController();
            }
        }

        public static int AddEvent(LoopControllers controller, Action action)
        {
            var actionList = s_Controllers[(int)controller].m_ActionList;

            for (int i = 0; i < actionList.Count; i++)
            {
                if (actionList[i] == null)
                {
                    actionList[i] = action;
                    return i;
                }
            }

            actionList.Add(action);
            return actionList.Count - 1;
        }

        public static void RemoveEvent(LoopControllers controller, ref int actionHandle)
        {
            s_Controllers[(int)controller].m_ActionList[actionHandle] = null;
            actionHandle = -1;
        }

        public static void Update()
        {
            s_Controllers[(int)LoopControllers.Update].Loop();
        }

        public static void OnGUI()
        {
            s_Controllers[(int)LoopControllers.OnGUI].Loop();
        }

        private GameLoopController()
        {
            m_ActionList = new List<Action>();
        }

        private void Loop()
        {
            for (int i = 0; i < m_ActionList.Count; i++)
            {
                if (m_ActionList[i] != null)
                {
                    m_ActionList[i]();
                }
            }
        }

        internal enum LoopControllers
        {
            Update = 0,
            OnGUI,
            ControllerCount
        }
    }
}
