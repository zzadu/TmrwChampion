using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSound : MonoBehaviour
{
    AudioSource click;

    void Start()
    {
        click = GameObject.Find("ClickSound").GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        click.Play();
    }
}
