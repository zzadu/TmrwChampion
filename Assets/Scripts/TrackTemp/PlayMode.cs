using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityStandardAssets.Characters.FirstPerson
{
    public class PlayMode : MonoBehaviour
    {
        public GameObject bike;
        public static bool isCycle = false;
        public static bool isSwim = false;
        public static bool isWalk = false;
        public GameObject SwimEnding;

        private void Start()
        {
            isCycle = true;
            SwimEnding.SetActive(false);
            // 위치 초기화
            gameObject.transform.position =  new Vector3(59.8f, 2.4f, 5.26f);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.name == "CycleFinish")
            {
                bike.SetActive(false);
                GetComponent<PlayerCycle>().enabled = false;
                gameObject.GetComponent<FirstPersonController>().enabled = true;

                // bike의 CapsuleCollider 다름, swim & marathon에서는 초기화해야함.
                GetComponent<CapsuleCollider>().center = Vector3.zero;
                GetComponent<CapsuleCollider>().radius = 1;
                GetComponent<CapsuleCollider>().height = 1;

                isCycle = false;
                isWalk = true;
            }

            if (other.name == "Trigger_SwimModeIn")
            {
                Debug.Log("SwimMode");
                gameObject.GetComponent<FirstPersonController>().enabled = false;
                gameObject.GetComponent<PlayerSwimming>().enabled = true;
                GameObject.FindWithTag("MainCamera").GetComponent<Transform>().localPosition = new Vector3(0, 2.3f, 0);

                isWalk = false;
                isSwim = true;
            }

            if (other.name == "Trigger_PoolOut")        // Trigger_SwimModeOut or Trigger_AnimationEnd
            {
                SwimEnding.SetActive(true);
                Debug.Log("Trigger_Poolout");
                // 객체 파일 위치 (계층 구조 이용)
                transform.parent = null;

                //GameObject.FindWithTag("SwimHelper").SetActive(false);
                if (GameObject.FindWithTag("SwimHelper") != null)
                {
                    GameObject.FindWithTag("SwimHelper").SetActive(false);
                }

                transform.parent = other.transform.parent;
                gameObject.transform.localPosition = Vector3.zero;

                GameObject.FindWithTag("MainCamera").GetComponent<Transform>().localPosition = new Vector3(0, 2f, 0); 

                gameObject.GetComponent<PlayerSwimming>().enabled = false;
                gameObject.GetComponent<FirstPersonController>().enabled = true;

                isSwim = false;
                isWalk = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.name == "Trigger_PoolOut")
            {
                SwimEnding.SetActive(false);
                this.transform.parent = null;
                this.transform.position = new Vector3(-48.2700005f, 3.55999994f, -49.1800003f);
                GameObject.FindWithTag("MainCamera").GetComponent<Transform>().localPosition = new Vector3(0, 2f, 0);
                gameObject.GetComponent<FirstPersonController>().enabled = true;
            }
        }
    }
}



