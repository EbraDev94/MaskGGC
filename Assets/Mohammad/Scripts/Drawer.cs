using System;
using UnityEngine;

public class Drawer : MonoBehaviour
{
    public Transform target;
    Vector3 startTarget;
    public float speedmove;

    private bool move = false;

    private void Start()
    {
        startTarget = transform.position;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Move()
    {
        move = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speedmove * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startTarget, speedmove * Time.deltaTime);
        }
    }
}
