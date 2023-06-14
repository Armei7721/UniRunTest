
using UnityEngine;
// 게임 오브젝트를 계속 왼쪽으로 움직이는 스크립트
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
        //게임오버가 아니라면
        if (!GameManager.instance.isGameover)
        {

            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }
}
