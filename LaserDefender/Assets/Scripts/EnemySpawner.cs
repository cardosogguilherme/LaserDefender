using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigS0> waveConfigs;
    WaveConfigS0 currentWave;
    [SerializeField] float timeBetweenWaves = 0f;

    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    public WaveConfigS0 GetCurrentWave() { return currentWave; }

    IEnumerator SpawnEnemyWaves()
    {
        foreach (WaveConfigS0 currentWave in waveConfigs)
        {
            this.currentWave = currentWave;

            for (int i = 0; i < currentWave.GetEnemyCount(); i++)
            {
                Instantiate(
                    currentWave.GetEnemyPrefab(i),
                    currentWave.GetStartingWaypoint().position,
                    Quaternion.identity,
                    transform);

                yield return new WaitForSecondsRealtime(currentWave.GetRandomSpawnTime());
            }

            yield return new WaitForSecondsRealtime(timeBetweenWaves);
        }
    }

}
