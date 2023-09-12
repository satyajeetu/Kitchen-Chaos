using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------



    public class GamePlayingClockUI : MonoBehaviour
    {
        // Public Properties ---------------------------------------------------



        // Private Fields ------------------------------------------------------

        [SerializeField] private Image timerImage;


        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------

        private void Update()
        {
            timerImage.fillAmount = GameManager.Singleton.GetGameplayTimerNormalized();
        }

        // Public Methods ------------------------------------------------------



        // Private Methods -----------------------------------------------------



        // Event Handlers ------------------------------------------------------



    }
}
