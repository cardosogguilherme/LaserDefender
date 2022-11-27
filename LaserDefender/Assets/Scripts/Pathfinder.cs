using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] WaveConfigS0 waveConfig;
    List<Transform> wayPoints = new List<Transform>();
    int wayPointIndex = 0;

    void Start()
    {
        wayPoints = waveConfig.GetWaypoints();
        transform.position = wayPoints[wayPointIndex].position;

    }

    void Update()
    {
        FollowPath();
    }

    void FollowPath()
    {
        if(wayPointIndex < wayPoints.Count)
        {
            Vector3 targetPosition = wayPoints[wayPointIndex].position;
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);

            if(transform.position == targetPosition)
            {
                wayPointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
