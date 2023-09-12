using UnityEngine;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------



    public class StoveBurnFlashingBarUI : MonoBehaviour
    {
        // Public Properties ---------------------------------------------------



        // Private Fields ------------------------------------------------------

        [SerializeField] private StoveCounter stoveCounter;

        private Animator animator;

        private const string IS_FLASHING = "IsFlashing";

        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------

        private void Start()
        {
            animator = GetComponent<Animator>();
            animator.SetBool(IS_FLASHING, false);

            stoveCounter.onProgressChanged += StoveCounter_onProgressChanged;
        }

        private void OnDestroy()
        {
            stoveCounter.onProgressChanged -= StoveCounter_onProgressChanged;
        }

        // Public Methods ------------------------------------------------------



        // Private Methods -----------------------------------------------------


        // Event Handlers ------------------------------------------------------


        private void StoveCounter_onProgressChanged(object sender, OnProgressChagnedEventArgs e)
        {
            float burningShowProgressAmount = 0.5f;
            bool show = stoveCounter.IsFried() && e.progressNormalized >= burningShowProgressAmount;

            animator.SetBool(IS_FLASHING, show);
        }
    }
}

