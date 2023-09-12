using UnityEngine;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------



    public class StoveCounterSound : MonoBehaviour
    {
        // Public Properties ---------------------------------------------------



        // Private Fields ------------------------------------------------------

        [SerializeField] private StoveCounter stoveCounter;

        private AudioSource audioSource;
        private float warningSoundTimer;
        private bool playWarningSound;

        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            stoveCounter.OnStoveStateChanged += StoveCounter_OnStoveStateChanged;
            stoveCounter.onProgressChanged += StoveCounter_onProgressChanged;
        }


        private void OnDisable()
        {
            stoveCounter.OnStoveStateChanged -= StoveCounter_OnStoveStateChanged;
            stoveCounter.onProgressChanged -= StoveCounter_onProgressChanged;
        }

        private void Update()
        {
            if (playWarningSound)
            {
                warningSoundTimer -= Time.deltaTime;

                if (warningSoundTimer < 0)
                {
                    float warningSoundTimerMax = 0.2f;
                    warningSoundTimer = warningSoundTimerMax;

                    SoundManger.Singleton.PlayWarningSound(stoveCounter.transform.position);
                }
            }
        }

        // Public Methods ------------------------------------------------------



        // Private Methods -----------------------------------------------------



        // Event Handlers ------------------------------------------------------

        private void StoveCounter_OnStoveStateChanged(object sender, OnStoveStateChangeEventArgs e)
        {
            bool playSound = (e.currentState == StoveState.FRYING || e.currentState == StoveState.FIRED);

            if (playSound)
            {
                audioSource.Play();
            }
            else
            {
                audioSource.Pause();
            }
        }

        private void StoveCounter_onProgressChanged(object sender, OnProgressChagnedEventArgs e)
        {
            float burningShowProgressAmount = 0.5f;
            playWarningSound = stoveCounter.IsFried() && e.progressNormalized >= burningShowProgressAmount;
        }

    }
}
