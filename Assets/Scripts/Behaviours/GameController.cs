using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Behaviours
{
    public class GameController : MonoBehaviour
    {
        #region Public inspector fields

        public float kEnemyUpdateInterval = 0.2f;
        public float kSpawnInterval = 1.0f;
        public Vector3 kSpawnPoint;
        public GameObject kEnemyPrefab;
        
        public float kCameraMinimumHeight = 1.0f;
        public float kCameraMaximumHeight = 50.0f;
        public float kCameraScrollSpeed = 1.0f;
        public float kCameraZoomSpeed = 1.2f;
        public int kCameraScrollBorderThickness = 10;

        #endregion

        private List<Enemy> m_Enemies = new List<Enemy>();
        private Rigidbody m_Rigidbody;

        private void Start()
        {
            StartCoroutine(SpawnEnemies());
            StartCoroutine(EnemyUpdate());

            m_Rigidbody = rigidbody;
        }

        private void Update()
        {
            UpdateCamera();
        }

        #region Enemy control

        private IEnumerator SpawnEnemies()
        {
            for (;;)
            {
                yield return new WaitForSeconds(kSpawnInterval);

                var enemyObject = Instantiate(kEnemyPrefab, kSpawnPoint, Quaternion.identity) as GameObject;
                m_Enemies.Add(enemyObject.GetComponent<Enemy>());
            }
        }

        private IEnumerator EnemyUpdate()
        {
            for (;;)
            {
                yield return new WaitForSeconds(kEnemyUpdateInterval);

                for (int i = 0; i < m_Enemies.Count; i++)
                {
                    if (m_Enemies[i].SparseUpdate())
                    {
                        m_Enemies[i] = m_Enemies[m_Enemies.Count - 1];
                        m_Enemies.RemoveAt(m_Enemies.Count - 1);
                    }
                }
            }
        }

        #endregion

        #region Camera control

        private void UpdateCamera()
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

        #endregion
    }
}
