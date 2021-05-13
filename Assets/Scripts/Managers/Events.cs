using System.Collections.Generic;
using UnityEngine;

namespace sp
{
    public static class Events
    {
        public enum Types
        {
            SceneLoading,
            SceneUploading,
            ScreensaverShown,
            GameTap,
            LoadStart,
            LoadShop,
            LoadLevel,
            ExitShop,
            Purchase,
            HighlightShopCell,
            RemoveHighlightShopCell,
            SelectShopCell,
            SelectCurrentBullet,
            IncreaseCurrency,
            IncreaseScore,
            ReduceCurrency,
            ContinueLevelByMoney,
            ContinueLevel,
            NotEnoughMoneyToContinue,
            LevelWin,
            BlockWin,
            NextBullet,
            Unknown
        }

        private static Queue<GameEvent> mEvents;

        private static bool mIsInitialized = false;

        private static bool mIsQueueLaunching = false;

        public static void InitManager()
        {
            if (!mIsInitialized)
            {
                mEvents = new Queue<GameEvent>();

                mIsInitialized = true;

                Debug.Log("Events manager loaded");
            }
        }

        public static void ResetManager()
        {
            mIsInitialized = false;
        }

        public static void LaunchEvent(Events.Types type, Scenes.Types scene = Scenes.Types.Unknown)
        {
            mEvents.Enqueue(new GameEvent(type, scene, EventHandler));

            if (!mIsQueueLaunching)
                LaunchNextEvent();
        }

        private static void LaunchNextEvent()
        {
            if (mEvents.Count > 0)
            {
                mIsQueueLaunching = true;
                var next = mEvents.Dequeue();
                next.Launch();
            }
            else
            {
                Debug.Log("Event queue is empty");

                mIsQueueLaunching = false;
            }
        }

        private static void EventHandler(Events.Types type, Scenes.Types scene = Scenes.Types.Unknown)
        {
            GeneralHandler.HandleAny(type, scene);
            LaunchNextEvent();
        }
    }

    public delegate void gameEvent(Events.Types type, Scenes.Types scene);

    public class GameEvent
    {
        private event gameEvent mEvent;

        private Events.Types mEventType = Events.Types.Unknown;

        private Scenes.Types mSceneType = Scenes.Types.Unknown;

        public GameEvent(Events.Types type, Scenes.Types scene, gameEvent handle)
        {
            mEventType = type;
            mSceneType = scene;
            mEvent += handle;
        }

        public void Launch()
        {
            Debug.Log(string.Format("Event \"{0}\" launched", mEventType));
            
            mEvent(mEventType, mSceneType);
        }
    }
}
