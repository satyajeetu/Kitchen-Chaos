using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------



    public class MainMenuUI : MonoBehaviour
    {
        // Public Properties ---------------------------------------------------



        // Private Fields ------------------------------------------------------

        [SerializeField] private Button playButton;
        [SerializeField] private Button quitButton;

        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------

        private void OnEnable()
        {
            Time.timeScale = 1;

            playButton.Select();

            playButton.onClick.AddListener(() =>
            {
                Loader.Load(Scene.GameScene);
            });

            quitButton.onClick.AddListener(() =>
            {
                Application.Quit();
            });
        }


        // Public Methods ------------------------------------------------------



        // Private Methods -----------------------------------------------------



        // Event Handlers ------------------------------------------------------



    }
}
