using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public string inputSteerAxis = "Horizontal";
    public string inputDriveAxis = "Vertical";
    public string inputHandbrake = "space";

    public bool canInput = true;

    public float SteerInput { get; set; }
    public float DriveInput { get; set; }
    public bool HandbrakeInput { get; set; }

    void Start()
    {
        
    }

    void Update()
    {
        if(canInput)
        {
            SteerInput = Input.GetAxis(inputSteerAxis);
            DriveInput = Input.GetAxis(inputDriveAxis);
            HandbrakeInput = Input.GetKey(inputHandbrake);
        }
    }
}


