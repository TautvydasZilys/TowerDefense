using TowerDefense.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.Behaviours.UI
{
    public abstract class BuildButton : MonoBehaviour
    {
        [SerializeField]
        private Button m_DeactivatedButtonPrefab;

        [SerializeField]
        private Button m_ActivatedButtonPrefab;

        private Button m_DeactivatedButton;
        private Button m_ActivatedButton;

        private GameObject m_GameObject;
        private Transform m_CameraTransform;
        private Camera m_MainCamera;
        private bool m_IsBuilding;
        private bool m_JustStarted;

        protected abstract Vector3 TowerPosition { get; set; }
        protected abstract void ActivateTower();
        protected abstract void DeactivateTower();
        protected abstract void BuildTower();

        public void Activate()
        {
            m_GameObject.SetActive(true);
            m_DeactivatedButton.gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            m_IsBuilding = false;
            m_DeactivatedButton.gameObject.SetActive(false);
            m_ActivatedButton.gameObject.SetActive(false);
            DeactivateTower();

            m_GameObject.SetActive(false);
        }

        protected virtual void Start()
        {
            var buttonPosition = m_DeactivatedButtonPrefab.transform.position;

            m_DeactivatedButton = (Button)Instantiate(m_DeactivatedButtonPrefab, buttonPosition, Quaternion.identity);
            m_ActivatedButton = (Button)Instantiate(m_ActivatedButtonPrefab, buttonPosition, Quaternion.identity);

            m_ActivatedButton.transform.SetParent(UIController.Canvas.transform, false);
            m_DeactivatedButton.transform.SetParent(UIController.Canvas.transform, false);

            m_DeactivatedButton.onClick.AddListener(StartBuilding);
            m_ActivatedButton.onClick.AddListener(StopBuilding);

            m_GameObject = gameObject;
            m_MainCamera = Camera.main;
            m_CameraTransform = m_MainCamera.transform;

            StopBuilding();
        }

        private void Update()
        {
            if (!m_IsBuilding)
                return;

            var cameraPos = m_CameraTransform.position;
            var mousePos = Input.mousePosition;
            var screenPos = new Vector3(mousePos.x, mousePos.y, cameraPos.y - TowerPosition.y);
            var newPos = m_MainCamera.ScreenToWorldPoint(screenPos);

            newPos.x = Mathf.Round(newPos.x);
            newPos.y = Mathf.Round(newPos.y);
            newPos.z = Mathf.Round(newPos.z);

            TowerPosition = newPos;

            if (!m_JustStarted && Input.GetMouseButtonUp(1))
            {
                StopBuilding();
                return;
            }

            if (!m_JustStarted && Input.GetMouseButtonUp(0))
            {
                BuildTower();

                if (!Input.GetButton("Shift"))
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
            ActivateTower();
        }

        private void StopBuilding()
        {
            m_IsBuilding = false;
            m_DeactivatedButton.gameObject.SetActive(true);
            m_ActivatedButton.gameObject.SetActive(false);
            DeactivateTower();
        }
    }
}
