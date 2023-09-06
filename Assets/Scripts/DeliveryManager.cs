using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------


    [DefaultExecutionOrder(-1)]
    public class DeliveryManager : MonoBehaviour
    {
        // Public Properties ---------------------------------------------------

        public static DeliveryManager Singleton;

        public event EventHandler onRecipieSpawned;
        public event EventHandler onRecipieCompleted;
        public event EventHandler onRecipieSuccess;
        public event EventHandler onRecipieFailed;

        // Private Fields ------------------------------------------------------

        [SerializeField] private RecipieListSO recipieListSO;

        private List<RecipieSO> waitingRecipeSOList = new ();
        private float spawnRecipieTimer;
        private float spawnRecipieTimerMax = 4f;
        private int waitingRecipieMax = 4;

        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------

        private void Awake()
        {
            Singleton = this;
        }

        private void Update()
        {
            GenerateDeliveryRequirements();
        }

        // Public Methods ------------------------------------------------------

        public void DeliverRecipie(PlateKitchenObject plateKitchenObject)
        {
            CheckIfPlateHasRecipieFromWaitingList(plateKitchenObject);
        }

        public List<RecipieSO> GetWaitingRecipieSOList()
        {
            return new (waitingRecipeSOList);
        }

        // Private Methods -----------------------------------------------------

        private void GenerateDeliveryRequirements()
        {
            spawnRecipieTimer -= Time.deltaTime;

            if (spawnRecipieTimer < 0)
            {
                spawnRecipieTimer = spawnRecipieTimerMax;

                if (waitingRecipieMax > waitingRecipeSOList.Count)
                {
                    RecipieSO waitingRecipieSO = recipieListSO.RecipieSOs[Random.Range(0, recipieListSO.RecipieSOs.Count)];
                    waitingRecipeSOList.Add(waitingRecipieSO);
                    onRecipieSpawned?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private void CheckIfPlateHasRecipieFromWaitingList(PlateKitchenObject plateKitchenObject)
        {
            for (int i = 0; i < waitingRecipeSOList.Count; i++)
            {
                RecipieSO waitingRecipieSO = waitingRecipeSOList[i];

                if (waitingRecipieSO.kitchenObjectSOs.Count != plateKitchenObject.GetKitchenObjectSOs().Count)
                {
                    continue;
                }

                bool plateContentsMatchesRecipie = true;

                foreach (KitchenObjectSO recipieKitchenOjectSO in waitingRecipieSO.kitchenObjectSOs)
                {
                    bool ingerdientFound = false;

                    foreach (KitchenObjectSO plateKitcheObjectSO in plateKitchenObject.GetKitchenObjectSOs())
                    {
                        if (plateKitcheObjectSO == recipieKitchenOjectSO)
                        {
                            ingerdientFound = true;
                            break;
                        }
                    }

                    if (!ingerdientFound)
                    {
                        plateContentsMatchesRecipie = false;
                    }
                }

                if (plateContentsMatchesRecipie)
                {
                    onRecipieCompleted?.Invoke(this, EventArgs.Empty);
                    onRecipieSuccess?.Invoke(this, EventArgs.Empty);
                    waitingRecipeSOList.RemoveAt(i);
                    return;
                }
            }

            onRecipieFailed?.Invoke(this, EventArgs.Empty);

            Debug.Log("Not found matching recipie");
        }

        // Event Handlers ------------------------------------------------------



    }
}