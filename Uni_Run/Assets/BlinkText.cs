using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlinkText : MonoBehaviour
{
    public float activationTime = 3f; // 활성화되어 있는 시간 (초)

    private bool isActivated = false; // 오브젝트가 활성화되었는지 여부
    public GameObject[] warningObjects; // 게임 오브젝트를 저장할 배열
    public float blinkInterval = 0.5f;
    public GameObject TWAxe;
    public bool isAxe;

    public float blinkIntervals = 3f;

    private float timer;
    void Start()
    { 
    }
    private void Update()
    {
        Axe();    
    }


    public void Axe()
    {   //isAxe true이고 isActivated가 false 일대
        if (isAxe && !isActivated)
        {   // randomindex에 random.Range(0,warningObjects.Length)사이값을 대입
            int randomIndex = Random.Range(0, warningObjects.Length);
            for (int i = 0; i < warningObjects.Length; i++)
            {
                if (i == randomIndex)
                {
                    warningObjects[i].SetActive(true);
                    isActivated = true;
                    timer = 0f; // 타이머 초기화
                }
                else
                {
                    warningObjects[i].SetActive(false);
                }
            }
        }

        if (isActivated)
        {
            timer += Time.deltaTime;
            if (timer >= activationTime)
            {
                for (int i = 0; i < warningObjects.Length; i++)
                {
                    warningObjects[i].SetActive(false);
                    
                    timer = 0;
                }
                isActivated = false;
                isAxe = false;
                Debug.Log("이건 뭐지");
            }
        }
    }

}