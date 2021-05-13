using UnityEngine;
using sp;

public class Shop : MonoBehaviour
{
    void OnGUI()
    {
        if (CheatsPanel.NeedShowPanel)
        {
            CheatsPanel.CheatPanel();

            var button1PosEnableRect = new Rect(CheatsPanel.button11PosX, CheatsPanel.button11PosY + CheatsPanel.Gap1Y,
                CheatsPanel.ButtonWidth, CheatsPanel.ButtonHeight);
            if (GUI.Button(button1PosEnableRect, "11 Pos Enable"))
            {
                var bullet = ShopData.GetBulletByCell(ShopData.Cells.Cell_11);
                ShopData.AddAvailableBullet(bullet);
                ShopData.CheckAvailableBullets();
            }

            var button1PosDisableRect = new Rect(CheatsPanel.button21PosX, CheatsPanel.button21PosY + CheatsPanel.Gap1Y,
                CheatsPanel.ButtonWidth, CheatsPanel.ButtonHeight);
            if (GUI.Button(button1PosDisableRect, "11 Pos Disable"))
            {
                var bullet = ShopData.GetBulletByCell(ShopData.Cells.Cell_11);
                int index = ShopData.GetBulletIndex(bullet);
                Profile.Data.Purchases[index] = 0;
                ShopData.CheckAvailableBullets();
            }

            var button2PosEnableRect = new Rect(CheatsPanel.button12PosX, CheatsPanel.button12PosY + CheatsPanel.Gap1Y,
                CheatsPanel.ButtonWidth, CheatsPanel.ButtonHeight);
            if (GUI.Button(button2PosEnableRect, "12 Pos Enable"))
            {
                var bullet = ShopData.GetBulletByCell(ShopData.Cells.Cell_12);
                ShopData.AddAvailableBullet(bullet);
                ShopData.CheckAvailableBullets();
            }

            var button2PosDisableRect = new Rect(CheatsPanel.button22PosX, CheatsPanel.button22PosY + CheatsPanel.Gap1Y,
                CheatsPanel.ButtonWidth, CheatsPanel.ButtonHeight);
            if (GUI.Button(button2PosDisableRect, "12 Pos Disable"))
            {
                var bullet = ShopData.GetBulletByCell(ShopData.Cells.Cell_12);
                int index = ShopData.GetBulletIndex(bullet);
                Profile.Data.Purchases[index] = 0;
                ShopData.CheckAvailableBullets();
            }

            var button3PosEnableRect = new Rect(CheatsPanel.button31PosX, CheatsPanel.button31PosY + CheatsPanel.Gap1Y,
                CheatsPanel.ButtonWidth, CheatsPanel.ButtonHeight);
            if (GUI.Button(button3PosEnableRect, "21 Pos Enable"))
            {
                var bullet = ShopData.GetBulletByCell(ShopData.Cells.Cell_21);
                ShopData.AddAvailableBullet(bullet);
                ShopData.CheckAvailableBullets();
            }

            var button3PosDisableRect = new Rect(CheatsPanel.button41PosX, CheatsPanel.button41PosY + CheatsPanel.Gap1Y,
                CheatsPanel.ButtonWidth, CheatsPanel.ButtonHeight);
            if (GUI.Button(button3PosDisableRect, "21 Pos Disable"))
            {
                var bullet = ShopData.GetBulletByCell(ShopData.Cells.Cell_21);
                int index = ShopData.GetBulletIndex(bullet);
                Profile.Data.Purchases[index] = 0;
                ShopData.CheckAvailableBullets();
            }

            var button4PosEnableRect = new Rect(CheatsPanel.button32PosX, CheatsPanel.button32PosY + CheatsPanel.Gap1Y,
                CheatsPanel.ButtonWidth, CheatsPanel.ButtonHeight);
            if (GUI.Button(button4PosEnableRect, "22 Pos Enable"))
            {
                var bullet = ShopData.GetBulletByCell(ShopData.Cells.Cell_22);
                ShopData.AddAvailableBullet(bullet);
                ShopData.CheckAvailableBullets();
            }

            var button4PosDisableRect = new Rect(CheatsPanel.button42PosX, CheatsPanel.button42PosY + CheatsPanel.Gap1Y,
                CheatsPanel.ButtonWidth, CheatsPanel.ButtonHeight);
            if (GUI.Button(button4PosDisableRect, "22 Pos Disable"))
            {
                var bullet = ShopData.GetBulletByCell(ShopData.Cells.Cell_22);
                int index = ShopData.GetBulletIndex(bullet);
                Profile.Data.Purchases[index] = 0;
                ShopData.CheckAvailableBullets();
            }
        }
    }
}
