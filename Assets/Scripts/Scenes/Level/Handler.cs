using System;
using UnityEngine;

namespace sp
{
    public static class LevelHandler
    {
        public static void Handle(Events.Types type)
        {
            switch (type)
            {
                case Events.Types.SceneLoading:
                    OnSceneLoading();
                    break;

                case Events.Types.LevelWin:
                    OnLevelWin();
                    break;

                case Events.Types.BlockWin:
                    OnBlockWin();
                    break;

                case Events.Types.NextBullet:
                    OnNextBullet();
                    break;

                case Events.Types.IncreaseScore:
                    OnIncreaseScore();
                    break;

                case Events.Types.GameTap:
                    OnGameTap();
                    break;

                default:
                    Debug.Log(String.Format("Can't find event type \"{0}\"", type));
                    break;
            }
        }

        public static void OnSceneLoading()
        {
            LevelPlayground.LoadLevels();
            LevelPlayground.LoadPrefabs();
            LevelPlayground.CreateWheel();
            LevelPlayground.CreateBullets();
            LevelPlayground.CreatePrizes();
            LevelPlayground.CreateBarriers();
            LevelPlayground.RenderPoints();
        }

        public static void OnLevelWin()
        {
            LevelPlayground.OnLevelWin();

            Server.Report(Server.ReportComands.LevelFinished);
        }

        public static void OnBlockWin()
        {
            LevelPlayground.OnBlockWin();

            Server.Report(Server.ReportComands.BlockFinished);
        }

        public static void OnNextBullet()
        {
            var state = LevelPlayground.GetLevelState();
            if (state == LevelPlayground.LevelState.LoseLevel)
                return;

            LevelPlayground.SetNextBulletAsActive();
        }

        public static void OnIncreaseScore()
        {
            GameData.IncreaseScore();
        }

        public static void OnGameTap()
        {
            if (CheatsPanel.ButtonClicked)
            {
                CheatsPanel.ButtonClicked = false;
                return;
            }

            if (LevelPlayground.ActiveBullet)
                LevelPlayground.ActiveBullet.GetComponent<Bullet>().LaunchBullet();
        }
    }
}