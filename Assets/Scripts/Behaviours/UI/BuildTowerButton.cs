using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TowerDefense.Controllers;

namespace TowerDefense.Behaviours.UI
{
    public sealed class BuildTowerButton : BuildButton
    {
        [SerializeField]
        private GameObject m_TowerPrefab;

        private GameObject m_Tower;
        private Transform m_TowerTransform;
        
        protected override Vector3 TowerPosition
        {
            get { return m_TowerTransform.position; }
            set { m_TowerTransform.position = value; }
        }

        protected override void Start()
        {
            m_Tower = (GameObject)Instantiate(m_TowerPrefab);
            m_TowerTransform = m_Tower.GetComponent<Transform>();

            base.Start();
        }

        protected override void ActivateTower()
        {
            m_Tower.SetActive(true);
        }

        protected override void DeactivateTower()
        {
            m_Tower.SetActive(false);
        }

        protected override void BuildTower()
        {
            Instantiate(m_TowerPrefab, m_TowerTransform.position, Quaternion.identity);
        }
    }
}