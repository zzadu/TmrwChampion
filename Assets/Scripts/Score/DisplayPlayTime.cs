using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPlayTime : MonoBehaviour
{
    string str_playTime;
    public Text text_playTime;
    GameObject bestScoreBoard;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        str_playTime = PlayerPrefs.GetString("playTime");
        text_playTime.text = str_playTime;

        // Best Socre Ç¥½Ã
        if (GameObject.Find("BestScore") != null)
        {
            bestScoreBoard = GameObject.Find("BestScore");
            if (GameTimer.isBest == true)
            {
                bestScoreBoard.SetActive(true);
            }
            else
            {
                bestScoreBoard.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
