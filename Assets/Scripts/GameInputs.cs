using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------

    public enum Binding
    {
        MOVE_UP,
        MOVE_DOWN,
        MOVE_LEFT,
        MOVE_RIGHT,
        INTERACT,
        INTERACT_ALTERNATE,
        PAUSE
    }

    [DefaultExecutionOrder(-1)]
    public class GameInputs : MonoBehaviour
    {
        // Public Properties ---------------------------------------------------

        public static GameInputs Singleton;

        public event EventHandler onInteractAction;
        public event EventHandler onInteractAlternateAction;
        public event EventHandler onPauseButtonClicked;
        public event EventHandler onInputBindingsChanged;


        // Private Fields ------------------------------------------------------

        private PlayerInputActions inputActions;

        private const string PLAYER_PREFS_BINDINGS = "InputBindings";

        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------

        private void Awake()
        {
            Singleton = this;
            inputActions = new PlayerInputActions();

            if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
            {
                inputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
            }
        }

        private void OnEnable()
        {
            inputActions.Player.Enable();

            inputActions.Player.Interact.performed += InputActions_OnInteractPerformed;
            inputActions.Player.InteractAlternate.performed += InputActions_OnInteractAlternatePreformed;
            inputActions.Player.Pause.performed += InputActions_OnPauseClicked;
        }

        private void OnDisable()
        {
            inputActions.Player.Disable();

            inputActions.Player.Interact.performed -= InputActions_OnInteractPerformed;
            inputActions.Player.InteractAlternate.performed -= InputActions_OnInteractAlternatePreformed;
            inputActions.Player.Pause.performed -= InputActions_OnPauseClicked;
        }

        private void OnDestroy()
        {
            inputActions.Dispose();
        }

        // Public Methods ------------------------------------------------------

        public Vector2 GetMovementVectorNormalized()
        {
            Vector2 inputVector = inputActions.Player.Move.ReadValue<Vector2>();
            inputVector.Normalize();
            return inputVector;
        }

        public string GetBindingText(Binding binding)
        {
            switch (binding)
            {
                default:

                case Binding.INTERACT:
                    return inputActions.Player.Interact.bindings[0].ToDisplayString();

                case Binding.INTERACT_ALTERNATE:
                    return inputActions.Player.InteractAlternate.bindings[0].ToDisplayString();

                case Binding.MOVE_UP:
                    return inputActions.Player.Move.bindings[1].ToDisplayString();

                case Binding.MOVE_DOWN:
                    return inputActions.Player.Move.bindings[2].ToDisplayString();

                case Binding.MOVE_LEFT:
                    return inputActions.Player.Move.bindings[3].ToDisplayString();

                case Binding.MOVE_RIGHT:
                    return inputActions.Player.Move.bindings[4].ToDisplayString();

                case Binding.PAUSE:
                    return inputActions.Player.Pause.bindings[0].ToDisplayString();
            }
        }

        public void RebindBinding(Binding binding, Action onActionRebound)
        {
            inputActions.Player.Disable();

            if (binding == Binding.INTERACT)
            {
                inputActions.Player.Interact.PerformInteractiveRebinding(0)
                .OnComplete(callback =>
                {
                    callback.Dispose();
                    inputActions.Player.Enable();
                    onActionRebound();
                }).Start();
    
                return;
            }

            if (binding == Binding.INTERACT_ALTERNATE)
            {
                inputActions.Player.InteractAlternate.PerformInteractiveRebinding(0)
                .OnComplete(callback =>
                {
                    callback.Dispose();
                    inputActions.Player.Enable();
                    onActionRebound();
                }).Start();

                return;
            }

            int moveBinding = 0;

            switch (binding)
            {
                case Binding.MOVE_UP:
                    moveBinding = 1;
                    break;
                case Binding.MOVE_DOWN:
                    moveBinding = 2;
                    break;
                case Binding.MOVE_LEFT:
                    moveBinding = 3;
                    break;
                case Binding.MOVE_RIGHT:
                    moveBinding = 4;
                    break;
            }

            inputActions.Player.Move.PerformInteractiveRebinding(moveBinding)
                .OnComplete(callback =>
                {
                    callback.Dispose();
                    inputActions.Player.Enable();
                    onActionRebound();
                }).Start();

            PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, inputActions.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();

            onInputBindingsChanged?.Invoke(this, EventArgs.Empty);
        }

        // Private Methods -----------------------------------------------------



        // Event Handlers ------------------------------------------------------

        private void InputActions_OnInteractPerformed(InputAction.CallbackContext context)
        {
            onInteractAction?.Invoke(this, EventArgs.Empty);
        }

        private void InputActions_OnInteractAlternatePreformed(InputAction.CallbackContext context)
        {
            onInteractAlternateAction?.Invoke(this, EventArgs.Empty);
        }

        private void InputActions_OnPauseClicked(InputAction.CallbackContext context)
        {
            onPauseButtonClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}

