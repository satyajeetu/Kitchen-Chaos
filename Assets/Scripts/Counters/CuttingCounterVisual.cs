using UnityEngine;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------



    public class CuttingCounterVisual : MonoBehaviour
    {
        // Public Properties ---------------------------------------------------



        // Private Fields ------------------------------------------------------

        [SerializeField] private CuttingCounter cuttingCounter;

        private Animator animator;
        private const string CUT = "Cut";


        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            cuttingCounter.onCut += CuttingCounter_OnCut;
        }

        private void OnDisable()
        {
            cuttingCounter.onCut -= CuttingCounter_OnCut;
        }


        // Public Methods ------------------------------------------------------



        // Private Methods -----------------------------------------------------



        // Event Handlers ------------------------------------------------------


        private void CuttingCounter_OnCut(object sender, System.EventArgs e)
        {
            animator.SetTrigger(CUT);
        }

    }
}

