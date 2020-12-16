using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public Text timerText;
    public Text scoreText;
    public GameObject countdown;

    [HideInInspector]
    public int score = 0;
    [HideInInspector]
    public bool isGameStarted;

    private Animator countdownAnim;
    private float timeLeft = 60f;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        isGameStarted = false;

        countdownAnim = countdown.GetComponent<Animator>();

        StartGame();
    }

    private void StartGame()
    {
        StartCoroutine(OneTwoThree());

        AudioManager.instance.PlaySound("background");

        scoreText.text = "Score: " + score.ToString();
    }

    void OnEnable()
    {
        EventManager.StartListening("TargetHit", IncreaseScore);
    }

    void OnDisable()
    {
        EventManager.StopListening("TargetHit", IncreaseScore);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        if (isGameStarted)
        {
            int temp = (int)timeLeft;
            timerText.text = "Time Left: " + temp.ToString();

            timeLeft -= Time.deltaTime;
        }

        if (timeLeft < 0)
        {
            GameOver();
        }
    }

    public void IncreaseScore()
    {
        score = score + 1;
        scoreText.text = "Score: " + score.ToString();
        PlayerPrefs.SetInt("Score", score);
    }

    void GameOver()
    {
        SceneManager.LoadScene(3);
    }

    IEnumerator OneTwoThree()
    {
        AudioManager.instance.PlaySound("countdown");

        yield return new WaitForSeconds(1f);

        countdownAnim.SetTrigger("count");

        yield return new WaitForSeconds(6f);

        isGameStarted = true;
    }
}
