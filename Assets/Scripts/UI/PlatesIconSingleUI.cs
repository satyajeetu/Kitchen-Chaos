using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------



    public class PlatesIconSingleUI : MonoBehaviour
    {
        // Public Properties ---------------------------------------------------



        // Private Fields ------------------------------------------------------

        [SerializeField] private Image image;


        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------



        // Public Methods ------------------------------------------------------

        public void SetKitchenObjectSO(KitchenObjectSO kitchenObjectSO)
        {
            image.sprite = kitchenObjectSO.iconSprite;
        }


        // Private Methods -----------------------------------------------------



        // Event Handlers ------------------------------------------------------



    }
}