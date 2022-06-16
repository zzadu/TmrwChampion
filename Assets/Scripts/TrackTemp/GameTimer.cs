using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    private bool isFinished = false;
    public static float playTime = 0;
    string str_playTime;
    public Text text_playTime;
    static public bool isBest;

    Slider HPSlider;

    private void Start()
    {
        playTime = 0;
        isBest = false;
        HPSlider = GameObject.FindGameObjectWithTag("HPSlider").GetComponent<Slider>();
    }

    void Update()
    {
        if (!isFinished)
        {
            playTime += Time.deltaTime;
        }


        //00:00 format으로 맞춤
        if (Mathf.Round(playTime % 60) < 10)
        {
            str_playTime = "0" + ((int)(playTime / 60)).ToString() + " : " + "0" + (Mathf.Round(playTime % 60)).ToString();
        }
        else
        {
            str_playTime = "0" + ((int)(playTime / 60)).ToString() + " : " + (Mathf.Round(playTime % 60)).ToString();
        }

        text_playTime.text = str_playTime;

        if (HPSlider.value == 0)
        {
            isFinished = true;
            PlayerPrefs.SetString("playTime", str_playTime);
            SceneManager.LoadScene("Gameover");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FinishLine"))
        {
            // Finish에 닿았을 때만 저장
            PlayerPrefs.SetString("playTime", str_playTime);

            Debug.Log("Finish line trigger entered!");
            isFinished = true;

            // Best Score 저장
            if (PlayerPrefs.HasKey("bestScore"))
            {
                if (playTime < PlayerPrefs.GetFloat("bestScore"))
                {
                    PlayerPrefs.SetFloat("bestScore", playTime);
                    isBest = true;
                }
            }
            else
            {
                PlayerPrefs.SetFloat("bestScore", playTime);
                isBest = true;
            }

            SceneManager.LoadScene("Score");
        }
    }
}
