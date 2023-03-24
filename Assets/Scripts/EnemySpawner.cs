using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    WaveConfigSO currentWave;

    private bool wavesOver = false;

    

    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());

    }

    private void OnEnable()
    {
        CastleHealth.OnCastleDestroyed += DisableSpawn;
    }

    private void OnDisable()
    {
        CastleHealth.OnCastleDestroyed -= DisableSpawn;
    }

    private void DisableSpawn()
    {
       gameObject.SetActive(false);
    }

    public bool WavesComplete()
    {
        return wavesOver;
    }



    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }

    IEnumerator SpawnEnemyWaves()
    {
        
        for (int y = 0; y < waveConfigs.Count; y++)
        {
            
            currentWave = waveConfigs[y];
            for (int i = 0; i < currentWave.GetEnemyCount(); i++)
            {
                Instantiate(currentWave.GetEnemyPrefab(i), currentWave.GetStartingWaypoint().position, Quaternion.identity, transform);
                yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                
                
            }
            
            if (y == waveConfigs.Count - 1)
            {
                wavesOver = true;
            }


            yield return new WaitForSeconds(timeBetweenWaves);
            
          
        }
       


    }

}