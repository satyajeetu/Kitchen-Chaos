using System;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------



    public class GamePauseUI : MonoBehaviour
    {
        // Public Properties ---------------------------------------------------



        // Private Fields ------------------------------------------------------

        [SerializeField] private Button mainMenuButton;
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button optionsButton;

        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------

        private void Start()
        {
            Hide();

            GameManager.Singleton.onGamePaused += GameManger_OnGamePaused;
            GameManager.Singleton.onGameUnpaused += GameManager_OnGameUnpaused;

            mainMenuButton.onClick.AddListener(MainMenuButton_OnClick);
            resumeButton.onClick.AddListener(ResumeButton_OnClick);
            optionsButton.onClick.AddListener(OptionsButton_OnClick);
        }

        private void OnDestroy()
        {
            GameManager.Singleton.onGamePaused -= GameManger_OnGamePaused;
            GameManager.Singleton.onGameUnpaused -= GameManager_OnGameUnpaused;
        }

        // Public Methods ------------------------------------------------------



        // Private Methods -----------------------------------------------------

        private void Show()
        {
            gameObject.SetActive(true);
            resumeButton.Select();
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }

        // Event Handlers ------------------------------------------------------

        private void GameManger_OnGamePaused(object sender, EventArgs e)
        {
            Show();
        }

        private void GameManager_OnGameUnpaused(object sender, EventArgs e)
        {
            Hide();
        }

        private void MainMenuButton_OnClick()
        {
            Loader.Load(Scene.MainMenuScene);
        }

        private void ResumeButton_OnClick()
        {
            GameManager.Singleton.TogglePause();
        }

        private void OptionsButton_OnClick()
        {
            OptionsUI.Singleton.Show(Show);
            Hide();
        }
    }
}

