using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI textTimer;

    private int minutes, seconds, miliseconds;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DisplayTime();
    }

    void DisplayTime()
    {
        minutes = (int)(Time.time / 60f);
        seconds = (int)(Time.time % 60f);
        miliseconds = (int)(Time.time * 100f)%100;
        //miliseconds = (int)(Time.time / 3600000.0f);
        textTimer.text = minutes.ToString("D2") + ":" + seconds.ToString("D2");//string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, miliseconds);
        //Debug.Log(minutes.ToString() + ":" + seconds.ToString() + ":" + miliseconds.ToString());
    }
}
