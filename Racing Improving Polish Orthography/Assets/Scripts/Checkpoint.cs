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

        Debug.Log("EEEEEESSSA: " + spawner.position.x + " " + spawner.position.y + " " + spawner.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            car.transform.position = spawner.position;
            car.transform.rotation = spawner.rotation;
           

            //Vector3 checkpoint_position = new Vector3(spawner.position.x, spawner.position.y, spawner.position.z);
            //Debug.Log("EEEEEESSSA: " + other.transform.position.x + " " + other.transform.position.y + " " + other.transform.position.z + "\n" + other.name);
            /*other.gameObject.transform.position = spawner.position;
            other.gameObject.transform.rotation = spawner.rotation;
            Debug.Log("EEEEEESSSA: " + other.transform.position.x + " " + other.transform.position.y + " " + other.transform.position.z);*/
        }
    }
}
