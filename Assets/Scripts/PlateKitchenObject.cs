using System;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------

    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

    public class PlateKitchenObject : KitchenObject
    {
        // Public Properties ---------------------------------------------------

        public event EventHandler<OnIngredientAddedEventArgs> onIngredientAdded;


        // Private Fields ------------------------------------------------------

        [SerializeField] private List<KitchenObjectSO> validKitchenObjects;

        private List<KitchenObjectSO> kitchenObjectSOs = new List<KitchenObjectSO>();

        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------



        // Public Methods ------------------------------------------------------

        public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
        {
            if (!validKitchenObjects.Contains(kitchenObjectSO))
            {
                return false;
            }

            if (kitchenObjectSOs.Contains(kitchenObjectSO))
            {
                return false;
            }

            kitchenObjectSOs.Add(kitchenObjectSO);

            onIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs { kitchenObjectSO = kitchenObjectSO });
            return true;
        }

        public List<KitchenObjectSO> GetKitchenObjectSOs()
        {
            return new List<KitchenObjectSO>(kitchenObjectSOs);
        }


        // Private Methods -----------------------------------------------------



        // Event Handlers ------------------------------------------------------



    }
}
