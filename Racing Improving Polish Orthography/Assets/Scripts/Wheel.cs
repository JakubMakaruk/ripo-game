using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Wheel : MonoBehaviour
{
    public bool steer;
    public bool power;
    public bool handbrake;

    public float SteerAngle { get; set; }
    public float Torque { get; set; }
    public float Handbrake { get; set; }

    private WheelCollider wheelCollider;
    private Transform wheelTransform;

    void Start()
    {
        wheelCollider = GetComponentInChildren<WheelCollider>();
        wheelTransform = GetComponentInChildren<MeshRenderer>().GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        wheelCollider.GetWorldPose(out Vector3 pos, out Quaternion rot);
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
        if (steer)
            wheelCollider.steerAngle = SteerAngle;
        if (power)
            wheelCollider.motorTorque = Torque;
        if (handbrake)
            wheelCollider.brakeTorque = Handbrake;
    }
}
