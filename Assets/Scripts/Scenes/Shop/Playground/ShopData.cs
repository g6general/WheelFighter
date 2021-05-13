using UnityEngine;
using UnityEngine.UI;
using sp;

public class ShopData
{
    public enum Cells
    {
        Cell_11,
        Cell_12,
        Cell_21,
        Cell_22,
        Unknown
    }

    public static Cells CurrentShopCell  { get; set; }
    public static Cells SelectedShopCell { get; set; }
    public static GameObject BuyButton   { get; set; }

    private static Color mHighlightedCellColor = new Color(214, 153, 227, 0.3f);
    private static Color mSelectedCellColor    = new Color(214, 153, 227, 0.5f);
    private static Color mNonActiveCellColor   = new Color(250, 225, 255, 0.2f);
    private static Color mNonActiveBulletColor = new Color(0, 0, 0, 0.5f);

    public static Color HighlightedCellColor { get { return mHighlightedCellColor; } }
    public static Color SelectedCellColor    { get { return mSelectedCellColor; } }
    public static Color NonActiveCellColor   { get { return mNonActiveCellColor; } }
    public static Color NonActiveBulletColor { get { return mNonActiveBulletColor; } }

    public static int NumberOfPurchases = 4;

    public static GameObject GetCellByType(Cells type)
    {
        GameObject cell = null;

        switch (type)
        {
            case Cells.Cell_11:
                cell = GameObject.Find("Cell_11");
                break;

            case Cells.Cell_12:
                cell = GameObject.Find("Cell_12");
                break;

            case Cells.Cell_21:
                cell = GameObject.Find("Cell_21");
                break;

            case Cells.Cell_22:
                cell = GameObject.Find("Cell_22");
                break;

            default:
                Debug.LogError("Current cell is not assigned");
                break;
        }

        return cell;
    }

    public static GameObject GetCurrentCell()
    {
        return GetCellByType(CurrentShopCell);
    }

    public static LevelPlayground.Bullets GetBulletByCell(Cells type)
    {
        var bullet = LevelPlayground.Bullets.Unknown;

        switch (type)
        {
            case Cells.Cell_11:
                bullet = LevelPlayground.Bullets.Knife;
                break;

            case Cells.Cell_12:
                bullet = LevelPlayground.Bullets.Pencil;
                break;

            case Cells.Cell_21:
                bullet = LevelPlayground.Bullets.Sword;
                break;

            case Cells.Cell_22:
                bullet = LevelPlayground.Bullets.Fork;
                break;
        }

        return bullet;
    }

    public static Cells GetCellByBullet(LevelPlayground.Bullets bullet)
    {
        Cells cell = Cells.Unknown;

        switch (bullet)
        {
            case LevelPlayground.Bullets.Knife:
                cell = Cells.Cell_11;
                break;

            case LevelPlayground.Bullets.Pencil:
                cell = Cells.Cell_12;
                break;

            case LevelPlayground.Bullets.Sword:
                cell = Cells.Cell_21;
                break;

            case LevelPlayground.Bullets.Fork:
                cell = Cells.Cell_22;
                break;
        }

        return cell;
    }

    public static void CheckAvailableBullets()
    {
        var bullet11 = GameObject.Find("bullet_11");
        var bullet12 = GameObject.Find("bullet_12");
        var bullet21 = GameObject.Find("bullet_21");
        var bullet22 = GameObject.Find("bullet_22");

        if (!IsAvailableBullet(LevelPlayground.Bullets.Knife))
            bullet11.GetComponent<SpriteRenderer>().color = NonActiveBulletColor;
        else
            bullet11.GetComponent<SpriteRenderer>().color = Color.white;

        if (!IsAvailableBullet(LevelPlayground.Bullets.Pencil))
            bullet12.GetComponent<SpriteRenderer>().color = NonActiveBulletColor;
        else
            bullet12.GetComponent<SpriteRenderer>().color = Color.white;

        if (!IsAvailableBullet(LevelPlayground.Bullets.Sword))
            bullet21.GetComponent<SpriteRenderer>().color = NonActiveBulletColor;
        else
            bullet21.GetComponent<SpriteRenderer>().color = Color.white;

        if (!IsAvailableBullet(LevelPlayground.Bullets.Fork))
            bullet22.GetComponent<SpriteRenderer>().color = NonActiveBulletColor;
        else
            bullet22.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public static void AddAvailableBullet(LevelPlayground.Bullets bullet)
    {
        if (!IsAvailableBullet(bullet))
        {
            int index = GetBulletIndex(bullet);
            if (index == -1)
                return;

            var purchases = Profile.Data.Purchases;

            if (purchases.Count != NumberOfPurchases)
            {
                purchases.Clear();
                purchases.Add(1);
                purchases.Add(0);
                purchases.Add(0);
                purchases.Add(0);
            }

            purchases[index] = 1;
        }
    }

    public static bool IsAvailableBullet(LevelPlayground.Bullets bullet)
    {
        int index = GetBulletIndex(bullet);
        if (index == -1)
            return false;

        var purchases = Profile.Data.Purchases;

        if (purchases.Count > index && purchases[index] > 0)
            return true;

        return false;
    }

    public static int GetBulletIndex(LevelPlayground.Bullets bullet)
    {
        int index = -1;

        switch (bullet)
        {
            case LevelPlayground.Bullets.Knife:
                index = 0;
                break;
            case LevelPlayground.Bullets.Pencil:
                index = 1;
                break;
            case LevelPlayground.Bullets.Sword:
                index = 2;
                break;
            case LevelPlayground.Bullets.Fork:
                index = 3;
                break;
        }

        return index;
    }

    public static void SetBuyButtonState(bool active, string cost = "")
    {
        if (BuyButton.activeSelf)
        {
            BuyButton.GetComponent<Button>().interactable = active;

            if (cost.Length > 0)
                GameObject.Find("TextPrize").GetComponent<Text>().text = cost;
        }
    }
        
    public static void ShowBuyButton(bool show)
    {
        BuyButton.GetComponent<Button>().interactable = false;
        BuyButton.SetActive(show);
    }

    public static int GetCost(Cells type)
    {
        int cost = 0;

        switch (type)
        {
            case Cells.Cell_11:
                cost = Configs.Balance.Cost11;
                break;

            case Cells.Cell_12:
                cost = Configs.Balance.Cost12;
                break;

            case Cells.Cell_21:
                cost = Configs.Balance.Cost21;
                break;

            case Cells.Cell_22:
                cost = Configs.Balance.Cost22;
                break;
        }

        return cost;
    }
}
