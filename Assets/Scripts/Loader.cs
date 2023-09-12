using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------

    public enum Scene
    {
        GameScene,
        MainMenuScene,
        SceneLoadingScene
    }

    public static class Loader
    {
        // Public Properties ---------------------------------------------------


        // Private Fields ------------------------------------------------------

        private static Scene targetScene;


        // Intitalization ------------------------------------------------------



        // Public Methods ------------------------------------------------------

        public static void Load(Scene targetScene)
        {
            Loader.targetScene = targetScene;

            SceneManager.LoadScene(Scene.SceneLoadingScene.ToString());
        }

        public static void LoaderCallback()
        {
            SceneManager.LoadScene(targetScene.ToString());
        }


        // Private Methods -----------------------------------------------------



        // Event Handlers ------------------------------------------------------



    }
}
