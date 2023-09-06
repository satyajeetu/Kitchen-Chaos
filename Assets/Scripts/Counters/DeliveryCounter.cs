using UnityEngine;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------



    public class DeliveryCounter : BaseCounter
    {
        // Public Properties ---------------------------------------------------



        // Private Fields ------------------------------------------------------



        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------



        // Public Methods ------------------------------------------------------

        public override void Interact(Player player)
        {
            if (player.HasKitchenObject())
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    DeliveryManager.Singleton.DeliverRecipie(plateKitchenObject);
                    player.GetKitchenObject().DestroySelf();
                }
            }
        }


        // Private Methods -----------------------------------------------------



        // Event Handlers ------------------------------------------------------

    }
}