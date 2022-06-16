using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class StopButton : MonoBehaviour
{
    public GameObject StopBoard;
    // private FirstPersonController FPSController;
    private GameObject FPSController;

    void Start()
    {
        StopBoard.SetActive(false);
        FPSController = GameObject.Find("FPSController");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PressStopButton();
        }
    }

    public void PressStopButton()
    {
        StopBoard.SetActive(true);
        FPSController.GetComponent<FirstPersonController>().enabled = false;
        FPSController.GetComponent<PlayerCycle>().enabled = false;
        FPSController.GetComponent<PlayerSwimming>().enabled = false;
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Retry()
    {
        Time.timeScale = 1;
    }

    public void Resume()
    {
        StopBoard.SetActive(false);
        if (UnityStandardAssets.Characters.FirstPerson.PlayMode.isCycle)
        {
            FPSController.GetComponent<PlayerCycle>().enabled = true;
        }
        else if (UnityStandardAssets.Characters.FirstPerson.PlayMode.isSwim)
        {
            FPSController.GetComponent<PlayerSwimming>().enabled = true;
        }
        else if (UnityStandardAssets.Characters.FirstPerson.PlayMode.isWalk)
        {
            FPSController.GetComponent<FirstPersonController>().enabled = true;
        }

        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}