using UnityEngine;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------



    public class KitchenObject : MonoBehaviour
    {
        // Public Properties ---------------------------------------------------



        // Private Fields ------------------------------------------------------

        [SerializeField] private KitchenObjectSO kitchenObjectSO;

        private IKitchenObjectParent kitchenObjectParent;

        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------



        // Public Methods ------------------------------------------------------

        public KitchenObjectSO GetKitchenObjectSO()
        {
            return kitchenObjectSO;
        }

        public void SetKitchenObjectParent(IKitchenObjectParent kitchenObject)
        {
            if (this.kitchenObjectParent != null)
            {
                this.kitchenObjectParent.ClearKitchenObject();
            }

            this.kitchenObjectParent = kitchenObject;

            if (kitchenObject.HasKitchenObject())
            {
                Debug.LogError("Counter already has a KitchenObject!");
            }

            this.kitchenObjectParent.SetKitchenObject(this);

            transform.parent = kitchenObject.GetKitchenObjectFollowTransform();
            transform.localPosition = Vector3.zero;
        }

        public IKitchenObjectParent ClearKitchenObjectParent()
        {
            return kitchenObjectParent;
        }


        // Private Methods -----------------------------------------------------



        // Event Handlers ------------------------------------------------------



    }
}

