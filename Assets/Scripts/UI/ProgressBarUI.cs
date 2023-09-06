using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------



    public class ProgressBarUI : MonoBehaviour
    {
        // Public Properties ---------------------------------------------------



        // Private Fields ------------------------------------------------------

        [SerializeField] private GameObject hasProgressGameObject;
        [SerializeField] private Image barImage;
        [SerializeField] private GameObject progressBarPanel;

        private IHasProgress hasProgress;

        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------

        private void Awake()
        {
            hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();

            if (hasProgress == null)
            {
                Debug.LogError(gameObject + " no IHasProgress");
            }
        }

        private void Start()
        {
            Hide();
            barImage.fillAmount = 0.0f;
        }

        private void OnEnable()
        {
            hasProgress.onProgressChanged += HasProgress_OnProgressChanged;
        }


        private void OnDisable()
        {
            hasProgress.onProgressChanged -= HasProgress_OnProgressChanged;
        }


        // Public Methods ------------------------------------------------------



        // Private Methods -----------------------------------------------------

        private void Show()
        {
            progressBarPanel.SetActive(true);
        }

        private void Hide()
        {
            progressBarPanel.SetActive(false);
        }

        // Event Handlers ------------------------------------------------------

        private void HasProgress_OnProgressChanged(object sender, OnProgressChagnedEventArgs e)
        {
            barImage.fillAmount = e.progressNormalized;

            if (e.progressNormalized == 0f || e.progressNormalized == 1f)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }

    }
}

