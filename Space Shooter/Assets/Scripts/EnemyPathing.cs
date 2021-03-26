using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{

    WaveConfig waveConfig;
    List<Transform> waypoints;
    int waypoint_index = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypoint_index].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void Move()
    {
        if (waypoint_index <= waypoints.Count - 1)
        {
            var target_pos = waypoints[waypoint_index].transform.position;
            var move_speed = waveConfig.GetMoveSpeed();
            var movement_this_frame = move_speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target_pos, movement_this_frame);
            if (transform.position == target_pos)
            {
                waypoint_index++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
