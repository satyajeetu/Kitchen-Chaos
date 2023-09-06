using UnityEngine;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------



    public class StoveCounterVisual : MonoBehaviour
    {
        // Public Properties ---------------------------------------------------



        // Private Fields ------------------------------------------------------

        [SerializeField] private GameObject[] stoveBurningObjects;
        [SerializeField] private StoveCounter stoveCounter;

        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------

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
            bool showVisual = e.currentState == StoveState.FRYING || e.currentState == StoveState.FIRED;

            foreach (GameObject stoveBurnObject in stoveBurningObjects)
            {
                stoveBurnObject.SetActive(showVisual);
            }
        }

    }
}
