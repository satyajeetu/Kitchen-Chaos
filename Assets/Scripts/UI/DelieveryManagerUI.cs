using UnityEngine;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------



    public class DelieveryManagerUI : MonoBehaviour
    {
        // Public Properties ---------------------------------------------------



        // Private Fields ------------------------------------------------------

        [SerializeField] private Transform container;
        [SerializeField] private Transform recipieTemplate;


        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------

        private void Awake()
        {
            recipieTemplate.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            DeliveryManager.Singleton.onRecipieCompleted += DeliveryManager_onRecipieCompleted;
            DeliveryManager.Singleton.onRecipieSpawned += DeliveryManager_onRecipieCompleted;
        }

        private void OnDisable()
        {
            DeliveryManager.Singleton.onRecipieCompleted -= DeliveryManager_onRecipieCompleted;
            DeliveryManager.Singleton.onRecipieSpawned -= DeliveryManager_onRecipieCompleted;
        }

        // Public Methods ------------------------------------------------------



        // Private Methods -----------------------------------------------------

        private void UpdateVisual()
        {
            foreach (Transform child in container)
            {
                if (child == recipieTemplate) continue;
                Destroy(child.gameObject);
            }

            foreach (RecipieSO recipieSO in DeliveryManager.Singleton.GetWaitingRecipieSOList())
            {
                Transform recipieTransform = Instantiate(recipieTemplate, container);
                recipieTransform.gameObject.SetActive(true);
                recipieTransform.GetComponent<DeliverManagerSingleUI>().SetRecipieSO(recipieSO);
            }
        }

        // Event Handlers ------------------------------------------------------

        private void DeliveryManager_onRecipieCompleted(object sender, System.EventArgs e)
        {
            UpdateVisual();
        }

    }
}

