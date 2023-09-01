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

        [SerializeField] private CuttingCounter cuttingCounter;
        [SerializeField] private Image barImage;
        [SerializeField] private GameObject progressBarPanel;


        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------

        private void Start()
        {
            Debug.Log("Start");
            Hide();
            barImage.fillAmount = 0.0f;
        }

        private void OnEnable()
        {
            Debug.Log("Enabled");
            cuttingCounter.onProgressChanged += CuttingCounter_onProgresseChanged;
        }


        private void OnDisable()
        {
            cuttingCounter.onProgressChanged -= CuttingCounter_onProgresseChanged;
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

        private void CuttingCounter_onProgresseChanged(object sender, OnProgressChagnedEventArgs e)
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

