using UnityEngine;

namespace sp
{
    public static class CheatsPanel
    {
        public static bool DesingMode = false;
        public static bool NeedShowPanel = false;
        public static bool ButtonClicked = false;

        public static float PanelWidth  = 360;
        public static float PanelHeight = 335;

        public static float PanelX = 50;
        public static float PanelY = 100;

        public static float ButtonWidth  = 100;
        public static float ButtonHeight = 30;

        public static float Delta1X = 20;
        public static float Delta1Y = 30;

        public static float Delta2X = 10;
        public static float Delta2Y = 10;

        public static float Gap1Y = 130;
        public static float Gap2Y = 220;

        public static float CheatsButtonX = 50;
        public static float CheatsButtonY = 60;

        public static string PanelName = "Cheat Menu";
        public static string CheatsButtonName = "Cheats";

        public static float button11PosX = PanelX + Delta1X;
        public static float button11PosY = PanelY + Delta1Y;

        public static float button21PosX = button11PosX;
        public static float button21PosY = button11PosY + Delta2Y + ButtonHeight;

        public static float button31PosX = button11PosX;
        public static float button31PosY = button21PosY + Delta2Y + ButtonHeight;

        public static float button41PosX = button11PosX;
        public static float button41PosY = button31PosY + Delta2Y + ButtonHeight;

        public static float button12PosX = button11PosX + Delta2X + ButtonWidth;
        public static float button12PosY = PanelY + Delta1Y;

        public static float button22PosX = button12PosX;
        public static float button22PosY = button11PosY + Delta2Y + ButtonHeight;

        public static float button32PosX = button12PosX;
        public static float button32PosY = button21PosY + Delta2Y + ButtonHeight;

        public static float button42PosX = button12PosX;
        public static float button42PosY = button31PosY + Delta2Y + ButtonHeight;

        public static float button13PosX = button12PosX + Delta2X + ButtonWidth;
        public static float button13PosY = PanelY + Delta1Y;

        public static float button23PosX = button13PosX;
        public static float button23PosY = button11PosY + Delta2Y + ButtonHeight;

        public static float button33PosX = button13PosX;
        public static float button33PosY = button21PosY + Delta2Y + ButtonHeight;

        public static float button43PosX = button13PosX;
        public static float button43PosY = button31PosY + Delta2Y + ButtonHeight;

        public static void CheatButton()
        {
            if (Debug.isDebugBuild)
            {
                var buttonRect = new Rect(CheatsButtonX, CheatsButtonY, ButtonWidth, ButtonHeight);
                if (GUI.Button(buttonRect, CheatsButtonName))
                {
                    NeedShowPanel = !NeedShowPanel;
                    ButtonClicked = true;
                }
            }
        }

        public static void CheatPanel()
        {
            if (NeedShowPanel)
            {
                var panelRect = new Rect(PanelX, PanelY, PanelWidth, PanelHeight);
                GUI.Box(panelRect, PanelName);

                var buttonScorePlusRect = new Rect(button11PosX, button11PosY, ButtonWidth, ButtonHeight);
                if (GUI.Button(buttonScorePlusRect, "+1 Score"))
                {
                    GameData.IncreaseScore();
                    ButtonClicked = true;
                }

                var buttonScoreMinusRect = new Rect(button21PosX, button21PosY, ButtonWidth, ButtonHeight);
                if (GUI.Button(buttonScoreMinusRect, "-1 Score"))
                {
                    --Profile.Data.Score;
                    ButtonClicked = true;
                }

                var buttonResetScoreRect = new Rect(button31PosX, button31PosY, ButtonWidth, ButtonHeight);
                if (GUI.Button(buttonResetScoreRect, "Reset Score"))
                {
                    Profile.Data.Score = 0;
                    ButtonClicked = true;
                }

                var buttonCurrencyPlusRect = new Rect(button12PosX, button12PosY, ButtonWidth, ButtonHeight);
                if (GUI.Button(buttonCurrencyPlusRect, "+1 Currency"))
                {
                    GameData.AddCurrency();
                    ButtonClicked = true;
                }

                var buttonCurrencyMinusRect = new Rect(button22PosX, button22PosY, ButtonWidth, ButtonHeight);
                if (GUI.Button(buttonCurrencyMinusRect, "-1 Currency"))
                {
                    GameData.ReduceCurrency();
                    ButtonClicked = true;
                }

                var buttonResetCurrencyRect = new Rect(button32PosX, button32PosY, ButtonWidth, ButtonHeight);
                if (GUI.Button(buttonResetCurrencyRect, "Reset Currency"))
                {
                    Profile.Data.Currency = 0;
                    ButtonClicked = true;
                }

                var buttonResetProgressRect = new Rect(button13PosX, button13PosY, ButtonWidth, ButtonHeight);
                if (GUI.Button(buttonResetProgressRect, "Reset Proress"))
                {
                    Profile.Settings.NeedResetSettings = true;
                    ButtonClicked = true;
                    GameData.RestartGame();
                }
            }
        }
    }
}