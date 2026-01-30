using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public bool allowMove;
    private float sp;
    private GameObject target;

    private Vector3 startPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.localPosition;
        target = FindObjectOfType<PlayerController>().gameObject;
        sp = 18;
    }

    // Update is called once per frame
    void Update()
    {
        if (allowMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.transform.position.x,1,target.transform.position.z), sp * Time.deltaTime);
        }
    }

    public void SetPose()
    {
        transform.localPosition = startPos;
    }
}
