using UnityEngine;

// 발판을 생성하고 주기적으로 재배치하는 스크립트
public class Test2 : MonoBehaviour
{
    public GameObject[] prefabs; // 발판 프리팹들을 참조할 배열

    public int count = 3; // 생성할 발판의 개수

    public float timeBetSpawnMin = 1.25f;
    public float timeBetSpawnMax = 2.25f;
    private float timeBetSpawn; // 다음 배치까지의 시간 간격
    private float xPos = 22f; // 발판을 배치할 x 좌표

    private GameObject[] platforms; // 미리 생성한 발판들
    private int currentIndex = 0; // 사용할 현재 순번의 발판

    private Vector2 poolPosition = new Vector2(0, -25); // 초기 위치에 숨겨둘 발판들의 위치
    private float lastSpawnTime; // 마지막 발판 배치 시점

    void Start()
    {
        platforms = new GameObject[count];

        // 변수들을 초기화하고 사용할 발판들을 미리 생성합니다.
        for (int i = 0; i < count; i++)
        {
            GameObject prefab = prefabs[i % prefabs.Length]; // 프리팹들을 루프하며 참조합니다.
            platforms[i] = Instantiate(prefab, poolPosition, Quaternion.identity);
        }

        lastSpawnTime = 0f;
        timeBetSpawn = 0f;
    }

    void Update()
    {
        // 게임 오버 상태인 경우 업데이트를 종료합니다.
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
