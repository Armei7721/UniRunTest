using UnityEngine;
//콜라이더의 값을 알수 있는 것
public class test3 : MonoBehaviour
{
    private Collider2D collider;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
        GetColliderEndX();
    }

    private void Update()
    {
        // 업데이트 중에도 콜라이더 끝 부분의 x 값을 얻을 수 있습니다.
        GetColliderEndX();
    }

    private void GetColliderEndX()
    {
        // 콜라이더가 할당되어 있지 않으면 종료합니다.
        if (collider == null)
        {
            Debug.LogWarning("콜라이더가 할당되지 않았습니다.");
            return;
        }

        // 콜라이더의 경계 상자를 가져옵니다.
        Bounds bounds = collider.bounds;

        // 경계 상자의 최대 지점을 가져옵니다.
        Vector3 maxPoint = bounds.max;

        // 콜라이더가 연결된 게임 오브젝트의 변환 정보를 가져옵니다.
        Transform transform = collider.transform;

        // 로컬 좌표 공간의 값을 월드 좌표 공간으로 변환합니다.
        Vector3 worldMaxPoint = transform.TransformPoint(maxPoint);

        // 콜라이더 끝 부분의 x 값을 가져옵니다.
        float endX = worldMaxPoint.x;

        //Debug.Log("콜라이더 끝 부분의 x 값: " + endX);
    }
}