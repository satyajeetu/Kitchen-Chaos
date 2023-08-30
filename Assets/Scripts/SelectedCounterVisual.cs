using System;
using UnityEngine;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------



    public class SelectedCounterVisual : MonoBehaviour
    {
        // Public Properties ---------------------------------------------------



        // Private Fields ------------------------------------------------------

        [SerializeField] private ClearCounter clearCounter;
        [SerializeField] private GameObject selectedCounterVisual;

        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------

        private void OnEnable()
        {
            Player.Singleton.onSelectedCounterChanged += Player_OnSelectedCounterChanged;
        }

        private void OnDisable()
        {
            Player.Singleton.onSelectedCounterChanged += Player_OnSelectedCounterChanged;
        }

        // Public Methods ------------------------------------------------------



        // Private Methods -----------------------------------------------------



        // Event Handlers ------------------------------------------------------

        private void Player_OnSelectedCounterChanged(object sender, OnSelectedCounterChangedEventArgs e)
        {
            selectedCounterVisual.SetActive(e.selectedCounter == clearCounter);
        }

    }
}

