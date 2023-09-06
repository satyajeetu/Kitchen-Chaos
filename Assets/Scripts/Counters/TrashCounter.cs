using System;
using UnityEngine;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------



    public class TrashCounter : BaseCounter
    {
        // Public Properties ---------------------------------------------------

        public static event EventHandler onAnyObjectTrashed;


        // Private Fields ------------------------------------------------------



        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------



        // Public Methods ------------------------------------------------------

        public override void Interact(Player player)
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().DestroySelf();

                onAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
            }
        }


        // Private Methods -----------------------------------------------------



        // Event Handlers ------------------------------------------------------

    }
}
