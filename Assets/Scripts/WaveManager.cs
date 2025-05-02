using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour, IDataPersistence
{
    public int CurrentWave { get; private set; }

    [SerializeField] private List<Enemy> possibleEnemies = new List<Enemy>();
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] public List<GameObject> spawnedEnemies = new List<GameObject>();
    [SerializeField] private List<GameObject> enemiesToSpawn = new List<GameObject>();

    [SerializeField] private int maxSpawnedEnemies = 5;
    [SerializeField] private int waveValueMultiplier = 10;
    private int wavePoints;
    private bool waveStarted = false;

    [SerializeField] private float waveDuration;
    private float waveTimer;
    private float spawnInterval;
    private float spawnTimer;

    public Action OnWaveStart;
    public Action OnWaveEnd;
    public Action<int> OnEnemiesRemainingUpdate;

    /// <summary>
    /// Start the wave of the undead. Dificulty based on point system.
    /// </summary>
    public void StartWave()
    {
        GenerateWave();
        Debug.Log("generated wave");
        spawnInterval = waveDuration / enemiesToSpawn.Count;
        waveTimer = waveDuration;
        maxSpawnedEnemies += CurrentWave * (waveValueMultiplier / 2);
        OnWaveStart?.Invoke();
        waveStarted = true;
    }

    public void StopWave()
    {
        waveStarted = false;
        spawnedEnemies.Clear();
        CurrentWave++;

        OnWaveEnd?.Invoke();
    }

    private void FixedUpdate()
    {
        if (waveStarted)
        {
            Debug.Log(spawnTimer);
            if (spawnTimer <= 0)
            {
                // spawn an enemy
                if (enemiesToSpawn.Count > 0 && spawnedEnemies.Count < maxSpawnedEnemies)
                {
                    int randSpawnIndex = Random.Range(0, spawnPoints.Count);
                    Debug.Log("Spawning enemy!");
                    GameObject enemy = Instantiate(enemiesToSpawn[0], spawnPoints[randSpawnIndex].position, Quaternion.identity);
                    enemiesToSpawn.RemoveAt(0);
                    spawnedEnemies.Add(enemy);

                    spawnTimer = Random.Range(0f, spawnInterval);
                } else if (enemiesToSpawn.Count == 0 && spawnedEnemies.Count == 0)
                {
                    waveTimer = 0;
                    StopWave();
                }
            } else
            {
                spawnTimer -= Time.fixedDeltaTime;
                waveTimer -= Time.fixedDeltaTime;
            }
            OnEnemiesRemainingUpdate?.Invoke(enemiesToSpawn.Count + spawnedEnemies.Count);
        }
    }

    private void GenerateWave()
    {
        wavePoints = CurrentWave * waveValueMultiplier;
        GenerateEnemies();
    }

    /// <summary>
    /// Generate the mix of enemies that will be spawned in the coming wave using a point based system.
    /// </summary>
    private void GenerateEnemies()
    {
        List<GameObject> enemyList = new List<GameObject>();

        // while we have wave points to spend
        while (wavePoints > 0)
        {
            // get a random enemy form the list of possible enemies
            int randIndex = Random.Range(0, possibleEnemies.Count);
            Enemy enemy = possibleEnemies[randIndex];

            // check if we can afford this enemy
            if (wavePoints - enemy.cost >= 0)
            {
                enemyList.Add(enemy.enemyPrefab);
                wavePoints -= enemy.cost;
            } else if (wavePoints <= 0)
            {
                break;
            }
            Debug.Log("loooooping!");
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = enemyList;
    }

    void IDataPersistence.LoadGameData(GameData data)
    {
        this.CurrentWave = data.CurrentWave;
    }

    void IDataPersistence.SaveGameData(ref GameData data)
    {
        data.CurrentWave = this.CurrentWave;
    }
}

[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public int cost;
}