using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TowerDefense.Controllers
{
    [Serializable]
    public class CameraController
    {
        public float kCameraMinimumHeight = 1.0f;
        public float kCameraMaximumHeight = 50.0f;
        public float kCameraScrollSpeed = 1.0f;
        public float kCameraZoomSpeed = 1.2f;
        public int kCameraScrollBorderThickness = 10;

        private Rigidbody m_Rigidbody;

        public void Initialize(Rigidbody cameraRigidbody)
        {
            m_Rigidbody = cameraRigidbody;
            GameLoopController.AddEvent(GameLoopController.LoopControllers.Update, Update);
        }

        public void Update()
        {
            var mousePosition = Input.mousePosition;
            var screenWidth = Screen.width;
            var screenHeight = Screen.height;
            var deltaTime = Time.deltaTime;

            var newPosition = m_Rigidbody.position;
            bool shouldMove = false;

            if (mousePosition.x < kCameraScrollBorderThickness)
            {
                newPosition += new Vector3(-deltaTime * kCameraScrollSpeed * newPosition.y, 0.0f, 0.0f);
                shouldMove = true;
            }
            else if (mousePosition.x > screenWidth - kCameraScrollBorderThickness)
            {
                newPosition += new Vector3(deltaTime * kCameraScrollSpeed * newPosition.y, 0.0f, 0.0f);
                shouldMove = true;
            }

            if (mousePosition.y < kCameraScrollBorderThickness)
            {
                newPosition += new Vector3(0.0f, 0.0f, -deltaTime * kCameraScrollSpeed * newPosition.y);
                shouldMove = true;
            }
            else if (mousePosition.y > screenHeight - kCameraScrollBorderThickness)
            {
                newPosition += new Vector3(0.0f, 0.0f, deltaTime * kCameraScrollSpeed * newPosition.y);
                shouldMove = true;
            }

            var wheel = Input.GetAxis("Mouse ScrollWheel");

            if (wheel > 0.0f)
            {
                if (newPosition.y > kCameraZoomSpeed * kCameraMinimumHeight)
                {
                    newPosition.y /= kCameraZoomSpeed;
                    shouldMove = true;
                }
            }
            else if (wheel < 0.0f)
            {
                if (kCameraZoomSpeed * newPosition.y < kCameraMaximumHeight)
                {
                    newPosition.y *= kCameraZoomSpeed;
                    shouldMove = true;
                }
            }

            if (shouldMove)
            {
                m_Rigidbody.MovePosition(newPosition);
            }
        }
    }
}
