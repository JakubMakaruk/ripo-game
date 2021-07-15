using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        switch (sceneName)
        {
            case "MainMenu":
                SceneManager.LoadScene("MainMenu");
                break;
            case "ChooseMap":
                SceneManager.LoadScene("ChooseMap");
                break;
            case "ChooseLevel":
                SceneManager.LoadScene("ChooseLevel");
                break;
            case "InfoMenu":
                SceneManager.LoadScene("InfoMenu");
                break;
            case "Map1":
            case "Map2":
            case "Map3":
                Image[] maps = new Image [3];
                maps = GetComponentsInChildren<Image>();

                for(int i=0; i< maps.Length; i++)
                {
                    if ((sceneName == "Map1" && maps[i].name == "Map1") || (sceneName == "Map2" && maps[i].name == "Map2") || (sceneName == "Map3" && maps[i].name == "Map3"))
                    {
                        maps[i].color = new Color32(255, 233, 0, 255);
                    }
                    else
                    {
                        maps[i].color = new Color32(255, 255, 255, 255);
                    }
                }
                GameManager.map = sceneName;
                break;
            case "Next":
                SceneManager.LoadScene("ChooseLevel");
                break;
            case "UorO":
            case "RZorZ":
            case "CorCI":
            case "SorSI":
            case "CHorH":
            case "ZorZI":
                TextMeshProUGUI[] levels = new TextMeshProUGUI[3];
                levels = GetComponentsInChildren<TextMeshProUGUI>();

                for(int i=0; i<levels.Length; i++)
                {
                    if((sceneName=="UorO" && levels[i].text=="U / Ó") || (sceneName == "RZorZ" && levels[i].text == "RZ / Ż") || (sceneName == "CorCI" && levels[i].text == "Ć / CI") || (sceneName == "SorSI" && levels[i].text == "Ś / SI") || (sceneName == "CHorH" && levels[i].text == "CH / H") || (sceneName == "ZorZI" && levels[i].text == "Ź / ZI"))
                    {
                        levels[i].color = new Color32(255, 233, 0, 255);
                    }
                    else
                    {
                        levels[i].color = new Color32(255, 255, 255, 255);
                    }
                }
                GameManager.level = sceneName;

                break;
            case "Play":
                SceneManager.LoadScene(GameManager.map);
                break;
            case "Exit":
                Application.Quit();
                break;
        }
    }
}
