
using UnityEngine;
// ���� ������Ʈ�� ��� �������� �����̴� ��ũ��Ʈ
public class ScrollingObject : MonoBehaviour
{
    public static float speed = 15f;

    private void Start()
    {

    }
    void Update()
    {
        //���ӿ����� �ƴ϶��
        if (!GameManager.instance.isGameover)
        {   //�ʴ� speed�� �ӵ��� �������� �����̵�
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }
}
