using UnityEngine;

namespace TowerDefense.Controllers
{
    internal static class VariablesController
    {
        public static int ScreenWidth { get; private set; }
        public static int ScreenHeight { get; private set; }

        public static void Initialize()
        {
            UpdateValues();
            GameLoopController.AddEvent(GameLoopController.LoopControllers.Update, UpdateValues);
        }

        private static void UpdateValues()
        {
            ScreenWidth = Screen.width;
            ScreenHeight = Screen.height;
        }
    }
}
