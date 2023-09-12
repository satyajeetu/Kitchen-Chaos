using System;
using UnityEngine;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------

    public enum StoveState
    {
        IDLE,
        FRYING,
        FIRED,
        BURNED
    }

    public class OnStoveStateChangeEventArgs : EventArgs
    {
        public StoveState currentState;
    }

    public class StoveCounter : BaseCounter, IHasProgress
    {
        // Public Properties ---------------------------------------------------

        public event EventHandler<OnStoveStateChangeEventArgs> OnStoveStateChanged;
        public event EventHandler<OnProgressChagnedEventArgs> onProgressChanged;


        // Private Fields ------------------------------------------------------

        [SerializeField] FryingRecipieSO[] fryingRecipieSOs;
        [SerializeField] BurningRecipieSO[] burningRecipieSOs;


        private float fryingTimer;
        private float burningTimer;
        private FryingRecipieSO fryingRecipieSO;
        private BurningRecipieSO burningRecipieSO;
        private StoveState stoveState;

        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------

        private void Start()
        {
            stoveState = StoveState.IDLE;
        }

        private void Update()
        {
            if (!HasKitchenObject())
            {
                return;
            }

            switch (stoveState)
            {
                case StoveState.IDLE:
                    break;

                case StoveState.FRYING:
                    StoveState_Frying();
                    break;

                case StoveState.FIRED:
                    StoveState_Fried();
                    break;

                case StoveState.BURNED:
                    break;
            }
        }

        // Public Methods ------------------------------------------------------

        public override void Interact(Player player)
        {
            if (!HasKitchenObject())
            {
                if (player.HasKitchenObject()
                    && HasRecipieWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    fryingRecipieSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    stoveState = StoveState.FRYING;
                    OnStoveStateChanged?.Invoke(this, new OnStoveStateChangeEventArgs { currentState = stoveState });
                    fryingTimer = 0;

                    onProgressChanged?.Invoke(this, new OnProgressChagnedEventArgs
                        { progressNormalized = fryingTimer / fryingRecipieSO.fringTimerMax });
                }
                else if (player.HasKitchenObject())
                {
                    if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                    {
                        if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                        {
                            GetKitchenObject().DestroySelf();
                        }

                        stoveState = StoveState.IDLE;
                        OnStoveStateChanged?.Invoke(this, new OnStoveStateChangeEventArgs { currentState = stoveState });

                        onProgressChanged?.Invoke(this, new OnProgressChagnedEventArgs
                        { progressNormalized = 0f });
                    }
                }
            }
            else
            {
                if (!player.HasKitchenObject())
                {
                    GetKitchenObject().SetKitchenObjectParent(player);
                    stoveState = StoveState.IDLE;
                    OnStoveStateChanged?.Invoke(this, new OnStoveStateChangeEventArgs { currentState = stoveState });

                    onProgressChanged?.Invoke(this, new OnProgressChagnedEventArgs
                    { progressNormalized = 0f });
                }
            }
        }

        public bool IsFried()
        {
            return stoveState == StoveState.FIRED;
        }

        // Private Methods -----------------------------------------------------

        private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputSo)
        {
            var fryingRecipieSO = GetFryingRecipeSOWithInput(inputSo);

            if (fryingRecipieSO == null)
            {
                return null;
            }

            return fryingRecipieSO.output;
        }

        private bool HasRecipieWithInput(KitchenObjectSO inputKitchenObjectSO)
        {
            return GetFryingRecipeSOWithInput(inputKitchenObjectSO) != null;
        }

        private FryingRecipieSO GetFryingRecipeSOWithInput(KitchenObjectSO kitchenObjectSO)
        {
            foreach (FryingRecipieSO fryingRecipeSO in fryingRecipieSOs)
            {
                if (fryingRecipeSO.input == kitchenObjectSO)
                {
                    return fryingRecipeSO;
                }
            }

            return null;
        }

        private BurningRecipieSO GetBurningRecipeSOWithInput(KitchenObjectSO kitchenObjectSO)
        {
            foreach (BurningRecipieSO burningRecipieSO in burningRecipieSOs)
            {
                if (burningRecipieSO.input == kitchenObjectSO)
                {
                    return burningRecipieSO;
                }
            }

            return null;
        }

        private void StoveState_Frying()
        {
            fryingTimer += Time.deltaTime;

            onProgressChanged?.Invoke(this, new OnProgressChagnedEventArgs
            { progressNormalized = fryingTimer / fryingRecipieSO.fringTimerMax });

            if (fryingTimer > fryingRecipieSO.fringTimerMax)
            {
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(fryingRecipieSO.output, this);

                burningRecipieSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                stoveState = StoveState.FIRED;
                OnStoveStateChanged?.Invoke(this, new OnStoveStateChangeEventArgs { currentState = stoveState });
                burningTimer = 0;

            }
        }

        private void StoveState_Fried()
        {
            burningTimer += Time.deltaTime;

            onProgressChanged?.Invoke(this, new OnProgressChagnedEventArgs
            { progressNormalized = burningTimer / burningRecipieSO.burningTimerMax });

            if (burningTimer > burningRecipieSO.burningTimerMax)
            {
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(burningRecipieSO.output, this);
                stoveState = StoveState.BURNED;
                OnStoveStateChanged?.Invoke(this, new OnStoveStateChangeEventArgs { currentState = stoveState });

                onProgressChanged?.Invoke(this, new OnProgressChagnedEventArgs
                { progressNormalized = 0f });
            }
        }

        // Event Handlers ------------------------------------------------------
    }
}
