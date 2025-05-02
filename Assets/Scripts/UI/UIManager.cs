using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject startWaveButton;
    [SerializeField] private GameObject playerStatsUI;

    [SerializeField] private TextMeshProUGUI currentWaveNumText;
    [SerializeField] private TextMeshProUGUI currentTotalScrapText;
    [SerializeField] private TextMeshProUGUI totalKillsText;
    [SerializeField] private TextMeshProUGUI remainingEnemiesText;

    private void Awake()
    {

    }

    private void Start()
    {
        GameManager.Instance.WaveManager.OnWaveEnd += UpdateWaveNumberText;
        GameManager.Instance.WaveManager.OnEnemiesRemainingUpdate += UpdateRemainingEnemiesText;
        GameManager.Instance.OnUpdateScrapTotal += UpdateTotalScrapText;

        // update to starting values
        UpdateWaveNumberText();
        UpdateRemainingEnemiesText(0);
        UpdateTotalScrapText(GameManager.Instance.PlayerStats.TotalScrap);
        UpdateTotalKillsText();
    }

    public void ShowShopUI()
    {
        shopUI?.SetActive(true);
    }

    public void HideShopUI() 
    { 
        shopUI?.SetActive(false); 
    }

    public void ShowStartWaveButton()
    {
        startWaveButton?.SetActive(true);
    }
    public void HideStartWaveButton()
    {
        startWaveButton?.SetActive(false);
    }

    public void ShowPlayerStats()
    {
        playerStatsUI?.SetActive(true);
    }

    public void HidePlayerStats()
    {
        playerStatsUI?.SetActive(false);
    }

    public void UpdateWaveNumberText()
    {
        currentWaveNumText.text = $"Wave: {GameManager.Instance.WaveManager.CurrentWave.ToString()}";
    }

    public void UpdateTotalScrapText(int total)
    {
        currentTotalScrapText.text = $"Scrap: {total.ToString()}";
    }

    public void UpdateTotalKillsText()
    {
        totalKillsText.text = $"Total Kills: {GameManager.Instance.PlayerStats.TotalKills.ToString()}";
    }

    public void UpdateRemainingEnemiesText(int numEnemies)
    {
        remainingEnemiesText.text = $"Remaining: {numEnemies}";
    }
}
