using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    /*Vector3[] positionArrayBurger = new[] { new Vector3(58f, 1f, 40f),
                                      new Vector3(52f, 4f, 67f),
                                      new Vector3(-1f, 1f, 64f),
                                      new Vector3(-34f, 1f, 60f),
                                      new Vector3(10f, 2f, 31f),
                                      new Vector3(-10f, 2f, 31f),
                                      new Vector3(-20f, 2f, 16f),
                                      new Vector3(-30f, 2f, 19f),
                                      new Vector3(-40f, 2f, 46f),
                                      new Vector3(18f, 1f, 36f) };
    public GameObject hamburgerPrefab;
    public GameObject hamburgerContainer;

    Vector3[] positionArrayCoin = new[] { new Vector3(61f, 3f, 21f),
                                      new Vector3(59f, 4f, 61f),
                                      new Vector3(44f, 4f, 53f),
                                      new Vector3(34f, 3f, 22f),
                                      new Vector3(18f, 4f, 36f) };
    public GameObject coinPrefab;
    public GameObject coinContainer;*/

    Vector3[] positionArrayCycle = new[] { new Vector3(58f, 4f, 18f),
                                      new Vector3(60f, 4f, 38f),
                                      new Vector3(53f, 6f, 65f),
                                      new Vector3(39f, 4f, 57f),
                                      new Vector3(43f, 4f, 36f),
                                      new Vector3(11f, 4f, 54f)
                                      };

    Vector3[] positionArrayMarathon = new[] { new Vector3(-34,2,-60),
                                      new Vector3(-30,9,-52),
                                      new Vector3(-30,9,-37),
                                      new Vector3(-4,6,-56),
                                      new Vector3(28,2,-62),
                                      new Vector3(49,2,-49)
                                      };

    public GameObject randomboxPrefab;
    public GameObject randomboxContainer;

    // Start is called before the first frame update
    void Start()
    {
        /*//햄버거 생성
        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, 5);
            Instantiate(hamburgerPrefab, positionArrayBurger[randomIndex], Quaternion.identity, hamburgerContainer.transform);
        }

        //코인 생성
        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, 5);
            Instantiate(coinPrefab, positionArrayCoin[randomIndex], Quaternion.Euler(90, -90, 90), coinContainer.transform);
        }*/

        //랜덤박스 생성 cycle
        for (int i = 0; i < 4; i++)
        {
            int randomIndex = Random.Range(0, 5);
            Instantiate(randomboxPrefab, positionArrayCycle[randomIndex], Quaternion.identity, randomboxContainer.transform);
        }
        
        //랜덤박스 생성 marathon
        for (int i = 0; i < 4; i++)
        {
            int randomIndex = Random.Range(0, 5);
            Instantiate(randomboxPrefab, positionArrayMarathon[randomIndex], Quaternion.Euler(0, 90, 0), randomboxContainer.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
