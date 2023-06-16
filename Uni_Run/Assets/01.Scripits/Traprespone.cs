using UnityEngine;

public class Traprespone : MonoBehaviour
{
    public GameObject[] prefabs; // 방해물 프리팹 배열
    public float spawnInterval = 4f; // 방해물 소환 간격

    private GameManager gameManager;
    private float nextSpawnTime; // 다음 소환 시간

    private void Start()
    {
        gameManager = GameManager.instance;
        nextSpawnTime = Time.time + spawnInterval; // 초기 다음 소환 시간 설정
    }

    private void Update()
    {
        // 현재 시간이 다음 소환 시간보다 크거나 같으면 방해물을 소환합니다.
        if (Time.time >= nextSpawnTime)
        {
            SpawnObstacle();
            nextSpawnTime = Time.time + spawnInterval; // 다음 소환 시간 업데이트
        }
    }

    private void SpawnObstacle()
    {
        int randomIndex = Random.Range(0, prefabs.Length); // 랜덤한 인덱스 선택
        GameObject obstacle = Instantiate(prefabs[randomIndex], prefabs[randomIndex].transform.position+new Vector3(20f,0f,0f), Quaternion.identity); // 방해물 소환
        Destroy(obstacle, 10);
        if (gameManager.distance <= 0)
        {
            Destroy(obstacle);
        }
        // 추가적인 설정이 필요한 경우, 예를 들어 방해물 위치 설정 등을 수행할 수 있습니다.
    }
}