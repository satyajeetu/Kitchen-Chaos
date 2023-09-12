 using System;
using UnityEngine;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------



    public class SoundManger : MonoBehaviour
    {
        // Public Properties ---------------------------------------------------

        public static SoundManger Singleton;

        // Private Fields ------------------------------------------------------

        [SerializeField] private AudioClipsSO audioClipsSO;

        private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";

        private float globalVolume = 1f;

        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------

        private void Awake()
        {
            Singleton = this;

            globalVolume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1.0f);
        }

        private void OnEnable()
        {
            DeliveryManager.Singleton.onRecipieSuccess += DeliverManager_OnRecipieSucess;
            DeliveryManager.Singleton.onRecipieFailed += DeliveryManager_OnRecipieFailed;
            CuttingCounter.onAnyCut += CuttingCounter_OnAnyCut;
            Player.Singleton.onPickSomething += Player_OnPickSomething;
            BaseCounter.onAnyObjectPlacedHere += BaseCounter_onAnyObjectPlacedHere;
            TrashCounter.onAnyObjectTrashed += TrashCounter_onAnyObjectTrashed;
        }

        private void OnDisable()
        {
            DeliveryManager.Singleton.onRecipieSuccess -= DeliverManager_OnRecipieSucess;
            DeliveryManager.Singleton.onRecipieFailed -= DeliveryManager_OnRecipieFailed;
            CuttingCounter.onAnyCut -= CuttingCounter_OnAnyCut;
            Player.Singleton.onPickSomething -= Player_OnPickSomething;
            BaseCounter.onAnyObjectPlacedHere -= BaseCounter_onAnyObjectPlacedHere;
            TrashCounter.onAnyObjectTrashed -= TrashCounter_onAnyObjectTrashed;
        }

        // Public Methods ------------------------------------------------------

        public void PlayCountDownSound()
        {
            PlaySound(audioClipsSO.warning, Vector3.zero);
        }

        public void PlayFootstepsSound(Transform transform, float volume = 1)
        {
            PlaySound(audioClipsSO.footSteps, transform.position, volume);
        }

        public void ChangeGlobalVolume()
        {
            globalVolume += .1f;

            if (globalVolume > 1f)
                globalVolume = 0;

            PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, globalVolume);
        }

        public float GetGlobalVolume()
        {
            return globalVolume;
        }

        public void PlayWarningSound(Vector3 position)
        {
            PlaySound(audioClipsSO.warning, position);
        }

        // Private Methods -----------------------------------------------------

        private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1)
        {
            AudioSource.PlayClipAtPoint(audioClip, position, volume * globalVolume);
        }

        private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1)
        {
            PlaySound(audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)], position, volume);
        }

        // Event Handlers ------------------------------------------------------

        private void DeliverManager_OnRecipieSucess(object sender, EventArgs e)
        {
            PlaySound(audioClipsSO.deliverySuccess, Camera.main.transform.position, 0.2f);
        }

        private void DeliveryManager_OnRecipieFailed(object sender, EventArgs e)
        {
            PlaySound(audioClipsSO.deliverFail, Camera.main.transform.position, 0.2f);
        }

        private void CuttingCounter_OnAnyCut(object sender, EventArgs e)
        {
            PlaySound(audioClipsSO.chop, Camera.main.transform.position, 0.2f);
        }

        private void Player_OnPickSomething(object sender, EventArgs e)
        {
            Player player = sender as Player;
            PlaySound(audioClipsSO.objectPickup, player.GetComponent<Transform>().position);
        }

        private void BaseCounter_onAnyObjectPlacedHere(object sender, EventArgs e)
        {
            BaseCounter baseCounter = sender as BaseCounter;
            PlaySound(audioClipsSO.objectDrop, baseCounter.GetComponent<Transform>().position);
        }

        private void TrashCounter_onAnyObjectTrashed(object sender, EventArgs e)
        {
            TrashCounter trashCounter = sender as TrashCounter;
            PlaySound(audioClipsSO.trash, trashCounter.GetComponent<Transform>().position);
        }
    }
}

