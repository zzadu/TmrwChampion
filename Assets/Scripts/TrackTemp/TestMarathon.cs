using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMarathon : MonoBehaviour
{
    GameObject player;
    GameObject poolOutTrigger;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        poolOutTrigger = GameObject.FindGameObjectWithTag("PoolOutTrigger");
    }

    // Update is called once per frame
    void Update()
    {
        //finish cycle track
        if (Input.GetKey(KeyCode.C))
        {
            player.transform.position = new Vector3(-45.0491562f, 2.13000131f, 62.7587547f);
        }

        
    }
}
