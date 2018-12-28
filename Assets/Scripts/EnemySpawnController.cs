using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    [SerializeField] public List<WaveConfig> waveConfigs;
    [SerializeField] public int startingWave = 0;
    [SerializeField] public bool shouldLoopWaves = false;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (shouldLoopWaves);
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int i = startingWave; i < waveConfigs.Count; i++)
        {
            yield return StartCoroutine(SpawnAllEnemiesInWave(waveConfigs[i]));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        int numberOfEnemiesInWave = waveConfig.GetNumberOfEnemiesInWave();
        float timeBetweenSpawns = waveConfig.GetTimeBetweenSpawns();

        for (int i = 0; i < numberOfEnemiesInWave; i++) {
            var newEnemy = Instantiate(
                waveConfig.GetEnemyPrefab(),
                waveConfig.GetWaypoints()[0].transform.position,
                Quaternion.identity
            );
            newEnemy.GetComponent<EnemyPathController>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }
    
}
