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

    public class GameManager : MonoBehaviour
    {
        // Public Properties ---------------------------------------------------

        public static GameManager Singleton;


        // Private Fields ------------------------------------------------------

        private GameState gameState;

        private float waitingToStartTimer = 1;
        private float countdownToStartTimer = 3;
        private float gamePlayingTimer = 10f;


        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------

        private void Awake()
        {
            Singleton = this;

            gameState = GameState.WAITING_TO_START;
        }

        private void Update()
        {
            switch (gameState)
            {
                case GameState.WAITING_TO_START:

                    waitingToStartTimer -= Time.deltaTime;

                    if (waitingToStartTimer < 0)
                    {
                        gameState = GameState.COUNTDOWN_TO_START;
                    }

                    break;

                case GameState.COUNTDOWN_TO_START:

                    countdownToStartTimer -= Time.deltaTime;

                    if (countdownToStartTimer < 0)
                    {
                        gameState = GameState.GAME_PLAYING;
                    }

                    break;

                case GameState.GAME_PLAYING:

                    gamePlayingTimer -= Time.deltaTime;

                    if (gamePlayingTimer < 0)
                    {
                        gameState = GameState.GAME_OVER;
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

        // Private Methods -----------------------------------------------------



        // Event Handlers ------------------------------------------------------



    }
}