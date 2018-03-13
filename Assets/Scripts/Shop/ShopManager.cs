using UnityEngine;
using UnityEngine.UI;
public class ShopManager : MonoBehaviour
{
    public Image PurchaseItemDisplay;
    public ShopSlot[] ItemSlots;
    public InventoryItem[] ShopItems;
    private static ShopSlot SelectedShopSlot;
    private int nextSlotIndex = 0;
    public Text shopKeeperText;

    void Start()
    {
        // 循环遍历商店中所有可用槽位，并从库存中挑选物品放入其中
        if (ItemSlots.Length > 0 && ShopItems.Length > 0)
        {
            for (int i = 0; i < ShopItems.Length; i++)
            {
                if (nextSlotIndex > ItemSlots.Length)
                    break;
                ItemSlots[nextSlotIndex].AddShopItem(ShopItems[i]);
                ItemSlots[nextSlotIndex].Manager = this;
                nextSlotIndex++;
            }
        }
    }

    // 购买当前选中的商店物品
    public void SetShopSelectedItem(ShopSlot slot)
    {
        SelectedShopSlot = slot;
        PurchaseItemDisplay.sprite = slot.Item.itemImage;
        shopKeeperText.text = " ";
    }

    // 购买选中物品
    public static void PurchaseSelectedItem()
    {
        SelectedShopSlot.PurchaseItem();
    }

    public void ConfirmPurchase()
    {
        PurchaseSelectedItem();
        shopKeeperText.text = "Thanks!";
    }

    public void LeaveTheShop()
    {
        NavigationManager.NavigateTo("Town");
    }
}