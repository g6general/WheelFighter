using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using sp;

public class ShopCell : MonoBehaviour, 
    IPointerClickHandler,
    IPointerEnterHandler,
    IPointerExitHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        ShopData.CurrentShopCell = GetPositionByName();
        Events.LaunchEvent(Events.Types.SelectShopCell, Scenes.ActiveScene);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShopData.CurrentShopCell = GetPositionByName();
        Events.LaunchEvent(Events.Types.HighlightShopCell, Scenes.ActiveScene);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ShopData.CurrentShopCell = GetPositionByName();
        Events.LaunchEvent(Events.Types.RemoveHighlightShopCell, Scenes.ActiveScene);
    }

    private ShopData.Cells GetPositionByName()
    {
        var current = ShopData.Cells.Unknown;

        if (gameObject.name == "Cell_11")
            current = ShopData.Cells.Cell_11;
        else if (gameObject.name == "Cell_12")
            current = ShopData.Cells.Cell_12;
        else if (gameObject.name == "Cell_21")
            current = ShopData.Cells.Cell_21;
        else if (gameObject.name == "Cell_22")
            current = ShopData.Cells.Cell_22;

        return current;
    }
}
