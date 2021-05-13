using System;
using UnityEngine;

namespace sp
{
    public static class GeneralHandler
    {
        public static void HandleAny(Events.Types type, Scenes.Types scene = Scenes.Types.Unknown)
        {
            switch (scene)
            {
                case Scenes.Types.Start:
                    StartHandler.Handle(type);
                    break;

                case Scenes.Types.Level:
                    LevelHandler.Handle(type);
                    break;

                case Scenes.Types.Result:
                    ResultHandler.Handle(type);
                    break;

                case Scenes.Types.Shop:
                    ShopHandler.Handle(type);
                    break;

                case Scenes.Types.Unknown:
                    Handle(type);
                    break;

                default:
                    Debug.Log(String.Format("Can't find scene type \"{0}\"", scene));
                    break;
            }
        }

        public static void Handle(Events.Types type)
        {
            switch (type)
            {
                case Events.Types.SceneLoading:
                    OnSceneLoading();
                    break;

                case Events.Types.SceneUploading:
                    OnSceneUploading();
                    break;

                case Events.Types.IncreaseCurrency:
                    OnIncreaseCurrency();
                    break;

                default:
                    Debug.Log(String.Format("Can't find event type \"{0}\"", type));
                    break;
            }
        }

        public static void OnSceneLoading()
        {
            CheatsPanel.NeedShowPanel = false;
        }

        public static void OnSceneUploading()
        {
            Profile.SaveProfile();
            Profile.SaveSettings();
        }

        public static void OnIncreaseCurrency()
        {
            GameData.AddCurrency(GameData.AddedCurrency);

            var counter = GameObject.Find("CounterText");
            counter.GetComponent<Animation>().Play();
        }
    }
}