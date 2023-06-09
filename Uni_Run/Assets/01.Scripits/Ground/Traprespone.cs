using UnityEngine;

public class Traprespone : MonoBehaviour
{
    public GameObject[] prefabs; // ���ع� ������ �迭
    public float spawnInterval = 4f; // ���ع� ��ȯ ����

    private float nextSpawnTime; // ���� ��ȯ �ð�

    private void Start()
    {
        nextSpawnTime = Time.time + spawnInterval; // �ʱ� ���� ��ȯ �ð� ����
    }

    private void Update()
    {
        // ���� �ð��� ���� ��ȯ �ð����� ũ�ų� ������ ���ع��� ��ȯ�մϴ�.
        if (Time.time >= nextSpawnTime)
        {
            SpawnObstacle();
            nextSpawnTime = Time.time + spawnInterval; // ���� ��ȯ �ð� ������Ʈ
        }
    }

    private void SpawnObstacle()
    {
        int randomIndex = Random.Range(0, prefabs.Length); // ������ �ε��� ����
        GameObject obstacle = Instantiate(prefabs[randomIndex], prefabs[randomIndex].transform.position+new Vector3(20f,0f,0f), Quaternion.identity); // ���ع� ��ȯ
        Destroy(obstacle, 10);
        // �߰����� ������ �ʿ��� ���, ���� ��� ���ع� ��ġ ���� ���� ������ �� �ֽ��ϴ�.
    }
}