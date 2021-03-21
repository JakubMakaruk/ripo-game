using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private string filename_u = "Assets/Scripts resources/u.txt";
    private string filename_o = "Assets/Scripts resources/ó.txt";

    public TextMeshProUGUI textObject;
    public TextMeshProUGUI wrongAnswersCounter;
    public TextMeshProUGUI correctAnswersCounter;
    public GameObject[] spawnersLetters;

    private Color yellowColor = new Color32(223, 221, 37, 255);
    private Color greenColor = new Color32(86, 229, 25, 255);
    private Color redColor = new Color32(229, 26, 30, 255);

    private int currentAnswer; // 10 - U , 11 - Ó
    private int counterWords;
    private string currentWord;
    private int randomStartValue; // 0 - words_u , 1 - words_o
    private int counterAll = 0, correctCounter = 0, wrongCounter = 0;
    private int randomValue;

    private Dictionary<string, int> words_u;
    private Dictionary<string, int> words_o;

    System.Random random = new System.Random();

    void Start()
    {
        words_u = new Dictionary<string, int>();
        words_o = new Dictionary<string, int>();

        words_u = File.ReadAllLines(filename_u).ToDictionary(x => x, y => 0);
        words_o = File.ReadAllLines(filename_o).ToDictionary(x => x, y => 0);


        foreach (KeyValuePair<string, int> item in words_u)
        {
            Debug.Log(item.Key + " " + item.Value.ToString());
        }

        correctAnswersCounter.text = "0";
        wrongAnswersCounter.text = "0";

        GenerateNewWord();
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10 || other.gameObject.layer == 11)
        {
            StartCoroutine(OnCollisionWithObstacle(other));
        }
    }

    private IEnumerator OnCollisionWithObstacle(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if ((other.gameObject.layer == 10 && currentAnswer == 10) || (other.gameObject.layer == 11 && currentAnswer == 11)) // COLLISION AND GOOD ANSWER
        {
            Debug.Log("Dobra odpowiedź!");
            Destroy(other.gameObject);

            textObject.color = greenColor;
            //textObject.text = "Dobrze!";
            textObject.SetText("Dobrze!");
            correctCounter++;
            correctAnswersCounter.text = correctCounter.ToString();

            DestroySpawnLetter(counterAll);

            SetWordAsAppeared(currentWord);
        }
        else if (other.gameObject.layer == 10 || other.gameObject.layer == 11) // COLLISION BUT WRONG ANSWER
        {
            Debug.Log("Zła odpowiedź!");
            Destroy(other.gameObject);

            textObject.color = redColor;
            //textObject.text = "Źle!";
            textObject.SetText("Źle!");
            wrongCounter++;
            wrongAnswersCounter.text = wrongCounter.ToString();

            DestroySpawnLetter(counterAll);
        }
        yield return new WaitForSeconds(2);
        GenerateNewWord();
        Debug.Log("-------------------------");
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
                randomValue = random.Next(0, words_u.Count);
                word = words_u.ElementAt(randomValue).Key;
                appear = words_u.ElementAt(randomValue).Value;
                currentAnswer = 10;
            }
            else
            {
                randomValue = random.Next(0, words_o.Count);
                word = words_o.ElementAt(randomValue).Key;
                appear = words_o.ElementAt(randomValue).Value;
                currentAnswer = 11;
            }
            Debug.Log(word + " " + appear.ToString());
        } while (appear != 0);

        textObject.SetText(word);
        currentWord = word;
        counterAll++;
        textObject.color = yellowColor;
    }

    private void DestroySpawnLetter(int index)
    {
        Destroy(spawnersLetters[index-1].gameObject);
    }

    private void SetWordAsAppeared(string word)
    {
        switch (currentAnswer)
        {
            case 10:
                Debug.Log("USTAWIAM: " + word + " " + words_u[word].ToString());
                this.words_u[word] = 1;
                Debug.Log(word + " " + words_u[word].ToString());
                break;
            case 11:
                Debug.Log("USTAWIAM: " + word + " " + words_o[word].ToString());
                this.words_o[word] = 1;
                Debug.Log(word + " " + words_o[word].ToString());
                break;
        }
    }
}