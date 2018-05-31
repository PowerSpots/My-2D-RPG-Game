using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    public InventoryItem Item;
    public ShopManager Manager;
    Image image;
    Text name;

    public CanvasGroup buyButton;

    // 将库存项目添加到当前插槽并显示
    public void AddShopItem(InventoryItem item)
    {
        image = transform.GetChild(0).GetComponent<Image>();
        image.sprite = item.itemImage;
        Item = item;
        name = transform.GetChild(1).GetComponent<Text>();
        name.text = item.itemName;
    }

    // 成功购买物品
    public void PurchaseItem()
    {
        GameState.CurrentPlayer.AddinventoryItem(Item);
    }
    // 玩家点击店铺中的商品
    public void ItemSelected()
    {
        if (Item != null)
        {
            Manager.SetShopSelectedItem(this);

            if (buyButton.alpha == 0)
            {
                buyButton.alpha = 1;
                buyButton.interactable = true;
                buyButton.blocksRaycasts = true;
            }
        }
    }
}