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

    public TextMeshProUGUI textObject;
    public TextMeshProUGUI wrongAnswersCounter;
    public TextMeshProUGUI correctAnswersCounter;
    public GameObject[] spawnersLetters;

    public GameObject endScreen;
    public TextMeshProUGUI[] endWrongWords;
    public TextMeshProUGUI endScore;
    public TextMeshProUGUI endTimer;
    public TextMeshProUGUI endTime;

    private Color yellowColor = new Color32(223, 221, 37, 255);
    private Color greenColor = new Color32(86, 229, 25, 255);
    private Color redColor = new Color32(229, 26, 30, 255);

    private int currentAnswer; // 10 - U , 11 - Ó , 12 - RZ, 13 - Z
    private int possibleAnswer1, possibleAnswer2;
    private int counterWords;
    private string currentWord;
    private int randomStartValue; // 0 - words_1 , 1 - words_2
    private int counterAll = 0, correctCounter = 0, wrongCounter = 0;
    private int randomValue;

    private Dictionary<string, int> words_1;
    private Dictionary<string, int> words_2;
    //private List<string> wrongAsweredWords;
    //private String[] words_u;
    //private String[] words_o;

    private int counterWrongWords=0;
    private int indexWrongWords=0;
    private string[] wrong_words;

    private string CurrentScene;

    System.Random random = new System.Random();

    void Start()
    {
        endScreen.gameObject.SetActive(false);

        words_1 = new Dictionary<string, int>();
        words_2 = new Dictionary<string, int>();
        wrong_words = new string[3];
        for (int i = 0; i < 3; i++)
            wrong_words[i] = "";

        //wrongAsweredWords = new List<string>();

        CurrentScene = SceneManager.GetActiveScene().name;

        switch(CurrentScene)
        {
            case "UorO":
                words_1 = File.ReadAllLines(filename_u).ToDictionary(x => x, y => 0);
                words_2 = File.ReadAllLines(filename_o).ToDictionary(x => x, y => 0);
                possibleAnswer1 = 10;
                possibleAnswer2 = 11;
                break;
            case "RZorZ":
                words_1 = File.ReadAllLines(filename_rz).ToDictionary(x => x, y => 0);
                words_2 = File.ReadAllLines(filename_z).ToDictionary(x => x, y => 0);
                possibleAnswer1 = 12;
                possibleAnswer2 = 13;
                break;
        }

        Debug.Log(CurrentScene);
        

        //words_u = File.ReadAllLines(filename_u);
        //words_o = File.ReadAllLines(filename_o);

        correctAnswersCounter.text = "0";
        wrongAnswersCounter.text = "0";

        GenerateNewWord();
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10 || other.gameObject.layer == 11 || other.gameObject.layer == 12 || other.gameObject.layer == 13)
        {
            StartCoroutine(OnCollisionWithObstacle(other));
        }
        else if(other.gameObject.layer == 9)
        {
            EndGame();
            GameManager.Instance.InputController.DriveInput = 0f;
            GameManager.Instance.InputController.SteerInput = 0f;
            GameManager.Instance.InputController.HandbrakeInput = true;
            GameManager.Instance.InputController.canInput = false;
            //GameManager.Instance.InputController.inputDriveAxis = "";
            //GameManager.Instance.InputController.inputSteerAxis = "";
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
        else if (other.gameObject.layer == possibleAnswer1 || other.gameObject.layer == possibleAnswer2) // COLLISION BUT WRONG ANSWER
        {
            Destroy(other.gameObject);

            textObject.color = redColor;
            textObject.SetText("Źle!");
            wrongCounter++;
            wrongAnswersCounter.text = wrongCounter.ToString();

            DestroySpawnerLetter(counterAll);

            Debug.Log("PRZED: " + wrong_words[indexWrongWords]);
            wrong_words[indexWrongWords] = wrong_words[indexWrongWords] + currentWord + "\n";
            Debug.Log("PO" + wrong_words[indexWrongWords]);
            counterWrongWords++;
            if (counterWrongWords >= 4)
            {
                counterWrongWords = 0;
                indexWrongWords++;
            }
            //wrongAsweredWords.Add(currentWord);

            //Debug.Log("--------------\nLISTA:\n");
            //foreach(string i in wrongAsweredWords)
            //{
            //    Debug.Log(i);
            //}
            //Debug.Log("DŁUGOŚĆ: " + wrongAsweredWords.Count);
        }
        yield return new WaitForSeconds(2);
        GenerateNewWord();
    }

    void GenerateNewWord() // for dictionary words
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

    /*void GenerateNewWord() // for array words
    {
        System.Random random = new System.Random();
        int randomStartValue = random.Next(0, 2);
        Debug.Log("Random start " + randomStartValue.ToString());
        if (randomStartValue == 0)
        {
            int randomValue = random.Next(0, words_u.Length);
            textObject.text = currentWord = words_u[randomValue];
            currentAnswer = 10;
        }
        else
        {
            int randomValue = random.Next(0, words_o.Length);
            textObject.text = currentWord = words_o[randomValue];
            currentAnswer = 11;
        }
        counterAll++;
        textObject.color = yellowColor;
    }*/

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

        for (int i = 0; i < 3; i++)
            endWrongWords[i].SetText(wrong_words[i]);

        /*for (int x = 0; x < endWrongWords.Length; x++)
        {
            string s = "";
            int rows = 0;
            while (wrongAsweredWords.Count > 0)
            {
                s = s + wrongAsweredWords.First() + "\n";
                wrongAsweredWords.RemoveAt(0);
                rows++;
                if (rows >= 4)
                    break;
            }
            endWrongWords[x].SetText(s);
        }*/
    }
}