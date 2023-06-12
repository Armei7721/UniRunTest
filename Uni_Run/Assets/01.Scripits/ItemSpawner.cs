using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] itemPrefabs; // 리스폰될 아이템 프리팹들의 배열
    public GameObject ScoreCoin;
    private float Itemtime;
    private float cointime;
    private float Cointransform;
    public float itmetimer= 20f;
    private float cointimer = 2;

    private float yposMin = -1.15f;
    private float yposMax = 3.5f;
    private float ypos;
    private void Start()
    {
        
    }
    private void Update()
    {
        SpawnItem();
        SpawnCoin();
    }
    public void SpawnCoin()
    {   cointime+= Time.deltaTime;
        if(cointime> cointimer)
        {
            float ypos = Random.Range(yposMin, yposMax);
            ScoreCoin.transform.position = new Vector2(23f, ypos);
            GameObject spawnedCoin = Instantiate(ScoreCoin, ScoreCoin.transform.position, Quaternion.identity);
            cointime = 0f;
            Destroy(spawnedCoin, 5f);
        }
    }
    public void SpawnItem()
    {
        Itemtime += Time.deltaTime;
        
        if (Itemtime > itmetimer)
        {
            float ypos = Random.Range(yposMin, yposMax);
            // 아이템을 랜덤하게 선택
            int randomIndex = Random.Range(0, itemPrefabs.Length);
            GameObject itemPrefab = itemPrefabs[randomIndex];
            itemPrefab.transform.position = new Vector2(23f, ypos); 

            // 아이템을 생성하고 위치 설정
            GameObject spawnedItem = Instantiate(itemPrefab, itemPrefab.transform.position, Quaternion.identity);
            Itemtime = 0f;
            Destroy(spawnedItem, 5f);
        }
    }
}