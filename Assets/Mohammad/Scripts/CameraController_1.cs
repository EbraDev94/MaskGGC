using System;
using Unity.Cinemachine;
using UnityEngine;
public enum StateCamera
{
    None,
    Back,
    Eye,
    LookAtHand,
    LookAtDrawer,
    LookAtKnife
}

public class CameraController_1 : MonoBehaviour
{
    public StateCamera stateCamera;
    
    public CinemachineVirtualCameraBase backCam;
    public CinemachineVirtualCameraBase eyeCam;
    
    
    public Transform drawerTarget;
    public Drawer drawer;
    
    public Transform headTarget;
    public Transform handtarget;
    public float speedLookAtHand = 5f;
    void Start()
    {
        backCam.Priority = 10;
        eyeCam.Priority = 0;
    }
    private void FixedUpdate()
    {
        if (stateCamera==StateCamera.LookAtHand)
        {
            LookAtHand();
        }
        if (stateCamera==StateCamera.LookAtDrawer)
        {
            LookAtDrawer();
        }
        if (stateCamera==StateCamera.Eye)
        {
            GoToEye();
        }
        if (stateCamera==StateCamera.Back)
        {
            GoToBack();
        }
    }
    public void GoToEye()
    {
        eyeCam.Priority = 20;
        backCam.Priority = 0;
    }
    public void GoToBack()
    {
        backCam.Priority = 20;
        eyeCam.Priority = 0;
    }
    void LookAtHand()
    {
        if (!handtarget) return;

        Vector3 dir = handtarget.position - headTarget.position;
        Quaternion rot = Quaternion.LookRotation(dir);
        headTarget.rotation = Quaternion.Slerp(
            headTarget.rotation,
            rot,
            Time.deltaTime * speedLookAtHand
        );
        Quaternion targetRot = Quaternion.LookRotation(dir);

        float angle = Quaternion.Angle(headTarget.rotation, targetRot);

        if (angle < 1)
        {
            print("LookAtHand DONE!");
        }
    }
    void LookAtDrawer()
    {
        if (!drawerTarget) return;

        Vector3 dir = drawerTarget.position - headTarget.position;
        Quaternion rot = Quaternion.LookRotation(dir);
        headTarget.rotation = Quaternion.Slerp(
            headTarget.rotation,
            rot,
            Time.deltaTime * speedLookAtHand
        );
        Quaternion targetRot = Quaternion.LookRotation(dir);

        float angle = Quaternion.Angle(headTarget.rotation, targetRot);

        if (angle < 1)
        {
            
            print("LookAtDrawer DONE!");
            drawer.Move();
            stateCamera = StateCamera.None;
        }
    }
    
}
