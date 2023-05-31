using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlinkText : MonoBehaviour
{
    public float activationTime = 3f; // Ȱ��ȭ�Ǿ� �ִ� �ð� (��)

    private bool isActivated = false; // ������Ʈ�� Ȱ��ȭ�Ǿ����� ����
    public GameObject[] warningObjects; // ���� ������Ʈ�� ������ �迭
    public float blinkInterval = 0.5f;
    public GameObject TWAxe;
    public bool isAxe= false;

    public float blinkIntervals = 3f;
    public GameObject axePrefab; // ������ ������
    private float timer;
    void Start()
    { 
    }
    private void Update()
    {
        Axe();    
    }


    public void Axe()
    {   //isAxe true�̰� isActivated�� false �ϴ�
        if (isAxe && !isActivated)
        {   // randomindex�� random.Range(0,warningObjects.Length)���̰��� ����
            int randomIndex = Random.Range(0, warningObjects.Length);
            for (int i = 0; i < warningObjects.Length; i++)
            {
                if (i == randomIndex)
                {
                    warningObjects[i].SetActive(true);
                    isActivated = true;
                    
                    timer = 0f; // Ÿ�̸� �ʱ�ȭ
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
        // ���� �������� �ν��Ͻ�ȭ�Ͽ� ������
        GameObject axe = Instantiate(axePrefab, position, Quaternion.identity);
        Rigidbody2D axeRigidbody = axe.GetComponent<Rigidbody2D>();

        // ������ ���ϴ� ���� ������ �־� ������
        Vector3 axeForce = Vector3.right * 10f; // ���÷� �������� ���� ����
        axeRigidbody.AddForce(axeForce, ForceMode2D.Impulse);
    }
}