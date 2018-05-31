using UnityEngine;
using UnityEngine.UI;
public class ShopManager : MonoBehaviour
{
    GameObject soundEffectsSource;
    public AudioClip buySound;

    public Image PurchaseItemDisplay;
    public ShopSlot[] ItemSlots;
    public InventoryItem[] ShopItems;
    private static ShopSlot SelectedShopSlot;
    private int nextSlotIndex = 0;
    public Text shopKeeperText;

    void Awake()
    {
        soundEffectsSource = GameObject.FindGameObjectWithTag("Sound");
    }

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

    // 选择要购买的物品
    public void SetShopSelectedItem(ShopSlot slot)
    {
        SelectedShopSlot = slot;
        PurchaseItemDisplay.sprite = slot.Item.itemImage;
        shopKeeperText.text = " ";
    }

    // 购买选中的物品
    public static void PurchaseSelectedItem()
    {
        SelectedShopSlot.PurchaseItem();
    }

    // 在选中商品后购买商品
    public void ConfirmPurchase()
    {
        if (soundEffectsSource != null)
        {
            soundEffectsSource.GetComponent<AudioSource>().PlayOneShot(buySound);
        }
        PurchaseSelectedItem();
        shopKeeperText.text = "谢谢!";
    }

    public void LeaveTheShop()
    {
        NavigationManager.NavigateTo("Town");
    }
}