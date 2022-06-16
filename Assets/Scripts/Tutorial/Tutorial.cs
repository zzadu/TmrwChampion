using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    GameObject[] TutorialItems = new GameObject[6];
    int cnt;

    void Start()
    {
        TutorialItems[0] = GameObject.Find("PlayButtonTutorial");
        TutorialItems[1] = GameObject.Find("HPTutorial");
        TutorialItems[2] = GameObject.Find("TimeTutorial");
        TutorialItems[3] = GameObject.Find("StopButtonTutorial");
        TutorialItems[4] = GameObject.Find("ItemTutorial");
        TutorialItems[5] = GameObject.Find("ItemTutorial2");

        for (int i = 1; i < TutorialItems.Length; i++)
        {
            TutorialItems[i].SetActive(false);
        }

        cnt = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && cnt <= 4)
        {
            TutorialItems[cnt].SetActive(false);
            TutorialItems[++cnt].SetActive(true);
        }
        else if (Input.GetMouseButtonDown(0) && cnt == 5)
        {
            SceneManager.LoadScene("TrackTemp1");
        }
    }
}
