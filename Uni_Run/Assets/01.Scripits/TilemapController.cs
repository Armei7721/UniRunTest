using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour
{
    public GameObject prefab; // 복제할 대상 프리팹

    void SpawnObject()
    {
        Vector3 position = new Vector3(0f, 0f, 0f); // 새로운 인스턴스의 위치
        Quaternion rotation = Quaternion.identity; // 새로운 인스턴스의 회전

        GameObject newInstance = Instantiate(prefab, position, rotation);
        // 대상 프리팹을 사용하여 새로운 인스턴스 생성

        // 생성된 인스턴스에 대한 추가적인 설정이나 조작 수행 가능
    }
    private void Start()
    {
        SpawnObject();
    }
    void Update()
    {

    }
}
