using System;
using UnityEngine;
public class Sc_Woman : MonoBehaviour
{
    public Sc_Man man;
    public Transform target;       // هدفی که پلیر باید به سمتش بره
    public float moveSpeed = 5f;   // سرعت حرکت
    public float rotationSpeed = 5f; // سرعت چرخش نرم
    private CharacterController controller;
    public float rotationThreshold = 1f;
    private bool move = false;
    public bool startmove = false;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (startmove)
        {
            if (target == null) return;
            if (!move)
            {
                RotateMan();
            }

            if (move)
            {
                MoveTowardsTarget();
            }
        }
    }

    void RotateMan()
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // بررسی زاویه بین جهت فعلی و هدف
            float angle = Quaternion.Angle(transform.rotation, targetRotation);
            if (angle < rotationThreshold)
            {
                // چرخش تقریبا کامل شده
                move = true;
            }
        }
    }
    void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            man.startmove = true;
            startmove = true;
        }
    }
}