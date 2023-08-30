using System;
using UnityEngine;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------



    public abstract class BaseCounter : MonoBehaviour, IKitchenObjectParent
    {


        // Public Properties ---------------------------------------------------



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


        // Private Methods -----------------------------------------------------



        // Event Handlers ------------------------------------------------------
    }
}

