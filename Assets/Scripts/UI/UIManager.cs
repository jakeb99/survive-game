using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject shopUI;

    public void ShowShopUI()
    {
        shopUI.SetActive(true);
    }

    public void HideShopUI() 
    { 
        shopUI.SetActive(false); 
    }
}
