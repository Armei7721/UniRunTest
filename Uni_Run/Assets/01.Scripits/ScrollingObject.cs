
using UnityEngine;
// ���� ������Ʈ�� ��� �������� �����̴� ��ũ��Ʈ
public class ScrollingObject : MonoBehaviour
{
    public static float speed = 10;
    private int TimeSpeed ;
    private void Start()
    {
        TimeSpeed += (int)Time.time;
    }
    void Update()
    {
        //���ӿ����� �ƴ϶��
        //if (!GameManager.instance.isGameover)
        //{   //�ʴ� speed�� �ӵ��� �������� �����̵�
           
        //    transform.Translate(Vector3.left * speed * Time.deltaTime); 
            
        //}
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
