using System;
using UnityEngine;
using UnityEngine.UI;

namespace sp
{
    public static class StartHandler
    {
        private static GameObject mLogoMode;
        private static GameObject mMenuMode;

        public static void Handle(Events.Types type)
        {
            switch (type)
            {
                case Events.Types.SceneLoading:
                    OnSceneLoading();
                    break;

                case Events.Types.ScreensaverShown:
                    OnScreensaverShown();
                    break;

                case Events.Types.LoadShop:
                    OnLoadShop();
                    break;

                case Events.Types.LoadLevel:
                    OnLoadLevel();
                    break;

                default:
                    Debug.Log(String.Format("Can't find event type \"{0}\"", type));
                    break;
            }
        }

        public static void OnSceneLoading()
        {
            mLogoMode = GameObject.Find("Logo");
            mMenuMode = GameObject.Find("Menu");
            mMenuMode.SetActive(false);

            var lastScene = Scenes.PreviousScene;
            if (lastScene == Scenes.Types.Result || lastScene == Scenes.Types.Shop)
            {
                OnScreensaverShown();
            }
        }

        public static void OnScreensaverShown()
        {
            mLogoMode.SetActive(false);
            mMenuMode.SetActive(true);

            GameObject.Find("TextPlay").GetComponent<Text>().text = Configs.Strings.GetString("play");

            mMenuMode.GetComponent<Animation>().Play();
        }

        public static void OnLoadShop()
        {
            Scenes.GoToScene(Scenes.Types.Shop);
        }

        public static void OnLoadLevel()
        {
            if (Scenes.PreviousScene == Scenes.Types.Result)
                LevelPlayground.Resetlevels();

            Scenes.GoToScene(Scenes.Types.Level);
        }
    }
}