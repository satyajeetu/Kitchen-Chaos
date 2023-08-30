using UnityEngine;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------



    public class Player : MonoBehaviour
    {
        // Public Properties ---------------------------------------------------



        // Private Fields ------------------------------------------------------

        [SerializeField] private float moveSpeed = 7f;
        [SerializeField] private float rotationSpeed = 10f;
        [SerializeField] private float playerSize = 0.7f;
        [SerializeField] private GameInputs gameInputs;

        private bool isWalking;
        private Vector3 stomachHeight = new (0, 0.3f, 0f);

        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------

        private void Update()
        {
            MovePlayer();
        }

        // Public Methods ------------------------------------------------------

        public bool IsWalking()
        {
            return isWalking;
        }


        // Private Methods -----------------------------------------------------

        private void MovePlayer()
        {
            Vector2 moveDir = gameInputs.GetMovementVectorNormalized();
            Vector3 moveDirVec3 = new(moveDir.x, 0, moveDir.y);

            isWalking = moveDir != Vector2.zero;

            Vector3 startPos = transform.position + stomachHeight;
            bool canMove = !Physics.Raycast(startPos, moveDirVec3, playerSize);

            if (canMove)
            {
                transform.position += moveSpeed * Time.deltaTime * moveDirVec3;
            }

            transform.forward = Vector3.Slerp(transform.forward, moveDirVec3, Time.deltaTime * rotationSpeed);
        }

        // Event Handlers ------------------------------------------------------



    }
}
