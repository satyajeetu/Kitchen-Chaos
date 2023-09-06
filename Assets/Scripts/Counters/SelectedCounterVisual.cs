using System;
using UnityEngine;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------



    public class SelectedCounterVisual : MonoBehaviour
    {
        // Public Properties ---------------------------------------------------



        // Private Fields ------------------------------------------------------

        [SerializeField] private BaseCounter baseCounter;
        [SerializeField] private GameObject[] selectedCounterVisuals;

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
            foreach (var visual in selectedCounterVisuals)
            {
                visual.SetActive(e.selectedCounter == baseCounter);
            }
        }

    }
}

