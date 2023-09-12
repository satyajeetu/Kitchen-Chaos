using System;
using UnityEngine;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------

    public enum GameState
    {
        WAITING_TO_START,
        COUNTDOWN_TO_START,
        GAME_PLAYING,
        GAME_OVER
    }

    [DefaultExecutionOrder(-1)]
    public class GameManager : MonoBehaviour
    {
        // Public Properties ---------------------------------------------------

        public static GameManager Singleton;

        public event EventHandler onStateChanged;
        public event EventHandler onGamePaused;
        public event EventHandler onGameUnpaused;

        // Private Fields ------------------------------------------------------

        private GameState gameState;

        private float countdownToStartTimer = 3;
        private float gamePlayingTimer;
        private float gamePlayingTimerMax = 10f;
        private bool isGamePaused = false;

        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------

        private void Awake()
        {
            Singleton = this;

            gameState = GameState.WAITING_TO_START;
        }

        private void OnEnable()
        {
            GameInputs.Singleton.onPauseButtonClicked += GameInputs_OnPauseButtonClicked;
            GameInputs.Singleton.onInteractAction += GameInputs_OnInteractAction;
        }

        private void OnDisable()
        {
            GameInputs.Singleton.onPauseButtonClicked -= GameInputs_OnPauseButtonClicked;
            GameInputs.Singleton.onInteractAction -= GameInputs_OnInteractAction;
        }

        private void Update()
        {
            switch (gameState)
            {
                case GameState.WAITING_TO_START:

                

                    break;

                case GameState.COUNTDOWN_TO_START:

                    countdownToStartTimer -= Time.deltaTime;

                    if (countdownToStartTimer < 0)
                    {
                        gameState = GameState.GAME_PLAYING;
                        onStateChanged?.Invoke(this, EventArgs.Empty);

                        gamePlayingTimer = gamePlayingTimerMax;
                    }

                    break;

                case GameState.GAME_PLAYING:

                    gamePlayingTimer -= Time.deltaTime;

                    if (gamePlayingTimer < 0)
                    {
                        gameState = GameState.GAME_OVER;
                        onStateChanged?.Invoke(this, EventArgs.Empty);
                    }

                    break;

                case GameState.GAME_OVER:


                    break;
            }

            // Debug.Log(gameState);
        }

        // Public Methods ------------------------------------------------------

        public bool IsGamePlaying()
        {
            return gameState == GameState.GAME_PLAYING;
        }

        public bool IsCountdownToStartActive()
        {
            return gameState == GameState.COUNTDOWN_TO_START;
        }

        public bool IsGameOverActive()
        {
            return gameState == GameState.GAME_OVER;
        }

        public float GetCountdownToStartTimer()
        {
            return countdownToStartTimer;
        }

        public float GetGameplayTimerNormalized()
        {
            // Debug.Log(gamePlayingTimerMax + " " + gamePlayingTimer);
            return 1 - gamePlayingTimer / gamePlayingTimerMax;
        }

        public void TogglePause()
        {
            if (!isGamePaused)
            {
                Time.timeScale = 0f;
                isGamePaused = true;
                onGamePaused?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                Time.timeScale = 1f;
                isGamePaused = false;
                onGameUnpaused?.Invoke(this, EventArgs.Empty);
            }
        }

        // Private Methods -----------------------------------------------------


        // Event Handlers ------------------------------------------------------

        private void GameInputs_OnPauseButtonClicked(object sender, EventArgs e)
        {
            TogglePause();
        }

        private void GameInputs_OnInteractAction(object sender, EventArgs e)
        {
            if (gameState == GameState.WAITING_TO_START)
            {
                gameState = GameState.COUNTDOWN_TO_START;
                onStateChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}