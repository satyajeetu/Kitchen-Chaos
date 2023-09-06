using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------



    public class DeliverManagerSingleUI : MonoBehaviour
    {
        // Public Properties ---------------------------------------------------



        // Private Fields ------------------------------------------------------

        [SerializeField] private TMP_Text recipieNameText;
        [SerializeField] private Transform iconContainer;
        [SerializeField] private Transform imageTemplate;


        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------

        private void Awake()
        {
            imageTemplate.gameObject.SetActive(false);
        }


        // Public Methods ------------------------------------------------------

        public void SetRecipieSO(RecipieSO recipieSO)
        {
            recipieNameText.text = recipieSO.name;


            foreach (KitchenObjectSO kitchenObjectSO in recipieSO.kitchenObjectSOs)
            {
                Transform currentImage = Instantiate(imageTemplate, iconContainer);
                currentImage.gameObject.SetActive(true);
                currentImage.GetComponent<Image>().sprite = kitchenObjectSO.iconSprite;
            }
        }


        // Private Methods -----------------------------------------------------



        // Event Handlers ------------------------------------------------------



    }
}

