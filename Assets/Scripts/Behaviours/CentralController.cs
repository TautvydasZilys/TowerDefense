using System.Collections;
using TowerDefense.Controllers;
using UnityEngine;

namespace TowerDefense.Behaviours
{
    public sealed class CentralController : MonoBehaviour
    {
        public CameraController CameraController;
        public GameplayController GameplayController;
        public UIController UIController;

        private static CentralController s_Instance;

        public CentralController()
        {
            s_Instance = this;
        }

        public static void BeginCoroutine(IEnumerator coroutine)
        {
            s_Instance.StartCoroutine(coroutine);
        }

        private void Start()
        {
            CameraController.Initialize(rigidbody);
            VariablesController.Initialize();
            UIController.Initialize();

            GameplayController.Start();
        }

        private void Update()
        {
            GameLoopController.Update();
        }
    }
}
