using UnityEngine;

public class ObstacleControl : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private SpawnManager spawnManager;

    void Update()
    {
        if (transform.position.z<=player.position.z-10)
        {
            spawnManager.CheckObstacle(gameObject);
        }
    }
}
