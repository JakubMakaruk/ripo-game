using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Car : MonoBehaviour
{
    public Transform centerOfMass;
    public float motorTorque = 100.0f;
    public float maxSteer = 20.0f;
    public float handbrakeValue = 600.0f;

    public float Steer { get; set; }
    public float Drive { get; set; }
    public bool Handbrake { get; set; }


    private Rigidbody _rigidbody;
    private Wheel[] wheels;
    

    private void Start()
    {
        wheels = GetComponentsInChildren<Wheel>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = centerOfMass.localPosition;
    }

    private void Update()
    {
        Steer = GameManager.Instance.InputController.SteerInput;
        Drive = GameManager.Instance.InputController.DriveInput;
        Handbrake = GameManager.Instance.InputController.HandbrakeInput;
        
        foreach (var wheel in wheels)
        {
            wheel.SteerAngle = Steer * maxSteer;
            wheel.Torque = Drive * motorTorque;
            wheel.Handbrake = handbrakeValue * (Handbrake ? 1.0f : 0.0f);
        }
    }
}
