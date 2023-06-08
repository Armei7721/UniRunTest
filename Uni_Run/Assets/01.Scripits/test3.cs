using UnityEngine;
//�ݶ��̴��� ���� �˼� �ִ� ��
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
        // ������Ʈ �߿��� �ݶ��̴� �� �κ��� x ���� ���� �� �ֽ��ϴ�.
        GetColliderEndX();
    }

    private void GetColliderEndX()
    {
        // �ݶ��̴��� �Ҵ�Ǿ� ���� ������ �����մϴ�.
        if (collider == null)
        {
            Debug.LogWarning("�ݶ��̴��� �Ҵ���� �ʾҽ��ϴ�.");
            return;
        }

        // �ݶ��̴��� ��� ���ڸ� �����ɴϴ�.
        Bounds bounds = collider.bounds;

        // ��� ������ �ִ� ������ �����ɴϴ�.
        Vector3 maxPoint = bounds.max;

        // �ݶ��̴��� ����� ���� ������Ʈ�� ��ȯ ������ �����ɴϴ�.
        Transform transform = collider.transform;

        // ���� ��ǥ ������ ���� ���� ��ǥ �������� ��ȯ�մϴ�.
        Vector3 worldMaxPoint = transform.TransformPoint(maxPoint);

        // �ݶ��̴� �� �κ��� x ���� �����ɴϴ�.
        float endX = worldMaxPoint.x;

        //Debug.Log("�ݶ��̴� �� �κ��� x ��: " + endX);
    }
}