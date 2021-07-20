using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Script for generate words depends on Scene Name U-Ó / RZ-Z
    private string filename_u = "Assets/Scripts resources/u.txt";
    private string filename_o = "Assets/Scripts resources/ó.txt";
    private string filename_rz = "Assets/Scripts resources/rz.txt";
    private string filename_z = "Assets/Scripts resources/ż.txt";
    private string filename_ci = "Assets/Scripts resources/ci.txt";
    private string filename_c = "Assets/Scripts resources/ć.txt";
    private string filename_s = "Assets/Scripts resources/ś.txt";
    private string filename_si = "Assets/Scripts resources/si.txt";
    private string filename_x = "Assets/Scripts resources/ź.txt";
    private string filename_zi = "Assets/Scripts resources/zi.txt";
    private string filename_ch = "Assets/Scripts resources/ch.txt";
    private string filename_h = "Assets/Scripts resources/h.txt";

    public TextMeshProUGUI textObject;
    public TextMeshProUGUI wrongAnswersCounter;
    public TextMeshProUGUI correctAnswersCounter;
    public GameObject[] spawnersLetters;

    public GameObject[] textLetters;

    public GameObject endScreen;
    public TextMeshProUGUI[] endWrongWords;
    public TextMeshProUGUI endScore;
    public TextMeshProUGUI endTimer;
    public TextMeshProUGUI endTime;

    private Color yellowColor = new Color32(223, 221, 37, 255);
    private Color greenColor = new Color32(86, 229, 25, 255);
    private Color redColor = new Color32(229, 26, 30, 255);

    private int minNumLayer = 10;
    private int maxNumLayer = 21;

    private int currentAnswer; // 10 - U , 11 - Ó , 12 - RZ, 13 - Z, 14 - CI, 15 - Ć, 16 - Ś, 17 - SI, 18 - CH, 19 - H, 20 - Ź, 21 - ZI
    private int possibleAnswer1, possibleAnswer2;
    private string currentWord;
    private int randomStartValue; // 0 - words_1 , 1 - words_2
    private int counterAll = 0, correctCounter = 0, wrongCounter = 0;
    private int randomValue;

    private Dictionary<string, int> words_1;
    private Dictionary<string, int> words_2;

    private int counterWrongWords=0;
    private int indexWrongWords=0;
    private string[] wrong_words;
    private int columns_wrong_words = 3;

    System.Random random = new System.Random();

    void Start()
    {
        endScreen.gameObject.SetActive(false);

        words_1 = words_2 = new Dictionary<string, int>();
        wrong_words = new string[columns_wrong_words];
        for (int i = 0; i < columns_wrong_words; i++)
            wrong_words[i] = "";


        switch(GameManager.level)
        {
            case "UorO":
                words_1 = File.ReadAllLines(filename_u).ToDictionary(x => x, y => 0);
                words_2 = File.ReadAllLines(filename_o).ToDictionary(x => x, y => 0);
                possibleAnswer1 = 10;
                possibleAnswer2 = 11;

                GenerateSpawners(possibleAnswer1, possibleAnswer2, 0, 1);

                break;
            case "RZorZ":
                words_1 = File.ReadAllLines(filename_rz).ToDictionary(x => x, y => 0);
                words_2 = File.ReadAllLines(filename_z).ToDictionary(x => x, y => 0);
                possibleAnswer1 = 12;
                possibleAnswer2 = 13;

                GenerateSpawners(possibleAnswer1, possibleAnswer2, 2, 3);

                break;
            case "CorCI":
                words_1 = File.ReadAllLines(filename_c).ToDictionary(x => x, y => 0);
                words_2 = File.ReadAllLines(filename_ci).ToDictionary(x => x, y => 0);
                possibleAnswer1 = 14;
                possibleAnswer2 = 15;

                GenerateSpawners(possibleAnswer1, possibleAnswer2, 4, 5);

                break;
            case "SorSI":
                words_1 = File.ReadAllLines(filename_s).ToDictionary(x => x, y => 0);
                words_2 = File.ReadAllLines(filename_si).ToDictionary(x => x, y => 0);
                possibleAnswer1 = 16;
                possibleAnswer2 = 17;

                GenerateSpawners(possibleAnswer1, possibleAnswer2, 6, 7);
                break;
            case "CHorH":
                words_1 = File.ReadAllLines(filename_ch).ToDictionary(x => x, y => 0);
                words_2 = File.ReadAllLines(filename_h).ToDictionary(x => x, y => 0);
                possibleAnswer1 = 18;
                possibleAnswer2 = 19;

                GenerateSpawners(possibleAnswer1, possibleAnswer2, 8, 9);
                break;
            case "ZorZI":
                words_1 = File.ReadAllLines(filename_x).ToDictionary(x => x, y => 0);
                words_2 = File.ReadAllLines(filename_zi).ToDictionary(x => x, y => 0);
                possibleAnswer1 = 20;
                possibleAnswer2 = 21;

                GenerateSpawners(possibleAnswer1, possibleAnswer2, 10, 11);
                break;
        }
        
        correctAnswersCounter.text = "0";
        wrongAnswersCounter.text = "0";

        GenerateNewWord();
    }

    void Update()
    {

    }

    private void GenerateSpawners(int layer1, int layer2, int letter1, int letter2)
    {
        for(int i=0; i<spawnersLetters.Length; i++)
        {
            GameObject ans1 = Instantiate(textLetters[letter1], 
                spawnersLetters[i].gameObject.transform.Find("First").transform.position, 
                spawnersLetters[i].gameObject.transform.Find("First").transform.rotation,
                spawnersLetters[i].transform);
            ans1.transform.localScale = spawnersLetters[i].gameObject.transform.Find("First").transform.localScale;
            spawnersLetters[i].gameObject.transform.Find("First").gameObject.layer = layer1;

            GameObject ans2 = Instantiate(textLetters[letter2],
                spawnersLetters[i].gameObject.transform.Find("Second").transform.position,
                spawnersLetters[i].gameObject.transform.Find("Second").transform.rotation,
                spawnersLetters[i].transform);
            ans2.transform.localScale = spawnersLetters[i].gameObject.transform.Find("Second").transform.localScale;
            spawnersLetters[i].gameObject.transform.Find("Second").gameObject.layer = layer2;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer >= minNumLayer && other.gameObject.layer <= maxNumLayer) // letter
        {
            StartCoroutine(OnCollisionWithObstacle(other));
        }
        else if(other.gameObject.layer == 9) // finish
        {
            EndGame();
            GameManager.Instance.InputController.DriveInput = GameManager.Instance.InputController.SteerInput = 0f;
            GameManager.Instance.InputController.HandbrakeInput = true;
            GameManager.Instance.InputController.canInput = false;
        }
    }

    private IEnumerator OnCollisionWithObstacle(Collider other)
    {
        if ((other.gameObject.layer == possibleAnswer1 && currentAnswer == possibleAnswer1) || (other.gameObject.layer == possibleAnswer2 && currentAnswer == possibleAnswer2)) // COLLISION AND GOOD ANSWER
        {
            Destroy(other.gameObject);

            textObject.color = greenColor;
            textObject.SetText("Dobrze!");
            correctCounter++;
            correctAnswersCounter.text = correctCounter.ToString();

            DestroySpawnerLetter(counterAll);

            SetWordAsAppeared(currentWord);
        }
        else if (other.gameObject.layer == possibleAnswer1 || other.gameObject.layer == possibleAnswer2) // COLLISION AND WRONG ANSWER
        {
            Destroy(other.gameObject);

            textObject.color = redColor;
            textObject.SetText("Źle!");
            wrongCounter++;
            wrongAnswersCounter.text = wrongCounter.ToString();

            DestroySpawnerLetter(counterAll);

            wrong_words[indexWrongWords] = wrong_words[indexWrongWords] + currentWord + "\n";
            counterWrongWords++;
            if (counterWrongWords >= 4)
            {
                counterWrongWords = 0;
                indexWrongWords++;
            }
        }
        yield return new WaitForSeconds(2);
        GenerateNewWord();
    }

    void GenerateNewWord()
    {
        int appear = -1;
        string word = "";
        do
        {
            int randomStartValue = random.Next(0, 2);
            if (randomStartValue == 0)
            {
                randomValue = random.Next(0, words_1.Count);
                word = words_1.ElementAt(randomValue).Key;
                appear = words_1.ElementAt(randomValue).Value;
                currentAnswer = possibleAnswer1;
            }
            else
            {
                randomValue = random.Next(0, words_2.Count);
                word = words_2.ElementAt(randomValue).Key;
                appear = words_2.ElementAt(randomValue).Value;
                currentAnswer = possibleAnswer2;
            }
        } while (appear != 0);

        textObject.color = yellowColor;
        textObject.SetText(word);
        currentWord = word;
        counterAll++;
    }

    private void DestroySpawnerLetter(int index)
    {
        Destroy(spawnersLetters[index-1].gameObject);
    }

    private void SetWordAsAppeared(string word)
    {
        if(currentAnswer == possibleAnswer1)
        {
            words_1[word] = 1;
        }
        else if(currentAnswer == possibleAnswer2)
        {
            words_2[word] = 1;
        }
    }

    private void EndGame()
    {
        textObject.gameObject.SetActive(false);
        wrongAnswersCounter.gameObject.SetActive(false);
        correctAnswersCounter.gameObject.SetActive(false);
        endTimer.gameObject.SetActive(false);


        endScreen.gameObject.SetActive(true);

        string score = correctCounter.ToString() + "/" + (correctCounter + wrongCounter).ToString();
        endScore.SetText(score);

        endTime.SetText(endTimer.text);

        for (int i = 0; i < columns_wrong_words; i++)
            endWrongWords[i].SetText(wrong_words[i]);
    }
}