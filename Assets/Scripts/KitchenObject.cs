using UnityEngine;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------



    public class KitchenObject : MonoBehaviour
    {
        // Public Properties ---------------------------------------------------



        // Private Fields ------------------------------------------------------

        [SerializeField] private KitchenObjectSO kitchenObjectSO;

        private ClearCounter clearCounter;

        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------



        // Public Methods ------------------------------------------------------

        public KitchenObjectSO GetKitchenObjectSO()
        {
            return kitchenObjectSO;
        }

        public void SetClearCounter(ClearCounter clearCounter)
        {
            if (this.clearCounter != null)
            {
                this.clearCounter.ClearKitchenObject();
            }

            this.clearCounter = clearCounter;

            if (clearCounter.HasKitchenObject())
            {
                Debug.LogError("Counter already has a KitchenObject!");
            }

            this.clearCounter.SetKitchenObject(this);

            transform.parent = clearCounter.GetKitchenObjectFollowTransform();
            transform.localPosition = Vector3.zero;
        }

        public ClearCounter GetClearCounter()
        {
            return clearCounter;
        }


        // Private Methods -----------------------------------------------------



        // Event Handlers ------------------------------------------------------



    }
}

