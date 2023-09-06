using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------

    [System.Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }

    public class PlateCompleteVisual : MonoBehaviour
    {
        // Public Properties ---------------------------------------------------



        // Private Fields ------------------------------------------------------

        [SerializeField] PlateKitchenObject plateKitchenObject;
        [SerializeField] List<KitchenObjectSO_GameObject> kitchenObjectSO_GameObjects;

        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------

        private void Awake()
        {
            foreach (var kitcheObjectData in kitchenObjectSO_GameObjects)
            {
                 kitcheObjectData.gameObject.SetActive(false);
            }
        }
        private void OnEnable()
        {
            plateKitchenObject.onIngredientAdded += PlateKitchenObject_onIngredientAdded;
        }

        private void OnDisable()
        {
            plateKitchenObject.onIngredientAdded -= PlateKitchenObject_onIngredientAdded;
        }

        // Public Methods ------------------------------------------------------



        // Private Methods -----------------------------------------------------



        // Event Handlers ------------------------------------------------------

        private void PlateKitchenObject_onIngredientAdded(object sender, OnIngredientAddedEventArgs e)
        {
            foreach (var kitcheObjectData in kitchenObjectSO_GameObjects)
            {
                if (e.kitchenObjectSO == kitcheObjectData.kitchenObjectSO)
                {
                    kitcheObjectData.gameObject.SetActive(true);
                }
            }
        }

    }
}
