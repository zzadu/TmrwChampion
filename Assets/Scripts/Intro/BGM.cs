using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    GameObject BGMusic;
    AudioSource bgm;

    void Start()
    {
        BGMusic = GameObject.Find("BGM");
        bgm = BGMusic.GetComponent<AudioSource>();

        if (!bgm.isPlaying)
        {
            bgm.Play();
        }
        DontDestroyOnLoad(BGMusic);
    }

    public void BGMOn()
    {
        BGMusic = GameObject.Find("BGM");
        bgm = BGMusic.GetComponent<AudioSource>();

        if (!bgm.isPlaying)
        {
            bgm.Play();
        }
    }

    public void BGMOff()
    {
        BGMusic = GameObject.Find("BGM");
        bgm = BGMusic.GetComponent<AudioSource>();

        bgm.Pause();
    }
}
