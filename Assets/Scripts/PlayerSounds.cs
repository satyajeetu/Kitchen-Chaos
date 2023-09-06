using UnityEngine;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------



    public class PlayerSounds : MonoBehaviour
    {
        // Public Properties ---------------------------------------------------



        // Private Fields ------------------------------------------------------

        private Player player;

        private float footstepTimer;
        private float footstepTimerMax = 0.1f;

        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------

        private void Awake()
        {
            player = GetComponent<Player>();
        }

        private void Update()
        {
            footstepTimer -= Time.deltaTime;

            if (footstepTimer < 0)
            {
                footstepTimer = footstepTimerMax;

                if (player.IsWalking())
                SoundManger.Singleton.PlayFootstepsSound(transform);
            }
        }

        // Public Methods ------------------------------------------------------



        // Private Methods -----------------------------------------------------



        // Event Handlers ------------------------------------------------------



    }
}
