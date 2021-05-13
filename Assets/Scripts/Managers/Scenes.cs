using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace sp
{
    public static class Scenes
    {
        public enum Types
        {
            Start,
            Level,
            Result,
            Shop,
            Unknown
        }

        private static Types mActiveScene = Types.Unknown;
        private static Types mPreviousScene = Types.Unknown;

        private static bool mIsInitialized = false;

        public static Types ActiveScene { get { return mActiveScene; } }
        public static Types PreviousScene { get { return mPreviousScene; } }

        public static void InitManager()
        {
            if (!mIsInitialized)
            {
                mPreviousScene = Types.Unknown;
                mActiveScene = GetSceneType(SceneManager.GetActiveScene());

                mIsInitialized = true;

                Debug.Log("Scenes manager loaded");
            }
        }

        public static void ResetManager()
        {
            mIsInitialized = false;
        }

        public static Types GetSceneType(Scene scene)
        {
            Types sceneType = Types.Unknown;

            switch (scene.name)
            {
                case "Start":
                    sceneType = Types.Start;
                    break;

                case "Level":
                    sceneType = Types.Level;
                    break;

                case "Result":
                    sceneType = Types.Result;
                    break;

                case "Shop":
                    sceneType = Types.Shop;
                    break;

                default:
                    break;
            }

            return sceneType;
        }

        public static Scene GetScene(Types scene)
        {
            return SceneManager.GetSceneByName(scene.ToString());
        }

        public static void GoToScene(Types targetScene)
        {
            mPreviousScene = mActiveScene;
            mActiveScene = targetScene;

            Debug.Log(String.Format("Scene \"{0}\" loading", targetScene));

            SceneManager.LoadScene(targetScene.ToString());
        }
    }
}