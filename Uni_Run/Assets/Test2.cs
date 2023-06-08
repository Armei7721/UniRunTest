using UnityEngine;

public class Test2 : MonoBehaviour
{
    public GameObject[] prefabs; // 프리팹을 담을 배열
    private int count; // 생성할 발판의 개수

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
        count = prefabs.Length;
        platforms = new GameObject[count]; // platforms 배열 초기화

        for (int i = 0; i < count; i++)
        {
            platforms[i] = Instantiate(prefabs[i], poolPosition, Quaternion.identity);
            //platforms[i].SetActive(false); // 초기에 발판들을 비활성화 상태로 설정
        }
        lastSpawnTime = 0f;
        timeBetSpawn = 3f;
    }

    void Update()
    {
        // 게임 오버 상태인 경우 업데이트를 종료합니다.
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

        //    xPos += 10f; // 발판 간격을 조정합니다.
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
                Debug.Log("Xpos의 값은");
                Debug.Log(xPos);
                
            }
        }
    }
}