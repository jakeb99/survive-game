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
        // check that the player has enough money
        // if so take the cost amount away from the players money

        // enter plavement mode
        itemPlacementSystem.SetPlaceableObjectPrefab(itemPrefab);
        itemPlacementSystem.SetPreviewObjectPrefab(itemPreviewPrefab);

        itemPlacementSystem.EnterPlacementMode();
    }

    
}
