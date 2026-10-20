using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    // CREATE A WAVE FOR SPAWNUNG
    [System.Serializable]
    public class Wave
    {
        public GameObject EnemyPrefab;
        public float SpawmTimer;
        public float SpawmInterval;
        public int EnemiesPerWave;
        public int EnemiesSpawnedCount;

    }
    // ENEMIES CONTEINER
    public List<Wave> Waves;
    public int WaveNumber;
    public Transform MinPos;
    public Transform MaxPos;

    // spawn rules
    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.currentState != GameManager.GameState.Gameplay)
        {
            return;
        }

        if (Player.Instance != null && Player.Instance.gameObject.activeSelf)
        {
            Waves[WaveNumber].SpawmTimer += Time.deltaTime;
            if (Waves[WaveNumber].SpawmTimer >= Waves[WaveNumber].SpawmInterval)
            {
                Waves[WaveNumber].SpawmTimer = 0;
                SpawnEnemy();
            }
            if (Waves[WaveNumber].EnemiesSpawnedCount >= Waves[WaveNumber].EnemiesPerWave)
            {
                Waves[WaveNumber].EnemiesSpawnedCount = 0;
                if (Waves[WaveNumber].SpawmInterval > 0.3f)
                {
                    Waves[WaveNumber].SpawmInterval *= 0.9f;
                }
                WaveNumber++;
            }
            if (WaveNumber >= Waves.Count) 
            { 
                WaveNumber = 0;
            }
        }

    }

    // spawning enemy from chosem position
    private void SpawnEnemy()
    {
        Instantiate(Waves[WaveNumber].EnemyPrefab, RandomSpawnPoint(), transform.rotation);
        Waves[WaveNumber].EnemiesSpawnedCount++;
    }

    //set a random position for spawm in range
    private Vector2 RandomSpawnPoint()
    {
        Vector2 spawnPoint;

       if(Random.Range(0f,1f) > 0.5)
        {
            spawnPoint.x=Random.Range(MinPos.position.x, MaxPos.position.x);
            if(Random.Range(0f, 1f) > 0.5)
            {
                spawnPoint.y = MinPos.position.y;
            }
            else
            {
                spawnPoint.y = MaxPos.position.y;
            }
        }
        else
        {
            spawnPoint.y = Random.Range(MinPos.position.y, MaxPos.position.y);
            if (Random.Range(0f, 1f) > 0.5)
            {
                spawnPoint.x = MinPos.position.x;
            }
            else
            {
                spawnPoint.x = MaxPos.position.x;
            }
        }


        return spawnPoint;
    }
}
