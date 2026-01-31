using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum StateCamera
{
    None,
    BackOut,
    Back,
    Eye,
    LookAtHand,
    LookAtDrawer,
    LookAtKnife,
    LookAtDoor
}

public class CameraController_1 : MonoBehaviour
{
    public StateCamera stateCamera;
    public int moveState = 0;

    public Transform backPose;
    public Transform eyePose;
    public Transform phonePose;
    public Transform drawerPose;
    public Transform doorPose;
    public float mouseSensitivity = 100f;

    private float pitch = 0f; // بالا و پایین
    private float yaw = 0f; // چپ و راست

    public float pitchMinX = -30f;
    public float pitchMaxX = 0;

    public float speedMoveCam = 5f;
    public float speeRotCam = 5f;

    public CinemachineVirtualCameraBase backCam;
    public CinemachineVirtualCameraBase eyeCam;
    public CinemachineVirtualCameraBase phoneCam;


    public Transform drawerTarget;
    public Drawer drawer;

    public Transform headTarget;
    public Transform handtarget;
    public float speedLookAtHand = 5f;

    public float speedRotateCinema = 5f;
    public GameObject light;

    public ObjectShake mobileShake;
    public GameObject fadeIN;
    public GameObject fadeOUT;
    void Start()
    {
        fadeIN.SetActive(false);
        fadeOUT.SetActive(true);
        transform.position = backPose.position;
        transform.rotation = backPose.rotation;

        Cursor.lockState = CursorLockMode.Locked;
        
        stateCamera = StateCamera.BackOut;
        StartCoroutine(IE_DelayBackOut());
        StartCoroutine(IE_DelayStart());
    }

    IEnumerator IE_DelayBackOut()
    {
        yield return new WaitForSeconds(2);
        fadeOUT.SetActive(false);
    }

    IEnumerator IE_DelayStart()
    {
        yield return new WaitForSeconds(3);
        stateCamera = StateCamera.Eye;
        moveState = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && moveState == 1)
        {
            moveState = 2;
            stateCamera = StateCamera.LookAtHand;
        }

        if (Input.GetKeyDown(KeyCode.F) && moveState == 3)
        {
            moveState = 4;
            stateCamera = StateCamera.LookAtDrawer;
        }

        if (Input.GetKeyDown(KeyCode.F) && moveState == 5)
        {
            moveState = 6;
            drawer.MoveKnife();
        }
        
        if (Input.GetKeyDown(KeyCode.F) && moveState == 8)
        {
            print("Click Door");
            moveState = 9;
            
            stateCamera = StateCamera.LookAtDoor;
        }
    }

    private void FixedUpdate()
    {
        if (stateCamera == StateCamera.LookAtHand)
        {
            GoToHand();
        }

        if (stateCamera == StateCamera.LookAtDrawer)
        {
            LookAtDrawer();
        }

        if (stateCamera == StateCamera.Eye)
        {
            if (moveState==7)
            {
                print("checkeye");
            }
            GoToEye();
        }

        if (stateCamera == StateCamera.Back)
        {
            GoToBack();
        }

        if (stateCamera != StateCamera.None)
        {
           // MoveCamera();
        }
        if (stateCamera == StateCamera.LookAtDoor)
        {
            GoToDoor();
        }
    }

    void MoveCamera()
    {
        if (stateCamera == StateCamera.None || stateCamera == StateCamera.Eye)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            yaw += mouseX;
            pitch -= mouseY;
            pitch = Mathf.Clamp(pitch, pitchMinX, pitchMaxX);
            transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
        }
    }

    public void GoToEye()
    {
        transform.position = Vector3.MoveTowards(transform.position, eyePose.position, speedMoveCam * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            eyePose.rotation,
            speeRotCam * Time.deltaTime
        );
        print("GoToEye");
        if (Vector3.Distance(transform.position, eyePose.position) < 1)// &&
           // Quaternion.Angle(transform.rotation, eyePose.rotation) < 10f)
        {
            if (moveState==7)
            {
                
                print("GoToEye DONE!");
                moveState = 8;
                StartCoroutine(IE_InLight());
            }
        }
    }
    IEnumerator IE_InLight()
    {
        yield return new WaitForSeconds(1);
        light.SetActive(true);
        
        StartCoroutine(IE_HideLight());
    }
    IEnumerator IE_HideLight()
    {
        yield return new WaitForSeconds(2);
        light.SetActive(false);
    }
    private Transform targetgoal;

    public void GoToBack()
    {
        transform.position = Vector3.MoveTowards(transform.position, backPose.position, speedMoveCam * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            backPose.rotation,
            speeRotCam * Time.deltaTime
        );
    }

    public void GoToHand()
    {
        transform.position = Vector3.MoveTowards(transform.position, phonePose.position, speedMoveCam * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            phonePose.rotation,
            speeRotCam * Time.deltaTime
        );
        StartCoroutine(IE_EndVideo());
        
        if (Vector3.Distance(transform.position, phonePose.position) < 1 &&
            Quaternion.Angle(transform.rotation, phonePose.rotation) < 5f)
        {
            print("GoToHand DONE!");
            StartCoroutine(IE_DelayShake());
        }
    }
    IEnumerator IE_DelayShake()
    {
        yield return new WaitForSeconds(2);
        mobileShake.StartShake();
    }
    IEnumerator IE_EndVideo()
    {
        yield return new WaitForSeconds(3);
        stateCamera = StateCamera.Eye;
        moveState = 3;
    }

    void LookAtDrawer()
    {
        transform.position =
            Vector3.MoveTowards(transform.position, drawerPose.position, speedMoveCam * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            drawerPose.rotation,
            speeRotCam * Time.deltaTime
        );
        if (Vector3.Distance(transform.position, drawerPose.position) < 1 &&
            Quaternion.Angle(transform.rotation, drawerPose.rotation) < 5f)
        {
            print("LookAtDrawer DONE!");
            drawer.Move();
            stateCamera = StateCamera.None;
        }
    }

    public void GoToDoor()
    {
        transform.position = Vector3.MoveTowards(transform.position, doorPose.position, speedMoveCam * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            doorPose.rotation,
            speeRotCam * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, doorPose.position) < 1 )//&&
            //Quaternion.Angle(transform.rotation, doorPose.rotation) < 5f)
        {
            print("GoToDoor DONE!");
            if (moveState==9)
            {
                moveState = 10;
                fadeIN.SetActive(true);
                StartCoroutine(IE_DelayWinLevel1());
            }
        }
    }
    IEnumerator IE_DelayWinLevel1()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Main");
    }
}