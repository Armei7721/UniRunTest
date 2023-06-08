using UnityEngine;

public class Test2 : MonoBehaviour
{
    public GameObject[] prefabs; // �������� ���� �迭
    private int count; // ������ ������ ����

    public float timeBetSpawnMin = 1.25f;
    public float timeBetSpawnMax = 2.25f;
    private float timeBetSpawn; // ���� ��ġ������ �ð� ����
    private float xPos = 22f; // ������ ��ġ�� x ��ǥ

    private GameObject[] platforms; // �̸� ������ ���ǵ�
    private int currentIndex = 0; // ����� ���� ������ ����

    private Vector2 poolPosition = new Vector2(0, -25); // �ʱ� ��ġ�� ���ܵ� ���ǵ��� ��ġ
    private float lastSpawnTime; // ������ ���� ��ġ ����

    void Start()
    {
        count = prefabs.Length;
        platforms = new GameObject[count]; // platforms �迭 �ʱ�ȭ

        for (int i = 0; i < count; i++)
        {
            platforms[i] = Instantiate(prefabs[i], poolPosition, Quaternion.identity);
            //platforms[i].SetActive(false); // �ʱ⿡ ���ǵ��� ��Ȱ��ȭ ���·� ����
        }
        lastSpawnTime = 0f;
        timeBetSpawn = 3f;
    }

    void Update()
    {
        // ���� ���� ������ ��� ������Ʈ�� �����մϴ�.
        if (GameManager.instance.isGameover)
        {
            return;
        }

        //if (Time.time >= lastSpawnTime + timeBetSpawn)
        //{
        //    lastSpawnTime = Time.time;
        //    platforms[currentIndex].SetActive(false);

        //    int randomIndex = Random.Range(0, count);
        //    currentIndex = randomIndex;
        //    GameObject newPlatform = Instantiate(prefabs[currentIndex], new Vector2(xPos, -3.7f), Quaternion.identity);
        //    platforms[currentIndex] = newPlatform;

        //    xPos += 10f; // ���� ������ �����մϴ�.
        //}

        for (int i = 0; i < count; i++)
        {
            Debug.Log(platforms[i].transform.position.x);
            if (platforms[i].activeSelf && platforms[i].transform.position.x < Camera.main.transform.position.x - 23.5f)
            {
                platforms[i].SetActive(false);

                int randomIndex = Random.Range(0, count);
                currentIndex = randomIndex;
                GameObject newPlatform = Instantiate(prefabs[currentIndex], new Vector2(xPos, -3.7f), Quaternion.identity);
                platforms[currentIndex] = newPlatform;
                Debug.Log("Xpos�� ����");
                Debug.Log(xPos);
                
            }
        }
    }
}