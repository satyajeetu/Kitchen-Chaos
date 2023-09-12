using UnityEngine;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------


    [DefaultExecutionOrder(-1)]
    public class MusicManager : MonoBehaviour
    {

        // Public Properties ---------------------------------------------------

        public static MusicManager Singleton;


        // Private Fields ------------------------------------------------------

        private AudioSource musicSource;
        private float globalVolume = 0.3f;

        private const string PLAYER_PREFS_MUSIC_MANAGER_VOLUME = "MusicManagerVolume";

        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------

        private void Awake()
        {
            Singleton = this;
            musicSource = GetComponent<AudioSource>();
            globalVolume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_MANAGER_VOLUME, 0.3f);
            musicSource.volume = globalVolume;
        }

        // Public Methods ------------------------------------------------------

        public void ChangeGlobalVolume()
        {
            globalVolume += .1f;

            if (globalVolume > 1f)
                globalVolume = 0;

            PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_MANAGER_VOLUME, globalVolume);
            musicSource.volume = globalVolume;
        }

        public float GetGlobalVolume()
        {
            return globalVolume;
        }


        // Private Methods -----------------------------------------------------



        // Event Handlers ------------------------------------------------------



    }
}

