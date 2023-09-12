using System;
using TMPro;
using UnityEngine;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------



    public class GameOverUI : MonoBehaviour
    {
        // Public Properties ---------------------------------------------------



        // Private Fields ------------------------------------------------------

        [SerializeField] private TMP_Text recipiesDeliveredText;
        [SerializeField] private GameObject gameOverUI;

        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------

        private void Awake()
        {
            Hide();
        }

        private void OnEnable()
        {
            GameManager.Singleton.onStateChanged += GameManager_OnGameStateChanged;
        }

        private void OnDisable()
        {
            GameManager.Singleton.onStateChanged -= GameManager_OnGameStateChanged;
        }

        // Public Methods ------------------------------------------------------



        // Private Methods -----------------------------------------------------

        private void Hide()
        {
            gameOverUI.gameObject.SetActive(false);
        }

        private void Show()
        {
            gameOverUI.gameObject.SetActive(true);
        }

        // Event Handlers ------------------------------------------------------

        private void GameManager_OnGameStateChanged(object sender, EventArgs e)
        {
            if (GameManager.Singleton.IsGameOverActive())
            {
                recipiesDeliveredText.text = DeliveryManager.Singleton.GetSuccessfulRecipieAmount().ToString();

                Show();
            }
            else
            {
                Hide();
            }
        }
    }
}
