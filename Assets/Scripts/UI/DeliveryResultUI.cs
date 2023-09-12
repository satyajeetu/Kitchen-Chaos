using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------



    public class DeliveryResultUI : MonoBehaviour
    {
        // Public Properties ---------------------------------------------------



        // Private Fields ------------------------------------------------------

        [SerializeField] private Image backgroundImage;
        [SerializeField] private Image iconImage;
        [SerializeField] private TMP_Text messageText;

        [SerializeField] private Color successColor;
        [SerializeField] private Color failedColor;
        [SerializeField] private Sprite successSprite;
        [SerializeField] private Sprite failedSprite;

        private Animator animator;

        private const string POPUP = "Popup";

        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------

        private void Start()
        {
            animator = GetComponent<Animator>();

            DeliveryManager.Singleton.onRecipieSuccess += DeliveryManager_OnRecipieSuccess;
            DeliveryManager.Singleton.onRecipieFailed += DeliveryManager_OnRecipieFailed;

            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            DeliveryManager.Singleton.onRecipieSuccess -= DeliveryManager_OnRecipieSuccess;
            DeliveryManager.Singleton.onRecipieFailed -= DeliveryManager_OnRecipieFailed;
        }


        // Public Methods ------------------------------------------------------



        // Private Methods -----------------------------------------------------



        // Event Handlers ------------------------------------------------------


        private void DeliveryManager_OnRecipieFailed(object sender, EventArgs e)
        {
            gameObject.SetActive(true);

            backgroundImage.color = failedColor;
            iconImage.sprite = failedSprite;
            messageText.text = "DELIVERY\nFAILED";

            animator.SetTrigger(POPUP);
        }

        private void DeliveryManager_OnRecipieSuccess(object sender, EventArgs e)
        {
            gameObject.SetActive(true);

            backgroundImage.color = successColor;
            iconImage.sprite = successSprite;
            messageText.text = "DELIVERY\nSUCCESS";

            animator.SetTrigger(POPUP);
        }

    }
}

