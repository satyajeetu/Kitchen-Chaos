using System;
using UnityEngine;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------



    public class ContainerCounter : BaseCounter
    {

        // Public Properties ---------------------------------------------------

        public event EventHandler onPlayerGrabObject;


        // Private Fields ------------------------------------------------------

        [SerializeField] private KitchenObjectSO kitchenObjectSO;


        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------



        // Public Methods ------------------------------------------------------

        public override void Interact(Player player)
        {
            if (player.HasKitchenObject())
            {
                return;
            }
            if (!HasKitchenObject())
            {
                KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
                onPlayerGrabObject?.Invoke(this, EventArgs.Empty);
            }
        }

        // Private Methods -----------------------------------------------------



        // Event Handlers ------------------------------------------------------



    }
}

