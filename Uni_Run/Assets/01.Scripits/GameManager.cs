using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isGameover = false;
    public TextMeshProUGUI Distance;
    public TextMeshProUGUI scoreText;
    public GameObject gameoverUI;

    public GameObject endingPrefab;
    public GameObject trapOff;
    public GameObject player;

    private int distance ;
    private int score = 0;
    private bool hasInstantiatedEnding = false; // 엔딩을 이미 복사했는지 여부를 추적하는 변수

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("씬에 두 개 이상의 게임 매니저가 존재합니다!");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        trapOff = GameObject.Find("TrapSpawner");
        player = GameObject.Find("Player");

        PlayerController playerController = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        Arrival();
        if (isGameover && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
    }
    public void Arrival()
    {
        distance = 100 - (int)Time.time;
        Distance.text = "도착까지 : " + distance +"m";
        if (!hasInstantiatedEnding && distance == 0)
        {
            distance = 0;
            trapOff.SetActive(false);
            transform.position = new Vector3(20f, 2f, 0f);
            Instantiate(endingPrefab, transform.position, Quaternion.identity);
            Time.timeScale = 0.5f;
            hasInstantiatedEnding = true;
        }
    }
    public void AddScore(int newScore)
    {
        if (!isGameover)
        {
            score += newScore;
            scoreText.text = "Score : " + score;
        }
    }
    public void OnPlayerDead()
    {
        isGameover = true;
        gameoverUI.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}