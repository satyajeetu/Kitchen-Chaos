using UnityEngine;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------



    public class StoveBurnWariningUI : MonoBehaviour
    {
        // Public Properties ---------------------------------------------------



        // Private Fields ------------------------------------------------------

        [SerializeField] private StoveCounter stoveCounter;


        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------

        private void Start()
        {
            stoveCounter.onProgressChanged += StoveCounter_onProgressChanged;
            Hide();
        }

        private void OnDestroy()
        {
            stoveCounter.onProgressChanged -= StoveCounter_onProgressChanged;
        }

        // Public Methods ------------------------------------------------------



        // Private Methods -----------------------------------------------------

        private void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }

        // Event Handlers ------------------------------------------------------


        private void StoveCounter_onProgressChanged(object sender, OnProgressChagnedEventArgs e)
        {
            float burningShowProgressAmount = 0.5f;
            bool show = stoveCounter.IsFried() && e.progressNormalized >= burningShowProgressAmount;

            if (show)
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

