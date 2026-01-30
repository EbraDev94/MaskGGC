using UnityEngine;

public class Magnet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fish")
        {
            other.gameObject.transform.parent.GetComponent<FishMovement>().allowMove = true;
        }
    }
}
