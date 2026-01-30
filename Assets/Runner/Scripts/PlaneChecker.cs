using UnityEngine;

public class PlaneChecker : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private GameObject fıshes;
    [SerializeField] private GameObject Magnet;
    [SerializeField] private GameObject Obstacle;

    void Update()
    {
        if (transform.position.z<=player.position.z-50)
        {
            print(gameObject.name);
            spawnManager.CheckPlane(gameObject);
            SetPoseFishes();
            SetMagnets();
            SetObstacle();
        }
    }

    void SetPoseFishes()
    {
        print(fıshes.transform.GetChild(0).childCount);
        for (int i = 0; i < fıshes.transform.GetChild(0).childCount; i++)
        {
            fıshes.transform.GetChild(0).GetChild(i).gameObject.SetActive(true);
            fıshes.transform.GetChild(0).GetChild(i).GetComponent<FishMovement>().allowMove = false;
            fıshes.transform.GetChild(0).GetChild(i).GetComponent<FishMovement>().SetPose();
        }
        for (int i = 0; i < fıshes.transform.GetChild(1).childCount; i++)
        {
            fıshes.transform.GetChild(1).GetChild(i).gameObject.SetActive(true);
            fıshes.transform.GetChild(1).GetChild(i).GetComponent<FishMovement>().allowMove = false;
            fıshes.transform.GetChild(1).GetChild(i).GetComponent<FishMovement>().SetPose();
        }
        for (int i = 0; i < fıshes.transform.GetChild(2).childCount; i++)
        {
            fıshes.transform.GetChild(2).GetChild(i).gameObject.SetActive(true);
            fıshes.transform.GetChild(2).GetChild(i).GetComponent<FishMovement>().allowMove = false;
            fıshes.transform.GetChild(2).GetChild(i).GetComponent<FishMovement>().SetPose();
        }
    }

    void SetMagnets()
    {
        for (int i = 0; i < Magnet.transform.childCount; i++)
        {
            Magnet.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    void SetObstacle()
    {
        for (int i = 0; i < Obstacle.transform.childCount; i++)
        {
            Obstacle.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
