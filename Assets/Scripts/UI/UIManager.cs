using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject startWaveButton;

    public void ShowShopUI()
    {
        shopUI.SetActive(true);
    }

    public void HideShopUI() 
    { 
        shopUI.SetActive(false); 
    }

    public void ShowStartWaveButton()
    {
        startWaveButton.SetActive(true);
    }
    public void HideStartWaveButton()
    {
        startWaveButton.SetActive(false);
    }
}
