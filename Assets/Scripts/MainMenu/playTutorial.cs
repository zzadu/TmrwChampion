using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playTutorial : MonoBehaviour
{
    private void Start()
    {
        if (!PlayerPrefs.HasKey("goTutorial"))
        {
            PlayerPrefs.SetInt("goTutorial", 1);
        }
    }

    public void changeScene()
    {
        if (PlayerPrefs.GetInt("goTutorial") == 1)
        {
            SceneManager.LoadScene("Tutorial");
            PlayerPrefs.SetInt("goTutorial", 0);
        }
        else
        {
            SceneManager.LoadScene("TrackTemp1");
        }
    }
}
