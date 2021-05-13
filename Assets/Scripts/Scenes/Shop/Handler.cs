using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace sp
{
    public static class ShopHandler
    {
        public static void Handle(Events.Types type)
        {
            switch (type)
            {
                case Events.Types.SceneLoading:
                    OnSceneLoading();
                    break;

                case Events.Types.ExitShop:
                    OnExitShop();
                    break;

                case Events.Types.Purchase:
                    OnPurchase();
                    break;

                case Events.Types.SelectShopCell:
                    OnSelectShopCell();
                    break;

                case Events.Types.HighlightShopCell:
                    OnHighlightShopCell();
                    break;

                case Events.Types.RemoveHighlightShopCell:
                    OnRemoveHighlightShopCell();
                    break;

                case Events.Types.SelectCurrentBullet:
                    OnSelectCurrentBullet();
                    break;

                case Events.Types.ReduceCurrency:
                    OnReduceCurrency();
                    break;

                default:
                    Debug.Log(String.Format("Can't find event type \"{0}\"", type));
                    break;
            }
        }

        public static void OnSceneLoading()
        {
            ShopData.SelectedShopCell = ShopData.Cells.Unknown;
            ShopData.BuyButton  = GameObject.Find("ButtonBuy");

            ShopData.AddAvailableBullet(LevelPlayground.Bullets.Knife);
            ShopData.CheckAvailableBullets();

            GameObject.Find("TextBuy").GetComponent<Text>().text  = Configs.Strings.GetString("buy");
            GameObject.Find("TextShop").GetComponent<Text>().text = Configs.Strings.GetString("shop");

            ShopData.ShowBuyButton(false);

            if (Profile.Settings.CurrentBullet == LevelPlayground.Bullets.Unknown)
                Profile.Settings.CurrentBullet = LevelPlayground.Bullets.Knife;

            ShopData.CurrentShopCell = ShopData.GetCellByBullet(Profile.Settings.CurrentBullet);

            Events.LaunchEvent(Events.Types.SelectShopCell, Scenes.ActiveScene);

            var args = new Dictionary<string, string>();
            args.Add("in", "true");
            Server.Report(Server.ReportComands.ShopVisit, args);
        }

        public static void OnExitShop()
        {
            var scene = Scenes.PreviousScene;

            var args = new Dictionary<string, string>();
            args.Add("in", "false");
            Server.Report(Server.ReportComands.ShopVisit, args);

            if (scene == Scenes.Types.Result)
                Scenes.GoToScene(Scenes.Types.Result);
            else if (scene == Scenes.Types.Start)
                Scenes.GoToScene(Scenes.Types.Start);
        }

        public static void OnPurchase()
        {
            ShopData.CurrentShopCell = ShopData.SelectedShopCell;
            var bullet = ShopData.GetBulletByCell(ShopData.SelectedShopCell);
            ShopData.AddAvailableBullet(bullet);
            Events.LaunchEvent(Events.Types.SelectShopCell, Scenes.ActiveScene);
            Events.LaunchEvent(Events.Types.ReduceCurrency, Scenes.ActiveScene);

            Server.Report(Server.ReportComands.Purchase);
        }

        public static void OnSelectShopCell()
        {
            ShopData.GetCellByType(ShopData.Cells.Cell_11).GetComponent<Image>().color = ShopData.NonActiveCellColor;
            ShopData.GetCellByType(ShopData.Cells.Cell_12).GetComponent<Image>().color = ShopData.NonActiveCellColor;
            ShopData.GetCellByType(ShopData.Cells.Cell_21).GetComponent<Image>().color = ShopData.NonActiveCellColor;
            ShopData.GetCellByType(ShopData.Cells.Cell_22).GetComponent<Image>().color = ShopData.NonActiveCellColor;

            ShopData.GetCurrentCell().GetComponent<Image>().color = ShopData.SelectedCellColor;
            ShopData.SelectedShopCell = ShopData.CurrentShopCell;

            var bullet = ShopData.GetBulletByCell(ShopData.CurrentShopCell);
            if (ShopData.IsAvailableBullet(bullet))
                Events.LaunchEvent(Events.Types.SelectCurrentBullet, Scenes.ActiveScene);
        }

        public static void OnHighlightShopCell()
        {
            if (ShopData.CurrentShopCell != ShopData.SelectedShopCell)
                ShopData.GetCurrentCell().GetComponent<Image>().color = ShopData.HighlightedCellColor;

            var bullet = ShopData.GetBulletByCell(ShopData.CurrentShopCell);
            ShopData.ShowBuyButton(!ShopData.IsAvailableBullet(bullet));

            var cost = ShopData.GetCost(ShopData.CurrentShopCell);
            var buttonState = GameData.IsEnoughCurrency(cost);

            ShopData.SetBuyButtonState(buttonState, cost.ToString());
        }

        public static void OnRemoveHighlightShopCell()
        {
            if (ShopData.CurrentShopCell != ShopData.SelectedShopCell)
                ShopData.GetCurrentCell().GetComponent<Image>().color = ShopData.NonActiveCellColor;

            var bullet = ShopData.GetBulletByCell(ShopData.SelectedShopCell);
            var condition = (ShopData.SelectedShopCell != ShopData.Cells.Unknown) && !ShopData.IsAvailableBullet(bullet);

            ShopData.ShowBuyButton(condition);

            var cost = ShopData.GetCost(ShopData.SelectedShopCell);
            var buttonState = GameData.IsEnoughCurrency(cost);

            ShopData.SetBuyButtonState(buttonState, cost.ToString());
        }

        public static void OnSelectCurrentBullet()
        {
            var bullet = ShopData.GetBulletByCell(ShopData.SelectedShopCell);
            Profile.Settings.CurrentBullet = bullet;

            Server.Report(Server.ReportComands.BulletSelected);
        }

        public static void OnReduceCurrency()
        {
            var cost = ShopData.GetCost(ShopData.SelectedShopCell);
            GameData.ReduceCurrency(cost);

            ShopData.CheckAvailableBullets();
        }
    }
}