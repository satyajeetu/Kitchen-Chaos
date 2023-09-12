using System;
using TMPro;
using UnityEngine;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------



    public class GameStartCountdownUI : MonoBehaviour
    {

        // Public Properties ---------------------------------------------------



        // Private Fields ------------------------------------------------------

        [SerializeField] private TMP_Text countdownText;

        private Animator animator;
        private int prevCountdownNumber = -1;
        private const string ANIMATOR_NUMBER_POP_UP = "NumberPopUp";

        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------

        private void Awake()
        {
            animator = GetComponent<Animator>();
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

        private void Update()
        {
            int countDownNumber = Mathf.CeilToInt(GameManager.Singleton.GetCountdownToStartTimer());
            countdownText.text = countDownNumber.ToString();

            if (prevCountdownNumber != countDownNumber)
            {
                prevCountdownNumber = countDownNumber;
                animator.SetTrigger(ANIMATOR_NUMBER_POP_UP);
                SoundManger.Singleton.PlayCountDownSound();
            }
        }

        // Public Methods ------------------------------------------------------



        // Private Methods -----------------------------------------------------

        private void Hide()
        {
            countdownText.gameObject.SetActive(false);
        }

        private void Show()
        {
            countdownText.gameObject.SetActive(true);
        }

        // Event Handlers ------------------------------------------------------

        private void GameManager_OnGameStateChanged(object sender, EventArgs e)
        {
            if (GameManager.Singleton.IsCountdownToStartActive())
            {
                Show();
            }
            else
            {
                Hide();
            }
        }
    }
}
