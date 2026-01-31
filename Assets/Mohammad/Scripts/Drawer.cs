using System;
using UnityEngine;

public class Drawer : MonoBehaviour
{
    public Transform target;
    public Transform targetHand;
    Vector3 startTarget;
    public float speedmove;
    public float speedmoveKnife;

    private int  moveState = 0;

    private void Start()
    {
        startTarget = transform.position;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Move()
    {
        moveState = 1;
    }
    public void MoveKnife()
    {
        if (moveState==3)
        {
            moveState = 4;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (moveState==1)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speedmove * Time.deltaTime);
            if (Vector3.Distance(transform.position,target.position)<0.05f)
            {
                moveState = 3;
                FindAnyObjectByType<CameraController_1>().moveState = 5;
            }
        }
        else if (moveState==2)
        {
            transform.position = Vector3.MoveTowards(transform.position, startTarget, speedmove * Time.deltaTime);
        }
        if (moveState==4)
        {
            transform.GetChild(0).position = Vector3.MoveTowards(transform.GetChild(0).position, targetHand.position, speedmoveKnife * Time.deltaTime);
            if (Vector3.Distance(transform.GetChild(0).position,targetHand.position)<0.05f)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                moveState = 5;
                print("Get Knife ");
                FindAnyObjectByType<CameraController_1>().stateCamera = StateCamera.Eye;
                FindAnyObjectByType<CameraController_1>().moveState = 7;
            }
        }
    }
}
