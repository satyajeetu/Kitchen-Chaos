using UnityEngine;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------



    public class StoveCounterSound : MonoBehaviour
    {
        // Public Properties ---------------------------------------------------



        // Private Fields ------------------------------------------------------

        [SerializeField] private StoveCounter stoveCounter;

        private AudioSource audioSource;

        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            stoveCounter.OnStoveStateChanged += StoveCounter_OnStoveStateChanged;
        }

        private void OnDisable()
        {
            stoveCounter.OnStoveStateChanged -= StoveCounter_OnStoveStateChanged;
        }

        // Public Methods ------------------------------------------------------



        // Private Methods -----------------------------------------------------



        // Event Handlers ------------------------------------------------------

        private void StoveCounter_OnStoveStateChanged(object sender, OnStoveStateChangeEventArgs e)
        {
            bool playSound = (e.currentState == StoveState.FRYING || e.currentState == StoveState.FIRED);

            if (playSound)
            {
                audioSource.Play();
            }
            else
            {
                audioSource.Pause();
            }
        }


    }
}
