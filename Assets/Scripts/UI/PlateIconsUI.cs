using System;
using UnityEngine;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------



    public class PlateIconsUI : MonoBehaviour
    {
        // Public Properties ---------------------------------------------------



        // Private Fields ------------------------------------------------------

        [SerializeField] private PlateKitchenObject plateKitchenObject;
        [SerializeField] private Transform iconTemplate;

        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------

        private void Awake()
        {
            iconTemplate.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            plateKitchenObject.onIngredientAdded += PlateKitchenObject_OnIngredientAdded;
        }

        private void OnDisable()
        {
            plateKitchenObject.onIngredientAdded -= PlateKitchenObject_OnIngredientAdded;
        }


        // Public Methods ------------------------------------------------------



        // Private Methods -----------------------------------------------------

        private void UpdateVisuals()
        {
            foreach (Transform child in transform)
            {
                if (child == iconTemplate)
                {
                    continue;
                }

                Destroy(child.gameObject);
            }

            foreach (KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetKitchenObjectSOs())
            {
                Transform iconTransform = Instantiate(iconTemplate, transform);
                iconTransform.gameObject.SetActive(true);
                iconTransform.GetComponent<PlatesIconSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
            }
        }

        // Event Handlers ------------------------------------------------------

        private void PlateKitchenObject_OnIngredientAdded(object sender, OnIngredientAddedEventArgs e)
        {
            UpdateVisuals();
        }
    }
}