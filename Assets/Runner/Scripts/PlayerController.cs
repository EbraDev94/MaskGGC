using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

enum PlayerPose
{
    Left,
    Center,
    Right
}

public class PlayerController : MonoBehaviour
{
    
    [Header("Forward Move")]
    public float forwardSpeed = 8f;

    [Header("Lane Move")]
    public float laneDistance = 2f;   
    public float laneSwitchSpeed = 10f;

    int currentLane = 0; 
    float targetX;
    public bool isDead = false;
    private bool isSlide = false;
    
    [SerializeField] private GameManagerRunner _gameManagerRunner;
    
    
    
    [SerializeField] float groundDistance = 1f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    
    
    [SerializeField] private float moveForce = 3;
    
    public bool move = false;
    [SerializeField] private PlayerPose _playerPose;

    private Rigidbody rb;
    private Animator _animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _playerPose = PlayerPose.Center;
    }
  
    public void StartRunner()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
        _gameManagerRunner.GetComponent<CinemachineBrain>().enabled = true;
        move = true;
    }
    IEnumerator IEDelayStart()
    {
        yield return new WaitForSeconds(1);
        move = true;
    }

    // Update is called once per frame
    void Update()
    {
    if (isDead) return;
        if (move)
        {

            if (Input.GetKeyDown(KeyCode.A))
            {
                MoveLeft();
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                MoveRight();
            }
            
            if (Input.GetKeyDown(KeyCode.S))
            {
                Slide();
            }
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
    }
    
    void FixedUpdate()
    {
    if (isDead) return;
        if (move)
        {
            MoveForward();
            MoveLane();
        }
    }
    void MoveForward()
    {
        Vector3 vel = rb.linearVelocity;
        vel.z = forwardSpeed;
        rb.linearVelocity = vel;
    }

    void MoveLane()
    {
        Vector3 pos = rb.position;

        pos.x = Mathf.MoveTowards(
            pos.x,
            targetX,
            laneSwitchSpeed * Time.fixedDeltaTime
        );

        rb.MovePosition(pos);
    }
    public void MoveLeft()
    {
        if (currentLane > -1)
        {
            currentLane--;
            targetX = currentLane * laneDistance;
        }
    }

    public void MoveRight()
    {
        if (currentLane < 1)
        {
            currentLane++;
            targetX = currentLane * laneDistance;
        }
    }

    public void Slide()
    {
        isSlide = true;
        _animator.SetTrigger("Slide");
                
        StartCoroutine(IE_Slide());
    }
    public void Jump()
    {
        if (IsGrounded())
        {
            print("Jump");
            rb.AddForce(Vector3.up * moveForce, ForceMode.Impulse);
            _animator.SetTrigger("Jump");
        }
    }

    public void SetTurn()
    {
        _animator.SetTrigger("Turn");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fish")
        {
            print("Fish");
            other.gameObject.transform.parent.gameObject.SetActive(false);
            _gameManagerRunner.AddScore();
        }
        else if (other.tag == "Obstacle")
        {
         rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            move = false;
            isDead = true;
            _animator.SetTrigger("Die");
            StartCoroutine(IE_DIE());
        }
        else if (other.tag == "Magnet")
        {
            other.gameObject.SetActive(false);
            _gameManagerRunner.SetMagnet();
        }
        else if (other.tag == "ObstacleSlide" && !isSlide)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            move = false;
            isDead = true;
            _animator.SetTrigger("Die");
            StartCoroutine(IE_DIE());
        }
    }
    IEnumerator IE_Slide()
    {
        yield return new WaitForSeconds(2);
        isSlide = false;
    }
    IEnumerator IE_DIE()
    {
        yield return new WaitForSeconds(3);
        _gameManagerRunner.GameOver();
    }

    private bool grounded;
    bool IsGrounded()
    {
        Debug.DrawRay(
            groundCheck.position,
            Vector3.down * groundDistance,
            Color.red
        );
        grounded = Physics.Raycast(groundCheck.position, Vector3.down, groundDistance, groundLayer);
        print(grounded);
        return grounded;
    }
}