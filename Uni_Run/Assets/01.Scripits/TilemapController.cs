using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour
{
    public GameObject prefab; // ������ ��� ������

    void SpawnObject()
    {
        Vector3 position = new Vector3(0f, 0f, 0f); // ���ο� �ν��Ͻ��� ��ġ
        Quaternion rotation = Quaternion.identity; // ���ο� �ν��Ͻ��� ȸ��

        GameObject newInstance = Instantiate(prefab, position, rotation);
        // ��� �������� ����Ͽ� ���ο� �ν��Ͻ� ����

        // ������ �ν��Ͻ��� ���� �߰����� �����̳� ���� ���� ����
    }
    private void Start()
    {
        SpawnObject();
    }
    void Update()
    {

    }
}
