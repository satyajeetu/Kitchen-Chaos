using UnityEngine;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------



    public class GameInputs : MonoBehaviour
    {
        // Public Properties ---------------------------------------------------



        // Private Fields ------------------------------------------------------

        private PlayerInputActions inputActions;


        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------

        private void Awake()
        {
            inputActions = new PlayerInputActions();
        }

        private void OnEnable()
        {
            inputActions.Player.Enable();
        }

        private void OnDisable()
        {
            inputActions.Player.Disable();
        }


        // Public Methods ------------------------------------------------------

        public Vector2 GetMovementVectorNormalized()
        {
            Vector2 inputVector = inputActions.Player.Move.ReadValue<Vector2>();
            inputVector.Normalize();
            return inputVector;
        }


        // Private Methods -----------------------------------------------------



        // Event Handlers ------------------------------------------------------



    }
}

