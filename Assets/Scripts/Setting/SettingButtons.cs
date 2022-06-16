using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingButtons : MonoBehaviour
{
    GameObject AlertBoard;

    private void Start()
    {
        AlertBoard = GameObject.Find("Alert");
        AlertBoard.SetActive(false);
    }

    public void Alert()
    {
        AlertBoard.SetActive(true);
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        AlertBoard.SetActive(false);
    }
}
