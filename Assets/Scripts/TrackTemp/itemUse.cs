using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class itemUse : MonoBehaviour
{
    Slider HPSlider;
    public Sprite transparent;
    public Sprite hamburgerItemSprite;
    public Sprite wingItemSprite;
    public Sprite snailItemSprite;
    GameObject player;
    FirstPersonController fpsScript;
    public PlayerCycle cycleScript;
    public PlayerSwimming swimScript;

    private float limitTime;
    private bool isUsingSpeedItem;

    // Start is called before the first frame update
    void Start()
    {
        HPSlider = GameObject.FindGameObjectWithTag("HPSlider").GetComponent<Slider>();
        player = GameObject.FindGameObjectWithTag("Player");
        fpsScript = player.GetComponent<FirstPersonController>();
        cycleScript = player.GetComponent<PlayerCycle>();
        swimScript = player.GetComponent<PlayerSwimming>();

        limitTime = 0;
        isUsingSpeedItem = false;
    }

    // Update is called once per frame
    void Update()
    {

        //아이템 사용
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (item.currentItems[0].sprite == hamburgerItemSprite)
            {
                HPSlider.value += 30.0f;
            }
            else if (item.currentItems[0].sprite == wingItemSprite)
            {
                Debug.Log("speed up");
                limitTime += 10f;
                fpsScript.m_WalkSpeed += 3.0f;
                cycleScript.m_cycleSpeed += 3.0f;
                swimScript.m_SwimSpeed += 3.0f;
                isUsingSpeedItem = true;
            }
            else if (item.currentItems[0].sprite == snailItemSprite)
            {
                Debug.Log("speed down");
                limitTime += 10f;
                fpsScript.m_WalkSpeed -= 3.0f;
                cycleScript.m_cycleSpeed -= 3.0f;
                swimScript.m_SwimSpeed -= 3.0f;
                isUsingSpeedItem = true;
            }

            for (int i = 0; i < 2; i++)
            {
                item.currentItems[i].sprite = item.currentItems[i + 1].sprite;
            }
            item.currentItems[2].sprite = transparent;
        }

        // 속력 아이템 시간 제한
        if (isUsingSpeedItem)
        {
            limitTime -= Time.deltaTime;

            if (limitTime <= 0)
            {
                print("Time Out");
                fpsScript.m_WalkSpeed = 5f;
                cycleScript.m_cycleSpeed = 13f;
                swimScript.m_SwimSpeed = 3f;
                isUsingSpeedItem = false;
            }
        }
    }

    public static void getItem()
    {
        //int random = (int)Random.Range(0.0f, 0.0f); // 아이템 추가 시 변경
        int random = Random.Range(0, 3);

        for (int i = 0; i < 3; i++)
        {
            if (item.currentItems[i].sprite.name == "transparent")
            {
                item.currentItems[i].sprite = item.itemSprites[random];
                break;
            }
        }
    }
}
