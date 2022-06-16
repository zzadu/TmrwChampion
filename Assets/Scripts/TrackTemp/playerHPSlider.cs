using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHPSlider : MonoBehaviour
{
    public Slider HPSlider;
    public Image HPSliderFill;
    GameObject player;
    Vector3 playerPos;
    Color hpRed, hpGreen;

    // Start is called before the first frame update
    void Start()
    {
        HPSlider = GetComponent<Slider>();
        player = GameObject.FindGameObjectWithTag("Player");
        hpRed = new Color(255, 0, 0);
        hpGreen = new Color(0, 255, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (HPSlider.value == 0f)
        {
            //HP가 0이면 저장했던 마지막 위치를 player의 위치로 고정
            player.transform.position = playerPos;
        }
        else
        {
            //HP가 0이 아니면 player 위치를 계속 저장
            playerPos = player.transform.position;
        }

        //a,d 키 입력 시간만큼 체력 깎임
        if (Input.GetKey(KeyCode.A))
        {
            HPSlider.value -= 0.02f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            HPSlider.value -= 0.02f;
        }

        //임시로 q누르면 체력 증진
        if (Input.GetKey(KeyCode.Q))
        {
            // HPSlider.value += 10.0f;
        }
        //임시로 m누르면 체력 감소
        if (Input.GetKey(KeyCode.M))
        {
            // HPSlider.value -= 10.0f;
        }

        //체력 30미만이면 fill color 빨간색으로
        if (HPSlider.value <= 30.0f)
        {          
            HPSliderFill.color = hpRed;
        }
        else
        {
            HPSliderFill.color = hpGreen;
        }

    }
   
}
