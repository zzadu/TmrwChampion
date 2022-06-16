using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class HPTrigger : MonoBehaviour
{
    Slider HPSlider;
    Image currentItem;
    public Sprite emptyItemSprite;
    public Sprite hamburgerItemSprite;
    public GameObject explosionPrefab;
    float explosionDur = 1.0f;
    
    public AudioClip se_hamburger;
    public AudioClip se_bomb;

    // Start is called before the first frame update
    void Start()
    {
        HPSlider = GameObject.FindGameObjectWithTag("HPSlider").GetComponent<Slider>();
        // currentItem = GameObject.FindGameObjectWithTag("currentItem").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.CompareTag("Hamburger") && other.CompareTag("Player"))
        {
            Debug.Log("Hamburger trigger!");
            AudioSource.PlayClipAtPoint(se_hamburger, this.transform.position);
            //HPSlider.value += 30.0f;
            // currentItem.sprite = hamburgerItemSprite;
            // Destroy(this.gameObject);
        }
    }

    
}
