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

        }

        // Private Methods -----------------------------------------------------



        // Event Handlers ------------------------------------------------------



    }
}
