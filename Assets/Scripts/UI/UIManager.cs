
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject startWaveButton;
    [SerializeField] private GameObject playerStatsUI;
    [SerializeField] private GameObject gameOverScreen;

    [SerializeField] private TextMeshProUGUI currentWaveNumText;
    [SerializeField] private TextMeshProUGUI currentTotalScrapText;
    [SerializeField] private TextMeshProUGUI totalKillsText;
    [SerializeField] private TextMeshProUGUI remainingEnemiesText;

    [SerializeField] private TextMeshProUGUI gameOverScreenKillsText;
    [SerializeField] private TextMeshProUGUI gameOverScreenWaveText;
    [SerializeField] private TextMeshProUGUI gameOverScreenBulletsShotText;


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

    public void ShowGameOverScreen()
    {
        gameOverScreen?.SetActive(true);
    }    
    public void HideGameOverScreen()
    {
        gameOverScreen?.SetActive(false);
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

    public void UpdateGameOverStats()
    {
        PlayerStats stats = GameManager.Instance.PlayerStats;
        gameOverScreenBulletsShotText.text = $"Bullets Shot: {stats.TotalBulletsShot.ToString()}";
        gameOverScreenKillsText.text = $"Zombies Killed: {stats.TotalKills.ToString()}";
        gameOverScreenWaveText.text = $"Waves Cleared: {(GameManager.Instance.WaveManager.CurrentWave - 1).ToString()}";
    }

    public void OnGameOverExitButtonClicked()
    {
        // load to before faile dwave then quit since we save on quit
        GameManager.Instance.saveSystem.RestartGame();
        Application.Quit();
    }

    public void OnGameOverResetButtonClicked()
    {
        // load the last save
        GameManager.Instance.saveSystem.RestartGame();
    }
}
