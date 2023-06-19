using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isGameover = false;
    public bool isEnding = false;
    public TextMeshProUGUI Distance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI EndingscoreText;
    public Image endingImage;
    public GameObject gameoverUI;

    public GameObject endingPrefab;
    public GameObject trapOff;
    public GameObject player;

    private bool isPaused = false;
    public int distance ;
    public float timeReset;
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
        distance = 100;
        trapOff = GameObject.Find("TrapSpawner");
        player = GameObject.Find("Player");

        PlayerController playerController = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        Arrival();
        if((isGameover || isEnding) && Input.GetMouseButtonUp(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    public void Arrival()
    {
        timeReset += Time.deltaTime;
        distance =(int)(100f -timeReset);
        Distance.text = "도착까지 : " + distance +"m";
        
        if (!hasInstantiatedEnding && distance <= 1)
        {
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

    public void Stop()
    {
        if(isPaused== false)
        {

            Time.timeScale = 0;
            isPaused = true;
        }
        
    }
    public void Go()
    {
        Time.timeScale = 1;
        isPaused = false;
    }
 
    public void OnPlayerDead()
    {

        isGameover = true;
        gameoverUI.SetActive(true);
    }
    public void Ending()
    {
        EndingscoreText.text = "Score :" + score; 
        isEnding = true;
        endingImage.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    
    public void Exit()
    {
        Application.Quit();
    }
    public void Restart()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
}