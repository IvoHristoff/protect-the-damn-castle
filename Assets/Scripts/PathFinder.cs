using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    EnemySpawner enemySpawner;
    WaveConfigSO waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;
    [SerializeField] float enemySpeed = 2f; 

    void Start()
    {
        waveConfig = enemySpawner.GetCurrentWave();
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].position;
    }

    void Awake()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void Update()
    {
        FollowPath();
    }

    void FollowPath()
    {
        if (waypointIndex < waypoints.Count)
        {
            Vector3 targetPosition = waypoints[waypointIndex].position;
            float delta = enemySpeed * waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public float GetDistanceToGoal()
    {
        float distance = 0;
        distance += Vector2.Distance(gameObject.transform.position, waypoints[waypointIndex].transform.position);
        for (int i = waypointIndex + 1; i < waypoints.Count - 1; i++)
        {
            Vector3 startPosition = waypoints[i].transform.position;
            Vector3 endPosition = waypoints[i + 1].transform.position;
            distance += Vector2.Distance(startPosition, endPosition);
        }
        return distance;
    }

    public float GetEnemySpeed()
    {
        return enemySpeed;
    }

    public void SetEnemySpeed(float moveSpeed)
    {
        enemySpeed = moveSpeed;
    }

 
}
