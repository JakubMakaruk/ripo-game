using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public InputController InputController { get; set; }
    public static string map { get; set; }
    public static string level { get; set; }

    void Awake()
    {
        Instance = this;
        InputController = GetComponentInChildren<InputController>();
    }

    void Update()
    {
        
    }
}
