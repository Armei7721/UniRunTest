using UnityEngine;

// ������ �����ϰ� �ֱ������� ���ġ�ϴ� ��ũ��Ʈ
public class Test : MonoBehaviour
{
    public GameObject platformPrefab; // ������ ������ ���� ������
    public int count = 3; // ������ ������ ����

    public float timeBetSpawnMin = 1.25f; // ���� ��ġ������ �ð� ���� �ּڰ�
    public float timeBetSpawnMax = 2.25f; // ���� ��ġ������ �ð� ���� �ִ�
    private float timeBetSpawn; // ���� ��ġ������ �ð� ����

    private float xPos = 30f; // ��ġ�� ��ġ�� x ��

    private GameObject[] platforms; // �̸� ������ ���ǵ�
    private int currentIndex = 0; // ����� ���� ������ ����

    private Vector2 poolPosition = new Vector2(0, -25); // �ʹݿ� ������ ���ǵ��� ȭ�� �ۿ� ���ܵ� ��ġ
    private float lastSpawnTime; // ������ ��ġ ����


    void Start()
    {
        // �������� �ʱ�ȭ�ϰ� ����� ���ǵ��� �̸� ����
        platforms = new GameObject[count];
        //Count��ŭ�� ���� ������ ���ο� ���� �迭 ����
        for (int i = 0; i < count; i++)
        {   //platformPrefab�� �������� �� ������ poolPosition ��ġ�� ���� ����
            //������ ������ platform �迭�� �Ҵ�
            platforms[i] = Instantiate(platformPrefab, poolPosition, Quaternion.identity);
        }
        // ������ ��ġ ���� �ʱ�ȭ
        lastSpawnTime = 0f;
        // ������ ��ġ������ �ð� ������ 0�� �ʱ�ȭ
        timeBetSpawn = 0f;
    }

    void Update()
    {
        //���ӿ��� ���¿����� �������� ����
        if (GameManager.instance.isGameover)
        {
            return;
        }
        //������ ��ġ �������� timeBetSpawn �̻� �ð��� �귶�ٸ�
        if (Time.time >= lastSpawnTime + timeBetSpawn)
        {
            //��ϵ� ������ ��ġ ������ ���� �������� ����
            lastSpawnTime = Time.time;

            // ���� ��ġ������ �ð� ������ timeBetSpawnMin, timeBetSpawnMax ���̿��� ���� ����
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);

            //����� ���� ������ ���� ���� ������Ʈ�� ��Ȱ��ȭ�ϰ� ��� �ٽ� Ȱ��ȭ
            // �̶� ������ Platform ������Ʈ�� OnEnable �޼��尡 �����
            platforms[currentIndex].SetActive(false);
            platforms[currentIndex].SetActive(true);

            //���� ������ ������ ȭ�� �����ʿ� ���ġ
            platforms[currentIndex].transform.position = new Vector2(xPos, 2.3f);
            //�����ѱ��
            currentIndex++;

            //������ ������ �����ߴٸ� ������ ����
            if (currentIndex >= count)
            {
                currentIndex = 0;
            }

        }
    }
}