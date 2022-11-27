using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigS0> waveConfigs;
    WaveConfigS0 currentWave;
    [SerializeField] float timeBetweenWaves = 0f;
    [SerializeField] bool isLooping;

    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    public WaveConfigS0 GetCurrentWave() { return currentWave; }

    IEnumerator SpawnEnemyWaves()
    {
        int index = 0;
        
        do
        {
            currentWave = waveConfigs[index];

            for (int i = 0; i < currentWave.GetEnemyCount(); i++)
            {
                Instantiate(
                    currentWave.GetEnemyPrefab(i),
                    currentWave.GetStartingWaypoint().position,
                    Quaternion.identity,
                    transform);

                yield return new WaitForSecondsRealtime(currentWave.GetRandomSpawnTime());
            }

            index = index < waveConfigs.Count - 1 ? index + 1 : 0; 

            yield return new WaitForSecondsRealtime(timeBetweenWaves);
        } while (isLooping);
    }

}
