using UnityEngine;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------



    public class ClearCounter: BaseCounter, IKitchenObjectParent
    {
        // Public Properties ---------------------------------------------------



        // Private Fields ------------------------------------------------------

        [SerializeField] private KitchenObjectSO kitchenObjectSO;


        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------



        // Public Methods ------------------------------------------------------

        public override void Interact(Player player)
        {
            if (!HasKitchenObject())
            {
                if (player.HasKitchenObject())
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
            }
            else
            {
                if (player.HasKitchenObject())
                {
                    GetKitchenObject().SetKitchenObjectParent(player);
                }
            }
        }

        // Private Methods -----------------------------------------------------



        // Event Handlers ------------------------------------------------------



    }
}
