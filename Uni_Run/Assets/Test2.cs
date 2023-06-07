using UnityEngine;

// ������ �����ϰ� �ֱ������� ���ġ�ϴ� ��ũ��Ʈ
public class Test2 : MonoBehaviour
{
    public GameObject[] prefabs; // ���� �����յ��� ������ �迭

    public int count = 3; // ������ ������ ����

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
        platforms = new GameObject[count];

        // �������� �ʱ�ȭ�ϰ� ����� ���ǵ��� �̸� �����մϴ�.
        for (int i = 0; i < count; i++)
        {
            GameObject prefab = prefabs[i % prefabs.Length]; // �����յ��� �����ϸ� �����մϴ�.
            platforms[i] = Instantiate(prefab, poolPosition, Quaternion.identity);
        }

        lastSpawnTime = 0f;
        timeBetSpawn = 0f;
    }

    void Update()
    {
        // ���� ���� ������ ��� ������Ʈ�� �����մϴ�.
        if (GameManager.instance.isGameover)
        {
            return;
        }
        if (Time.time >= lastSpawnTime + timeBetSpawn)
        {
            lastSpawnTime = Time.time;

            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);

            timeBetSpawn = 0;
            platforms[currentIndex].SetActive(false);
            platforms[currentIndex].SetActive(true);

            platforms[currentIndex].transform.position = new Vector2(xPos, -3.9f);
            currentIndex++;

            if (currentIndex >= count)
            {
                currentIndex = 0;
            }
        }
    }
}
