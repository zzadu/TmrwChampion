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
            //HP�� 0�̸� �����ߴ� ������ ��ġ�� player�� ��ġ�� ����
            player.transform.position = playerPos;
        }
        else
        {
            //HP�� 0�� �ƴϸ� player ��ġ�� ��� ����
            playerPos = player.transform.position;
        }

        //a,d Ű �Է� �ð���ŭ ü�� ����
        if (Input.GetKey(KeyCode.A))
        {
            HPSlider.value -= 0.02f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            HPSlider.value -= 0.02f;
        }

        //�ӽ÷� q������ ü�� ����
        if (Input.GetKey(KeyCode.Q))
        {
            // HPSlider.value += 10.0f;
        }
        //�ӽ÷� m������ ü�� ����
        if (Input.GetKey(KeyCode.M))
        {
            // HPSlider.value -= 10.0f;
        }

        //ü�� 30�̸��̸� fill color ����������
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
