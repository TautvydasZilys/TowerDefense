using System.Collections;
using System.Collections.Generic;
using TowerDefense.Controllers;
using UnityEngine;

namespace TowerDefense.Behaviours
{
    public class CentralController : MonoBehaviour
    {
        public CameraController CameraController;
        public GameplayController GameplayController;

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
            CameraController.Initiaze(rigidbody);
            VariablesController.Initialize();

            GameplayController.Start();
        }

        private void Update()
        {
            GameLoopController.Update();
        }

        private void OnGUI()
        {
            GameLoopController.OnGUI();
        }
    }
}
