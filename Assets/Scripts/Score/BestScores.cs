using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class BestScores : MonoBehaviour
{
    public static float[] bestScores = new float[3];
    GameObject[] displayScores = new GameObject[3];

    private void Start()
    {
        if (!PlayerPrefs.HasKey("1st")) {
            PlayerPrefs.SetFloat("1st", 99);
        }
        if (!PlayerPrefs.HasKey("2nd"))
        {
            PlayerPrefs.SetFloat("2nd", 99);
        }
        if (!PlayerPrefs.HasKey("3rd"))
        {
            PlayerPrefs.SetFloat("3rd", 99);
        }

        displayScores[0] = GameObject.Find("1st");
        displayScores[1] = GameObject.Find("2nd");
        displayScores[2] = GameObject.Find("3rd");

        ranking();
    }

    void ranking()
    {
        if (GameTimer.playTime < PlayerPrefs.GetFloat("1st"))
        {
            PlayerPrefs.SetFloat("3rd", PlayerPrefs.GetFloat("2nd"));
            PlayerPrefs.SetFloat("2nd", PlayerPrefs.GetFloat("1st"));
            PlayerPrefs.SetFloat("1st", GameTimer.playTime);
        }
        else if (GameTimer.playTime < PlayerPrefs.GetFloat("2nd"))
        {
            PlayerPrefs.SetFloat("3rd", PlayerPrefs.GetFloat("2nd"));
            PlayerPrefs.SetFloat("2nd", GameTimer.playTime);
        }
        else if (GameTimer.playTime < PlayerPrefs.GetFloat("3rd"))
        {
            PlayerPrefs.SetFloat("3rd", GameTimer.playTime);
        }

        displayRanking();
    }

    void displayRanking()
    {
        displayScores[0].GetComponent<Text>().text = switchTo(PlayerPrefs.GetFloat("1st"));

        if (PlayerPrefs.GetFloat("2nd") == 99)
        {
            displayScores[1].GetComponent<Text>().text = switchTo(0);
        }
        else
        {
            displayScores[1].GetComponent<Text>().text = switchTo(PlayerPrefs.GetFloat("2nd"));
        }

        if (PlayerPrefs.GetFloat("3rd") == 99)
        {
            displayScores[2].GetComponent<Text>().text = switchTo(0);
        }
        else
        {
            displayScores[2].GetComponent<Text>().text = switchTo(PlayerPrefs.GetFloat("3rd"));
        }
    }

    String switchTo(float playTime)
    {
        if (Mathf.Round(playTime % 60) < 10)
        {
            return "0" + (Mathf.Round(playTime / 60)).ToString() + " : " + "0" + (Mathf.Round(playTime % 60)).ToString();
        }
        else
        {
            return "0" + (Mathf.Round(playTime / 60)).ToString() + " : " + (Mathf.Round(playTime % 60)).ToString();
        }
    }
}