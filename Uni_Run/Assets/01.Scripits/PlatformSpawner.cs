using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject[] childObjects; // 자식 오브젝트를 담을 배열
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
        //    int randomIndex = Random.Range(0, childObjects.Length);
        //    currentIndex = randomIndex;
        //    childObjects[currentIndex].SetActive(true);
        //    childObjects[currentIndex].transform.position = new Vector2(xPos, -3.7f);
        //}

        // 발판이 카메라를 벗어났는지 확인하고 재배치합니다.
        for (int i = 0; i < count; i++)
        {
            
            //플랫폼 배열의 활성화 되어있고 플랫폼 배열의 포지션 x의 값이 카메라 포지션의 - 45f 값 만큼 지나갔을때 발동
            if (platforms[i].activeSelf && platforms[i].transform.position.x < Camera.main.transform.position.x - 45f)
            {   //플랫폼 비활성화를 시키고
                platforms[i].SetActive(false);
                
                // randomIndex의 범위 0~childObjects의 수 중 랜덤으로 할당
                int randomIndex = Random.Range(0, childObjects.Length);
                currentIndex = randomIndex;
                childObjects[currentIndex].transform.position = new Vector2(xPos, -3.7f);
                childObjects[currentIndex].SetActive(true);
               
            }
        }
    }
}