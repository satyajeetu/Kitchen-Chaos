using System;
using UnityEngine;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------

    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [DefaultExecutionOrder(-1)]
    public class Player : MonoBehaviour, IKitchenObjectParent
    {
        // Public Properties ---------------------------------------------------

        public static Player Singleton { get; private set; }

        public event EventHandler onPickSomething;
        public event EventHandler<OnSelectedCounterChangedEventArgs> onSelectedCounterChanged;


        // Private Fields ------------------------------------------------------

        [SerializeField] private float moveSpeed = 7f;
        [SerializeField] private GameInputs gameInputs;
        [SerializeField] private LayerMask counterLayerMask;
        [SerializeField] private Transform kitcheObjectHoldPoint;

        private float rotationSpeed = 10f;
        private float playerRadius = 0.7f;
        private float playerHeight = 2.0f;

        private bool isWalking;
        private Vector3 lastInteractDir;
        private BaseCounter selectedCounter;
        private KitchenObject kitchenObject;

        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------

        private void Awake()
        {
            if (Singleton != null)
            {
                Debug.LogError("Singleton is getting created again");
            }

            Singleton = this;
        }

        private void OnEnable()
        {
            gameInputs.onInteractAlternateAction += GameInputs_OnInteractAlternateAction;
            gameInputs.onInteractAction += GameInputs_OnInteractAction;
        }

        private void OnDisable()
        {
            gameInputs.onInteractAction -= GameInputs_OnInteractAction;
            gameInputs.onInteractAlternateAction -= GameInputs_OnInteractAlternateAction;
        }

        private void Update()
        {
            HandleMovement();
            HandleInteraction();
        }

        // Public Methods ------------------------------------------------------

        public bool IsWalking()
        {
            return isWalking;
        }

        public Transform GetKitchenObjectFollowTransform()
        {
            return kitcheObjectHoldPoint;
        }

        public void SetKitchenObject(KitchenObject kitchenObject)
        {
            this.kitchenObject = kitchenObject;

            if (kitchenObject != null)
            {
                onPickSomething?.Invoke(this, EventArgs.Empty);
            }
        }

        public KitchenObject GetKitchenObject()
        {
            return kitchenObject;
        }

        public void ClearKitchenObject()
        {
            kitchenObject = null;
        }

        public bool HasKitchenObject()
        {
            return kitchenObject != null;
        }

        // Private Methods -----------------------------------------------------

        private void HandleMovement()
        {
            Vector2 moveDir = gameInputs.GetMovementVectorNormalized();
            Vector3 moveDirVec3 = new(moveDir.x, 0, moveDir.y);

            isWalking = moveDir != Vector2.zero;

            float moveDistance = moveSpeed * Time.deltaTime;
            Vector3 startPos = transform.position;
            Vector3 endPos = transform.position + playerHeight * Vector3.up;
            bool canMove = !Physics.CapsuleCast(startPos, endPos, playerRadius, moveDirVec3, moveDistance);

            if (!canMove)   // If cannot move try sliding to left or right
            {
                Vector3 moveDirX = new Vector3(moveDirVec3.x, 0, 0).normalized;
                canMove = moveDirVec3.x != 0 && !Physics.CapsuleCast(startPos, endPos, playerRadius, moveDirX, moveDistance);

                if (canMove)    // Can slide to only X movement
                {
                    moveDirVec3 = moveDirX;
                }
                else    // If cannot move in X Direction
                {
                    Vector3 moveDirZ = new Vector3(0, 0, moveDirVec3.z).normalized;
                    canMove = moveDirVec3.z != 0 && !Physics.CapsuleCast(startPos, endPos, playerRadius, moveDirZ, moveDistance);

                    if (canMove)    // If can move in Z direction
                    {
                        moveDirVec3 = moveDirZ;
                    }
                }
            }

            if (canMove)    // If can move at all
            {
                transform.position += moveDistance * moveDirVec3;
            }

            transform.forward = Vector3.Slerp(transform.forward, moveDirVec3, Time.deltaTime * rotationSpeed);
        }


        private void HandleInteraction()
        {
            Vector2 moveDir = gameInputs.GetMovementVectorNormalized();
            Vector3 moveDirVec3 = new(moveDir.x, 0f, moveDir.y);

            lastInteractDir = moveDirVec3 == Vector3.zero ? lastInteractDir : moveDirVec3;

            float interactDistance = 2f;

            if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycast, interactDistance, counterLayerMask))
            {
                if (raycast.transform.TryGetComponent(out BaseCounter baseCounter))
                {
                    if (selectedCounter != baseCounter)
                    {
                        selectedCounter = baseCounter;
                        SetSelectedCounter(selectedCounter);
                    }
                }
                else
                {
                    SetSelectedCounter(null);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }

        private void SetSelectedCounter(BaseCounter selectedCounter)
        {
            this.selectedCounter = selectedCounter;
            onSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { selectedCounter = this.selectedCounter });
        }

        // Event Handlers ------------------------------------------------------

        private void GameInputs_OnInteractAction(object sender, EventArgs args)
        {
            if (!GameManager.Singleton.IsGamePlaying())
            {
                return;
            }

            if (selectedCounter == null)
            {
                return;
            }

            selectedCounter.Interact(this);
        }

        private void GameInputs_OnInteractAlternateAction(object sender, EventArgs e)
        {
            if (!GameManager.Singleton.IsGamePlaying())
            {
                return;
            }

            if (selectedCounter == null)
            {
                return;
            }

            selectedCounter.InteractAlternate(this);
        }
    }
}
