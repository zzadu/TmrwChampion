using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class convexOut : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("convex trigger");
            GameObject.FindWithTag("Player").transform.position = this.transform.position + new Vector3(1, 0, 0);
        }
    }
}
