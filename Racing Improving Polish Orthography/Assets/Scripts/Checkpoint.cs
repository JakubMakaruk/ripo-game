using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject car;

    private Transform spawner;

    void Start()
    {
        spawner = this.gameObject.transform.GetChild(0);
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            car.transform.position = spawner.position;
            car.transform.rotation = spawner.rotation;
            car.GetComponent<Rigidbody>().velocity = Vector3.zero;
            car.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }
}
