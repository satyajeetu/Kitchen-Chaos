using UnityEngine;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------



    public class ClearCounter: MonoBehaviour
    {
        // Public Properties ---------------------------------------------------



        // Private Fields ------------------------------------------------------

        [SerializeField] private KitchenObjectSO kitchenObjectSO;
        [SerializeField] private Transform counterTop;

        private KitchenObject kitchenObject;

        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------



        // Public Methods ------------------------------------------------------

        public void Interact()
        {
            if (kitchenObject == null)
            {
                Transform kitcheObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTop);
                kitcheObjectTransform.GetComponent<KitchenObject>().SetClearCounter(this);
            }
            else
            {
                Debug.Log(kitchenObject.GetClearCounter());
            }
        }

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
