using System;
using UnityEngine;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------

    public class OnProgressChagnedEventArgs : EventArgs
    {
        public float progressNormalized;
    }

    public class CuttingCounter : BaseCounter
    {
        // Public Properties ---------------------------------------------------

        public event EventHandler<OnProgressChagnedEventArgs> onProgressChanged;
        public event EventHandler onCut;

        // Private Fields ------------------------------------------------------

        [SerializeField] private CuttingRecipeSO[] cuttingRecipes;

        private int cuttingProgress;

        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------



        // Public Methods ------------------------------------------------------

        public override void Interact(Player player)
        {
            if (!HasKitchenObject())
            {
                if (player.HasKitchenObject()
                    && HasRecipieWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;

                    CuttingRecipeSO cuttingRecipe = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    onProgressChanged?.Invoke(this, new OnProgressChagnedEventArgs
                    {
                        progressNormalized = (float)cuttingProgress / cuttingRecipe.cuttingProgressMax
                    });
                }
            }
            else
            {
                if (!player.HasKitchenObject())
                {
                    GetKitchenObject().SetKitchenObjectParent(player);
                }
            }
        }

        public override void InteractAlternate(Player player)
        {
            if (HasKitchenObject()
                && HasRecipieWithInput(GetKitchenObject().GetKitchenObjectSO()))
            {
                cuttingProgress++;

                onCut?.Invoke(this, EventArgs.Empty);

                CuttingRecipeSO cuttingRecipe = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                onProgressChanged?.Invoke(this, new OnProgressChagnedEventArgs
                {
                    progressNormalized = (float)cuttingProgress / cuttingRecipe.cuttingProgressMax
                });

                if (cuttingProgress >= cuttingRecipe.cuttingProgressMax)
                {
                    KitchenObjectSO outputSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

                    if (outputSO == null)
                    {
                        return;
                    }

                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(outputSO, this);
                }
            }
        }

        // Private Methods -----------------------------------------------------

        private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputSo)
        {
            var cuttingRecipe = GetCuttingRecipeSOWithInput(inputSo);

            if (cuttingRecipe == null)
            {
                return null;
            }

            return cuttingRecipe.output;
        }

        private bool HasRecipieWithInput(KitchenObjectSO inputKitchenObjectSO)
        {
             return GetCuttingRecipeSOWithInput(inputKitchenObjectSO) != null;
        }

        private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO kitchenObjectSO)
        {
            foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipes)
            {
                if (cuttingRecipeSO.input == kitchenObjectSO)
                {
                    return cuttingRecipeSO;
                }
            }

            return null;
        }

        // Event Handlers ------------------------------------------------------

    }
}
