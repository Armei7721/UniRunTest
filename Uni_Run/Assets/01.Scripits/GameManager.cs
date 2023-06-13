using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isGameover = false;
    public TextMeshProUGUI scoreText;
    public GameObject gameoverUI;

    public GameObject endingPrefab;
    public GameObject trapOff;
    public GameObject Player;

    private int score = 0;

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
        Player = GameObject.Find("Player");
        
        PlayerController playerController = Player.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (isGameover && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (Time.time >= 5f)
        {
            
            trapOff.SetActive(false);
            transform.position = new Vector3(22f, 2f, 0f);
            Instantiate(endingPrefab, transform.position, Quaternion.identity);
            Time.timeScale = 0.5f;
            PlayerController playerController = Player.GetComponent<PlayerController>();
            playerController.enabled = false;


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
