using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class item : MonoBehaviour
{
    public Sprite transparent;

    // 현재 아이템 담는 object
    public static Image[] currentItems = new Image[3];

    // 아이템 이미지 모음
    public Sprite hamburgerItemSprite;
    public Sprite wingItemSprite;
    public Sprite snailItemSprite;

    public static Sprite[] itemSprites = new Sprite[3];

    void Start()
    {
        itemSprites[0] = hamburgerItemSprite;
        itemSprites[1] = wingItemSprite;
        itemSprites[2] = snailItemSprite;

        // 아이템 보관함
        currentItems[0] = GameObject.Find("itemSprite").GetComponent<Image>();
        currentItems[1] = GameObject.Find("itemSprite1").GetComponent<Image>();
        currentItems[2] = GameObject.Find("itemSprite2").GetComponent<Image>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (currentItems[2].sprite == transparent)
            {
                itemUse.getItem();
            }
            gameObject.SetActive(false);
        }
    }
}
