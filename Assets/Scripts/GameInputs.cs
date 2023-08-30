using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------


    public class GameInputs : MonoBehaviour
    {
        // Public Properties ---------------------------------------------------

        public event EventHandler onInteractAction;

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

            inputActions.Player.Interact.performed += InputActions_OnInteractPerformed;
        }

        private void OnDisable()
        {
            inputActions.Player.Disable();

            inputActions.Player.Interact.performed -= InputActions_OnInteractPerformed;
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

        private void InputActions_OnInteractPerformed(InputAction.CallbackContext context)
        {
            onInteractAction?.Invoke(this, EventArgs.Empty);
        }
    }
}

