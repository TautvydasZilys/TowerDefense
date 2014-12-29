using System;
using TowerDefense.Behaviours;
using TowerDefense.Behaviours.UI;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.Controllers
{
    [Serializable]
    public sealed class UIController
    {
        private static UIController s_Instance;

        [SerializeField]
        private Canvas m_Canvas;

        [SerializeField]
        private Button m_StartGameButton;

        [SerializeField]
        private BuildButton[] m_BuildButtons;

        [SerializeField]
        private Text m_WaveStartingText;

        [SerializeField]
        private Text m_GameOverText;

        public static Canvas Canvas { get { return s_Instance.m_Canvas; } }
        public static Button StartGameButton { get { return s_Instance.m_StartGameButton; } }
        public static BuildButton[] BuildButtons { get { return s_Instance.m_BuildButtons; } }
        public static Text WaveStartingText { get { return s_Instance.m_WaveStartingText; } }
        public static Text GameOverText { get { return s_Instance.m_GameOverText; } }

        public UIController()
        {
            s_Instance = this;
        }

        public void Initialize()
        {
            m_StartGameButton.gameObject.SetActive(false);
            m_WaveStartingText.gameObject.SetActive(false);
            m_GameOverText.gameObject.SetActive(false);
        }
    }
}
