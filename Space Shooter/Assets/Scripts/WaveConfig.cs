using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float time_between_spawns = 0.5f;
    [SerializeField] float spawn_random_factor = 0.3f;
    [SerializeField] int number_of_enemies = 5;
    [SerializeField] float move_speed = 2f;

    public GameObject GetEnemyPrefab() { return enemyPrefab; }
    public List<Transform> GetWaypoints() 
    {
        var wave_waypoints = new List<Transform>();
        foreach (Transform point in pathPrefab.transform)
        {
            wave_waypoints.Add(point);
        }
        return wave_waypoints;
    }
    public float GetTimeBetweenSpawns() { return time_between_spawns; }
    public float GetSpawnRandomFactor() { return spawn_random_factor; }
    public int GetNumberOfEnemies() { return number_of_enemies; }
    public float GetMoveSpeed() { return move_speed; }

}
