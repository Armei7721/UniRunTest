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
    public bool isAxe= false;

    public float blinkIntervals = 3f;
    public GameObject axePrefab; // 도끼의 프리팹
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
                    InstantiateAxe(warningObjects[i].transform.position);
                    timer = 0;
                }
                isActivated = false;
                isAxe = false;
            }
        }
    }
    private void InstantiateAxe(Vector3 position)
    {
        // 도끼 프리팹을 인스턴스화하여 날리기
        GameObject axe = Instantiate(axePrefab, position, Quaternion.identity);
        Rigidbody2D axeRigidbody = axe.GetComponent<Rigidbody2D>();

        // 도끼에 원하는 힘과 방향을 주어 날리기
        Vector3 axeForce = Vector3.right * 10f; // 예시로 앞쪽으로 힘을 가함
        axeRigidbody.AddForce(axeForce, ForceMode2D.Impulse);
    }
}