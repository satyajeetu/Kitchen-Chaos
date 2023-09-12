using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------



    public class OptionsUI : MonoBehaviour
    {
        // Public Properties ---------------------------------------------------

        public static OptionsUI Singleton;


        // Private Fields ------------------------------------------------------

        [SerializeField] private Button closeButton;

        [SerializeField] private Button soundEffectsButton;
        [SerializeField] private TMP_Text soundeffectsText;

        [SerializeField] private Button musicButton;
        [SerializeField] private TMP_Text musicText;

        [SerializeField] private Transform rebindingPannel;

        [Space]
        [Header("KeyBindings")]
        [SerializeField] private TMP_Text moveUpText;
        [SerializeField] private Button moveUpButton;

        [SerializeField] private TMP_Text moveDownText;
        [SerializeField] private Button moveDownButton;

        [SerializeField] private TMP_Text moveRightText;
        [SerializeField] private Button moveRightButton;

        [SerializeField] private TMP_Text moveLeftText;
        [SerializeField] private Button moveLeftButton;

        [SerializeField] private TMP_Text interactText;
        [SerializeField] private Button interactButton;

        [SerializeField] private TMP_Text alternateInteractText;
        [SerializeField] private Button alternateInteractButton;

        private string SOUND_EFFECTS = "Sound Effects";
        private string MUSIC = "Music";

        private Action onCloseButtonAction;


        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------

        private void Awake()
        {
            Singleton = this;
            UpdateBindingsVisuals();
        }

        private void OnEnable()
        {
            GameManager.Singleton.onGameUnpaused += GameManager_OnGameUnpaused;
        }

        private void OnDisable()
        {
            GameManager.Singleton.onGameUnpaused -= GameManager_OnGameUnpaused;
        }

        private void Start()
        {
            soundeffectsText.text = UpdateVisuals(SOUND_EFFECTS, SoundManger.Singleton.GetGlobalVolume());
            musicText.text = UpdateVisuals(MUSIC, MusicManager.Singleton.GetGlobalVolume());

            soundEffectsButton.onClick.AddListener(() =>
            {
                SoundManger.Singleton.ChangeGlobalVolume();
                soundeffectsText.text = UpdateVisuals(SOUND_EFFECTS, SoundManger.Singleton.GetGlobalVolume());
            });

            musicButton.onClick.AddListener(() =>
            {
                MusicManager.Singleton.ChangeGlobalVolume();
                musicText.text = UpdateVisuals(MUSIC, MusicManager.Singleton.GetGlobalVolume());
            });

            closeButton.onClick.AddListener(() =>
            {
                Hide();
                onCloseButtonAction();
                GameManager.Singleton.TogglePause();
            });

            moveUpButton.onClick.AddListener(() => RebindBinding(Binding.MOVE_UP));
            moveDownButton.onClick.AddListener(() => RebindBinding(Binding.MOVE_DOWN));
            moveLeftButton.onClick.AddListener(() => RebindBinding(Binding.MOVE_LEFT));
            moveRightButton.onClick.AddListener(() => RebindBinding(Binding.MOVE_RIGHT));
            interactButton.onClick.AddListener(() => RebindBinding(Binding.INTERACT));
            alternateInteractButton.onClick.AddListener(() => RebindBinding(Binding.INTERACT_ALTERNATE));

            HideRebindingPanel();
            Hide();
        }


        // Public Methods ------------------------------------------------------

        public void Show(Action onCLoseButtonAction)
        {
            onCloseButtonAction = onCLoseButtonAction;
            gameObject.SetActive(true);
            soundEffectsButton.Select();
        }


        // Private Methods -----------------------------------------------------

        private string UpdateVisuals(string visual, float amount)
        {
            return visual + " : " + Mathf.Round(amount * 10f);
        }

        private void UpdateBindingsVisuals()
        {
            moveUpText.text = GameInputs.Singleton.GetBindingText(Binding.MOVE_UP);
            moveDownText.text = GameInputs.Singleton.GetBindingText(Binding.MOVE_DOWN);
            moveLeftText.text = GameInputs.Singleton.GetBindingText(Binding.MOVE_LEFT);
            moveRightText.text = GameInputs.Singleton.GetBindingText(Binding.MOVE_RIGHT);
            alternateInteractText.text = GameInputs.Singleton.GetBindingText(Binding.INTERACT_ALTERNATE);
            interactText.text = GameInputs.Singleton.GetBindingText(Binding.INTERACT);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }

        private void ShowRebindingPanel()
        {
            rebindingPannel.gameObject.SetActive(true);
        }

        private void HideRebindingPanel()
        {
            rebindingPannel.gameObject.SetActive(false);
        }

        private void RebindBinding(Binding binding)
        {
            ShowRebindingPanel();

            GameInputs.Singleton.RebindBinding(binding, () =>
            {
                UpdateBindingsVisuals();
                HideRebindingPanel();
            });
        }

        // Event Handlers ------------------------------------------------------

        private void GameManager_OnGameUnpaused(object sender, EventArgs e)
        {
            Hide();
        }
    }
}

