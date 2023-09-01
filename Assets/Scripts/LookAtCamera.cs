using UnityEngine;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------

    [System.Serializable]
    public enum LookMode
    {
        LOOK_AT,
        LOOK_AT_INVERTED,
        CAMERA_FORWARD,
        CAMERA_FORWARD_INVERTED
    }

    public class LookAtCamera : MonoBehaviour
    {
        // Public Properties ---------------------------------------------------



        // Private Fields ------------------------------------------------------

        [SerializeField] private LookMode lookMode;

        private Transform cameraTransform;

        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------

        private void Awake()
        {
            cameraTransform = Camera.main.transform;
        }

        private void LateUpdate()
        {
            switch (lookMode)
            {
                case LookMode.LOOK_AT:
                    transform.LookAt(cameraTransform);
                    break;

                case LookMode.LOOK_AT_INVERTED:
                    Vector3 dirFromCamera = transform.position - cameraTransform.position;
                    transform.LookAt(transform.position + dirFromCamera);
                    break;

                case LookMode.CAMERA_FORWARD:
                    transform.forward = cameraTransform.forward;
                    break;

                case LookMode.CAMERA_FORWARD_INVERTED:
                    transform.forward = -cameraTransform.forward;
                    break;
            }
        }

        // Public Methods ------------------------------------------------------



        // Private Methods -----------------------------------------------------



        // Event Handlers ------------------------------------------------------



    }
}
