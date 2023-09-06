using UnityEngine;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------



    public class ContainerCounterVisual : MonoBehaviour
    {
        // Public Properties ---------------------------------------------------



        // Private Fields ------------------------------------------------------

        [SerializeField] private ContainerCounter containerCounter;

        private Animator animator;
        private const string OPEN_CLOSE = "OpenClose";


        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            containerCounter.onPlayerGrabObject += ContainerCounter_OnPlayerGrabObject;
        }

        private void OnDisable()
        {
            containerCounter.onPlayerGrabObject -= ContainerCounter_OnPlayerGrabObject;
        }


        // Public Methods ------------------------------------------------------



        // Private Methods -----------------------------------------------------



        // Event Handlers ------------------------------------------------------


        private void ContainerCounter_OnPlayerGrabObject(object sender, System.EventArgs e)
        {
            animator.SetTrigger(OPEN_CLOSE);
        }

    }
}

