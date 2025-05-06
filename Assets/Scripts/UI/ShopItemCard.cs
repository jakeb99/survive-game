using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemCard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemHealthText;
    [SerializeField] private TextMeshProUGUI itemCostText;
    [SerializeField] private Image itemImageRect;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private GameObject itemPreviewPrefab;
    private PlacementSystem itemPlacementSystem;
    private ShopItemData itemData;

    

    private void Awake()
    {
        itemPlacementSystem = FindFirstObjectByType<PlacementSystem>();
        itemData = itemPrefab.GetComponent<ShopItemData>();
    }

    private void Start()
    {
        itemNameText.text = itemData.itemName;
        itemCostText.text = $"Cost: {itemData.itemCost.ToString()}";
        itemHealthText.text = $"Durability: {itemPrefab.GetComponent<Health>().maxHealth.ToString()}";
    }

    public void BuyItem()
    {
        // try to buy the item
        if (!GameManager.Instance.DecreaseScrap(itemData.itemCost)) return;


        // enter plavement mode
        itemPlacementSystem.SetPlaceableObjectPrefab(itemPrefab);
        itemPlacementSystem.SetPreviewObjectPrefab(itemPreviewPrefab);

        itemPlacementSystem.EnterPlacementMode();
    }

    
}
