using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI textTimer;

    public int minutes, seconds, miliseconds;

    void Start()
    {

    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Map1" || SceneManager.GetActiveScene().name == "Map2" || SceneManager.GetActiveScene().name == "Map3")
        {
            DisplayTime();
        }
    }

    void DisplayTime()
    {
        minutes = (int)(Time.timeSinceLevelLoad / 60f);
        seconds = (int)(Time.timeSinceLevelLoad % 60f);
        miliseconds = (int)(Time.timeSinceLevelLoad * 100f)%100;
        textTimer.text = minutes.ToString("D2") + ":" + seconds.ToString("D2");
    }
}
