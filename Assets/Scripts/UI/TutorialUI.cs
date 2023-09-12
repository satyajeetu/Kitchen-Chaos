using System;
using TMPro;
using UnityEngine;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------



    public class TutorialUI : MonoBehaviour
    {
        // Public Properties ---------------------------------------------------



        // Private Fields ------------------------------------------------------

        [Header("Keyboard")]
        [SerializeField] private TMP_Text keyMoveUpText;
        [SerializeField] private TMP_Text keyMoveDownText;
        [SerializeField] private TMP_Text keyMoveRightText;
        [SerializeField] private TMP_Text keyMoveLeftText;
        [SerializeField] private TMP_Text keyInteractText;
        [SerializeField] private TMP_Text keyAlternateInteractText;
        [SerializeField] private TMP_Text keyPauseText;

        [Header("Gamepad")]
        [SerializeField] private TMP_Text keyGamepadInteractText;
        [SerializeField] private TMP_Text keyGamepadAlternateInteractText;
        [SerializeField] private TMP_Text keyGamepadPauseText;


        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------

        private void Start()
        {
            GameInputs.Singleton.onInputBindingsChanged += GameInputs_OnInputBindingsChanged;
            GameManager.Singleton.onStateChanged += GameManager_OnGameStateChanged;
            Show();
            UpdateVisual();
        }

        private void OnDestroy()
        {
            GameInputs.Singleton.onInputBindingsChanged -= GameInputs_OnInputBindingsChanged;
            GameManager.Singleton.onStateChanged -= GameManager_OnGameStateChanged;
        }

        // Public Methods ------------------------------------------------------



        // Private Methods -----------------------------------------------------

        private void UpdateVisual()
        {
            keyMoveUpText.text = GameInputs.Singleton.GetBindingText(Binding.MOVE_UP);
            keyMoveDownText.text = GameInputs.Singleton.GetBindingText(Binding.MOVE_DOWN);
            keyMoveRightText.text = GameInputs.Singleton.GetBindingText(Binding.MOVE_RIGHT);
            keyMoveLeftText.text = GameInputs.Singleton.GetBindingText(Binding.MOVE_LEFT);

            keyInteractText.text = GameInputs.Singleton.GetBindingText(Binding.INTERACT);
            keyAlternateInteractText.text = GameInputs.Singleton.GetBindingText(Binding.INTERACT_ALTERNATE);
            keyPauseText.text = GameInputs.Singleton.GetBindingText(Binding.PAUSE);
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }

        // Event Handlers ------------------------------------------------------

        private void GameInputs_OnInputBindingsChanged(object sender, EventArgs e)
        {
            UpdateVisual();
        }

        private void GameManager_OnGameStateChanged(object sender, EventArgs e)
        {
            if (GameManager.Singleton.IsCountdownToStartActive())
            {
                Hide();
            }
        }
    }
}

