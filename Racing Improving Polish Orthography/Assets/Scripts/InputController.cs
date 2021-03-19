using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public string inputSteerAxis = "Horizontal";
    public string inputDriveAxis = "Vertical";
    public string inputHandbrake = "space";

    public float SteerInput { get; private set; }
    public float DriveInput { get; private set; }
    public bool HandbrakeInput { get; private set; }

    void Start()
    {
        
    }

    void Update()
    {
        SteerInput = Input.GetAxis(inputSteerAxis);
        DriveInput = Input.GetAxis(inputDriveAxis);
        HandbrakeInput = Input.GetKey(inputHandbrake);
    }
}
