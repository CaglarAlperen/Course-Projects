using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int starting_wave = 0;
    [SerializeField] bool looping = false;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } while (looping);       
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int i = starting_wave; i < waveConfigs.Count; i++)
        {
            var current_wave = waveConfigs[i];
            yield return StartCoroutine(SpawnAllEnemiesInWave(current_wave));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig wave_config)
    {
        for (int i = 0; i < wave_config.GetNumberOfEnemies(); i++)
        {
            var newEnemy = Instantiate(wave_config.GetEnemyPrefab(), wave_config.GetWaypoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(wave_config);
            yield return new WaitForSeconds(wave_config.GetTimeBetweenSpawns());
        }      
    }
}
