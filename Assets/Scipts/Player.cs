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
        [SerializeField] private GameInputs gameInputs;

        private bool isWalking;


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
            isWalking = moveDir != Vector2.zero;
            Vector3 moveDirVec3 = new(moveDir.x, 0, moveDir.y);
            transform.position += moveSpeed * Time.deltaTime * moveDirVec3;
            transform.forward = Vector3.Slerp(transform.forward, moveDirVec3, Time.deltaTime * rotationSpeed);
        }

        // Event Handlers ------------------------------------------------------



    }
}
