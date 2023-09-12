using System;
using UnityEngine;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------



    public class PlatesCounter : BaseCounter
    {
        // Public Properties ---------------------------------------------------

        public event EventHandler onPlateSpawned;
        public event EventHandler onPlateRemoved;

        // Private Fields ------------------------------------------------------

        [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

        private float spawnPlateTimer;
        private const float SPAWN_PLATE_TIMER_MAX = 4.0f ;

        private int platesSpawnedAmount;
        private int platesSpawnedAmountMax = 4;

        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------

        private void Update()
        {
            spawnPlateTimer += Time.deltaTime;

            if (GameManager.Singleton.IsGamePlaying()
                && spawnPlateTimer > SPAWN_PLATE_TIMER_MAX)
            {
                spawnPlateTimer = 0;

                if (platesSpawnedAmount < platesSpawnedAmountMax)
                {
                    platesSpawnedAmount++;

                    onPlateSpawned?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        // Public Methods ------------------------------------------------------

        public override void Interact(Player player)
        {
            if (player.HasKitchenObject())
            {
                return;
            }

            if (platesSpawnedAmount > 0)
            {
                platesSpawnedAmount--;

                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                onPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }


        // Private Methods -----------------------------------------------------



        // Event Handlers ------------------------------------------------------

    }
}
