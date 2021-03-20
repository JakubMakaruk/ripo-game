using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

    private int currentAnswer;
    private int counterWords;
    private string currentWord;
    private int randomStartValue; // 0 - words_u , 1 - words_o
    private int counterAll = 0, correctCounter = 0, wrongCounter = 0;

    private string[] words_u;
    private string[] words_o;
    private List<string> words_appeared;

    void Start()
    {
        words_appeared = new List<string>();

        words_u = File.ReadAllLines(filename_u);
        words_o = File.ReadAllLines(filename_o);
        for (int i = 0; i < words_u.Length; i++)
        {
            Debug.Log(words_u[i]);
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
        if ((other.gameObject.layer == 10 && currentAnswer == 10) || (other.gameObject.layer == 11 && currentAnswer == 11))
        {
            Debug.Log("Dobra odpowiedź!");
            Destroy(other.gameObject);

            textObject.color = greenColor;
            textObject.text = "Dobrze!";
            correctCounter++;
            correctAnswersCounter.text = correctCounter.ToString();

            DestroySpawnLetter(counterAll);
            words_appeared.Add(currentWord); // add current word to list to not repeat if answered correctly

            yield return new WaitForSeconds(2);
            GenerateNewWord();
        }
        else if (other.gameObject.layer == 10 || other.gameObject.layer == 11)
        {
            Debug.Log("Zła odpowiedź!");
            Destroy(other.gameObject);

            textObject.color = redColor;
            textObject.text = "Źle!";
            wrongCounter++;
            wrongAnswersCounter.text = wrongCounter.ToString();

            DestroySpawnLetter(counterAll);

            yield return new WaitForSeconds(2);
            GenerateNewWord();
        }
    }

    private void GenerateNewWord()
    {
        do
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
            
        } while (IfWordAppeared(currentWord));
        counterAll++;
        textObject.color = yellowColor;
    }

    private void DestroySpawnLetter(int index)
    {
        Debug.Log("SIEMA +" + index.ToString());
        Destroy(spawnersLetters[index-1].gameObject);
    }

    private bool IfWordAppeared(string word)
    {
        foreach(string it in words_appeared)
        {
            Debug.Log("AAAAA: " + it);
        }
        Debug.Log(words_appeared.Contains(word));
        return words_appeared.Contains(word);
    }
}