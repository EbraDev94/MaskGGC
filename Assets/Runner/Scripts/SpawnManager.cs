using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float LastPlane;
    [SerializeField] private float LastObstacle;

    public void CheckPlane(GameObject gm)
    {
        gm.transform.position = new Vector3(0, 0, LastPlane);
        LastPlane += 36;
    }
    public void CheckObstacle(GameObject gm)
    {
        gm.transform.position = new Vector3(gm.transform.position.x, 0, LastObstacle);
        LastObstacle += 36;
    }
}
