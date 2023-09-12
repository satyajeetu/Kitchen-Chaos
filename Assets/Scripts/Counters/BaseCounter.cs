using System;
using UnityEngine;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------



    public abstract class BaseCounter : MonoBehaviour, IKitchenObjectParent
    {


        // Public Properties ---------------------------------------------------

        public static event EventHandler onAnyObjectPlacedHere;


        // Private Fields ------------------------------------------------------

        [SerializeField] private Transform counterTop;

        private KitchenObject kitchenObject;


        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------



        // Public Methods ------------------------------------------------------

        public abstract void Interact(Player player);

        public Transform GetKitchenObjectFollowTransform()
        {
            return counterTop;
        }

        public void SetKitchenObject(KitchenObject kitchenObject)
        {
            this.kitchenObject = kitchenObject;

            if (kitchenObject != null)
            {
                onAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
            }
        }

        public KitchenObject GetKitchenObject()
        {
            return kitchenObject;
        }

        public void ClearKitchenObject()
        {
            kitchenObject = null;
        }

        public bool HasKitchenObject()
        {
            return kitchenObject != null;
        }

        public virtual void InteractAlternate(Player player)
        {
        }

        public static void ResetStaticData()
        {
            onAnyObjectPlacedHere = null;
        }

        // Private Methods -----------------------------------------------------



        // Event Handlers ------------------------------------------------------
    }
}

