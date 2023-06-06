using UnityEngine;

// 발판을 생성하고 주기적으로 재배치하는 스크립트
public class PlatformSpawner : MonoBehaviour
{
    public GameObject[] childObjects; // 자식 오브젝트를 담을 배열
    public int count; // 생성할 발판의 개수

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
        childObjects = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            childObjects[i] = transform.GetChild(i).gameObject;
        }

        count = childObjects.Length;
        platforms = new GameObject[count]; // platforms 배열 초기화

        for (int i = 0; i < count; i++)
        {
            platforms[i] = Instantiate(childObjects[i], poolPosition, Quaternion.identity);
            platforms[i].SetActive(false); // 초기에 발판들을 비활성화 상태로 설정
        }

        lastSpawnTime = 0f;
        timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
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

            platforms[currentIndex].SetActive(false);

            int randomIndex = Random.Range(0, childObjects.Length);
            currentIndex = randomIndex;
            childObjects[currentIndex].SetActive(true);
            childObjects[currentIndex].transform.position = new Vector2(xPos, -3.9f);
        }
    }
}