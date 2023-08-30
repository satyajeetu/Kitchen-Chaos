using UnityEngine;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------



    public class PlayerAnimator : MonoBehaviour
    {
        // Public Properties ---------------------------------------------------



        // Private Fields ------------------------------------------------------

        [SerializeField] private Player player;

        private Animator animator;

        private const string IS_WALKING = "IsWalking";


        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            UpdateAnimator();
        }

        // Public Methods ------------------------------------------------------



        // Private Methods -----------------------------------------------------

        private void UpdateAnimator()
        {
            animator.SetBool(IS_WALKING, player.IsWalking());
        }


        // Event Handlers ------------------------------------------------------



    }
}

