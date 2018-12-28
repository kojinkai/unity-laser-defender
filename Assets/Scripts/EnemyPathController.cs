using System.Collections.Generic;
using UnityEngine;

public class EnemyPathController : MonoBehaviour
{
    WaveConfig waveConfig;
    List<Transform> waypoints;
    float moveSpeed;
    int currentWaypointIndex = 0;

    private void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        moveSpeed = waveConfig.GetMoveSpeed();
        transform.position = waypoints[currentWaypointIndex].transform.position;
    }

    private void Update()
    {
        MoveEnemy();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void MoveEnemy()
    {
        // if there are still waypoints left to move towards...
        if (currentWaypointIndex <= waypoints.Count - 1)
        {
            // get the transform position of the current waypoint and move speed and move towards the waypoint
            var targetPosition = waypoints[currentWaypointIndex].transform.position;
            var movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(
                transform.position,
                targetPosition,
                movementThisFrame);
            // Once we are at the waypoint increment the waypoint index
            if (transform.position == targetPosition)
            {
                currentWaypointIndex++;
            }
        }
        // else we have moved through all the waypoints so destroy the enemy
        else
        {
            Destroy(gameObject);
        }
    }
}
