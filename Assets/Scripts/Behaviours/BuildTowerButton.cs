using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TowerDefense.Controllers;

namespace TowerDefense.Behaviours
{
    public class BuildTowerButton : MonoBehaviour
    {
        [SerializeField]
        private Button m_DeactivatedButtonPrefab;

        [SerializeField]
        private Button m_ActivatedButtonPrefab;

        [SerializeField]
        private GameObject m_TowerPrefab;

        private Button m_DeactivatedButton;
        private Button m_ActivatedButton;
        private GameObject m_Tower;

        private Transform m_TowerTransform;
        private Transform m_CameraTransform;
        private Camera m_MainCamera;
        private bool m_IsBuilding;
        private bool m_JustStarted;

        private void Start()
        {
            var buttonPosition = m_DeactivatedButtonPrefab.transform.position;

            m_DeactivatedButton = (Button)Instantiate(m_DeactivatedButtonPrefab, buttonPosition, Quaternion.identity);
            m_ActivatedButton = (Button)Instantiate(m_ActivatedButtonPrefab, buttonPosition, Quaternion.identity);
            m_Tower = (GameObject)Instantiate(m_TowerPrefab);

            m_ActivatedButton.transform.SetParent(UIController.Canvas.transform, false);
            m_DeactivatedButton.transform.SetParent(UIController.Canvas.transform, false);

            m_DeactivatedButton.onClick.AddListener(StartBuilding);
            m_ActivatedButton.onClick.AddListener(StopBuilding);
            m_TowerTransform = m_Tower.GetComponent<Transform>();
            m_MainCamera = Camera.main;
            m_CameraTransform = m_MainCamera.transform;

            StopBuilding();
        }

        private void Update()
        {
            if (!m_IsBuilding)
                return;

            var currentPos = m_TowerTransform.position;
            var cameraPos = m_CameraTransform.position;
            var mousePos = Input.mousePosition;
            var screenPos = new Vector3(mousePos.x, mousePos.y, cameraPos.y - currentPos.y);
            var newPos = m_MainCamera.ScreenToWorldPoint(screenPos);

            newPos.x = Mathf.Round(newPos.x);
            newPos.y = Mathf.Round(newPos.y);
            newPos.z = Mathf.Round(newPos.z);

            m_TowerTransform.position = newPos;

            if (!m_JustStarted && Input.GetMouseButtonUp(1))
            {
                StopBuilding();
                return;
            }

            if (!m_JustStarted && Input.GetMouseButtonUp(0))
            {
                BuildTower();
                StopBuilding();
                return;
            }

            m_JustStarted = false;
        }

        private void StartBuilding()
        {
            m_IsBuilding = true;
            m_JustStarted = true;
            m_DeactivatedButton.gameObject.SetActive(false);
            m_ActivatedButton.gameObject.SetActive(true);
            m_Tower.SetActive(true);
        }

        private void StopBuilding()
        {
            m_IsBuilding = false;
            m_DeactivatedButton.gameObject.SetActive(true);
            m_ActivatedButton.gameObject.SetActive(false);
            m_Tower.SetActive(false);
        }

        private void BuildTower()
        {
            var position = m_TowerTransform.position;
            Instantiate(m_TowerPrefab, position, Quaternion.identity);
        }
    }
}