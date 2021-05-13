using System;
using UnityEngine;
using UnityEngine.UI;

namespace sp
{
    public static class ResultHandler
    {
        public static void Handle(Events.Types type)
        {
            switch (type)
            {
                case Events.Types.SceneLoading:
                    OnSceneLoading();
                    break;

                case Events.Types.ContinueLevelByMoney:
                    OnContinueLevelByMoney();
                    break;

                case Events.Types.ContinueLevel:
                    OnContinueLevel();
                    break;

                case Events.Types.LoadStart:
                    OnLoadStart();
                    break;

                case Events.Types.LoadShop:
                    OnLoadShop();
                    break;

                case Events.Types.NotEnoughMoneyToContinue:
                    OnNotEnoughMoneyToContinue();
                    break;

                default:
                    Debug.Log(String.Format("Can't find event type \"{0}\"", type));
                    break;
            }
        }

        public static void OnSceneLoading()
        {
            var textResult  = GameObject.Find("TextResult").GetComponent<Text>();
            var textBody    = GameObject.Find("TextBody").GetComponent<Text>();
            var textPlay    = GameObject.Find("TextPlay").GetComponent<Text>();
            var buttonDeny  = GameObject.Find("ButtonDeny");
            var prize       = GameObject.Find("CounterSmallPrize");
            var counterText = GameObject.Find("CounterSmallText");

            buttonDeny.GetComponentInChildren<Text>().text = Configs.Strings.GetString("no_thanks");

            var levelState = LevelPlayground.GetLevelState();

            if (levelState == LevelPlayground.LevelState.WinLevel)
            {
                Color newColor = new Color();
                ColorUtility.TryParseHtmlString("#6DC248", out newColor);

                textResult.color = newColor;
                textResult.text = Configs.Strings.GetString("victory");

                textBody.color = newColor;
                textBody.text = Configs.Strings.GetString("reward");

                textPlay.text = Configs.Strings.GetString("go_on");

                counterText.GetComponent<Text>().text = Configs.Balance.LevelReward.ToString();

                GameData.AddedCurrency = Configs.Balance.LevelReward;
                Events.LaunchEvent(Events.Types.IncreaseCurrency);

                buttonDeny.SetActive(false);
                prize.SetActive(false);
            }
            else if (levelState == LevelPlayground.LevelState.LoseLevel)
            {
                Color newColor = new Color();
                ColorUtility.TryParseHtmlString("#CB6176", out newColor);

                textResult.color = newColor;
                textResult.text = Configs.Strings.GetString("loss");

                textBody.color = newColor;
                textBody.text = Configs.Strings.GetString("total");

                textPlay.text = Configs.Strings.GetString("continue");

                var paymentText = GameObject.Find("CounterPayment").GetComponent<Text>();
                paymentText.text = Configs.Balance.PaymentForContinue.ToString();

                counterText.GetComponent<Text>().text = GameData.GetCurrency().ToString();
            }
        }

        public static void OnContinueLevelByMoney()
        {
            GameData.ReduceCurrency(Configs.Balance.PaymentForContinue);
            LevelPlayground.SetLevelState(LevelPlayground.LevelState.Unknown);

            Server.Report(Server.ReportComands.NextLevelByMoney);

            Scenes.GoToScene(Scenes.Types.Level);
        }

        public static void OnContinueLevel()
        {
            LevelPlayground.Resetlevels();
            LevelPlayground.SetLevelState(LevelPlayground.LevelState.Unknown);

            Server.Report(Server.ReportComands.NextLevel);

            Scenes.GoToScene(Scenes.Types.Level);
        }

        public static void OnNotEnoughMoneyToContinue()
        {
            Server.Report(Server.ReportComands.NotEnoughMoney);
        }

        public static void OnLoadStart()
        {
            LevelPlayground.SetLevelState(LevelPlayground.LevelState.Unknown);
            Scenes.GoToScene(Scenes.Types.Start);

            Server.Report(Server.ReportComands.RestartLevel);
        }

        public static void OnLoadShop()
        {
            Scenes.GoToScene(Scenes.Types.Shop);
        }
    }
}